using System;
using System.Collections.Generic;
using System.Threading;
using Gtk;

namespace Sidimsp
{
	public class Processor
	{
		public Processor (int numCores, int numProcesses, int maxCpuBurstTimeRemaining, 
			int minCpuBurstTimeRemaing, int maxIOBurstTimeRemaining, int minIOBurstTimeRemaining,
			int arrivalTimeRange, List<int> queueTypes, List<int> quantums)
		{
			//Set the systemTime to zero
			systemTime = 0;
			
			//Initialize the event handling for all of the cores, there should be one for each Core
			autoEvents = new AutoResetEvent[numCores];
			for(int i = 0; i < numCores; i++){
				autoEvents[i] = new AutoResetEvent(false);	
			}
			
			//Initialize the event handling for the Processor, there should be two for each Processor
			manualEvent = new ManualResetEvent(false);
			manualEvent2 = new ManualResetEvent(false);
			
			//Initialize this flag to false, as we don't want cores to finish processing until all of the cores are done.
			stopProcessing = false;
			
			_finishedProcesses = new List<Process>();
			_coreList = new List<Core>();
			_numCores = numCores;
			_numProcesses = numProcesses;
			_queueTypes = queueTypes;
			_quantums = quantums;
			_maxCpuBurstTimeRemaining = maxCpuBurstTimeRemaining;
			_minCpuBurstTimeRemaining = minCpuBurstTimeRemaing;
			_maxIOBurstTimeRemaining = maxIOBurstTimeRemaining;
			_minIOBurstTimeRemaining = minIOBurstTimeRemaining;
			_arrivalTimeRange = arrivalTimeRange;
			_coreThreadList = new List<Thread>();
			_generatedProcesses = new Queue<Process>();
		}
		
		//This will store the number of CPU cycles
		public static int systemTime;
		
		//This will tell the cores to stop processing
		public static Boolean stopProcessing;
		
		//This will synchronize the Cores with the Processor systemTime, one for each Core
		public static AutoResetEvent[] autoEvents;
		
		//This will tell the Cores to wait until the Processor is done creating processes/load balancing
		public static ManualResetEvent manualEvent;
		public static ManualResetEvent manualEvent2;
		
		//The _locker will prevent the _finishedProcesses from being accessed by multiple threads simultaneously
		static readonly object _locker = new object();
		private static List<Process> _finishedProcesses;
		
		private List<Core> _coreList;
		private static int _numCores;
		private int _numProcesses;
		private int _maxCpuBurstTimeRemaining;
		private int _minCpuBurstTimeRemaining;
		private int _maxIOBurstTimeRemaining;
		private int _minIOBurstTimeRemaining;
		private int _simulationLength;
		private int _arrivalTimeRange;
		private List<int> _queueTypes;
		private List<int> _quantums;
		private List<Thread> _coreThreadList;
		private Queue<Process> _generatedProcesses;
		
		public void StartSimulation() {
			// generate core objects
			Core newCore;
			for(int i = 0; i < _numCores; i++) {
				newCore = new Core(i, _queueTypes, _quantums);
				_coreList.Add( newCore );
			}
			
			int cpuBurstTime;
			int ioBurstTime;
			int pid;
			int priority;
			int arrivalTime = 0;
			int lastArrivalTime;
			string processState = "Waiting";
			Random random = new Random();
			Process p;				//Reference to process objects created below
			
			//Generate processes
			for(int i = 0; i <_numProcesses; i++) {
				cpuBurstTime = random.Next( _minCpuBurstTimeRemaining, _maxCpuBurstTimeRemaining );
				ioBurstTime = random.Next( _minIOBurstTimeRemaining, _maxIOBurstTimeRemaining );
				pid = i;
				priority = random.Next(0,9);
				lastArrivalTime = arrivalTime;
				if ( i != 0 )	// make sure the first arrival time is at 0
					arrivalTime = lastArrivalTime + random.Next(0, _arrivalTimeRange);
				
				p = new Process(pid, priority, arrivalTime, processState, cpuBurstTime, ioBurstTime);
				
				_generatedProcesses.Enqueue(p);		// add the process to the list of generated processes
			}
		
			
			// generate core threads
			for(int i = 0; i < _numCores; i++){
				_coreThreadList.Add(new Thread(new ThreadStart(_coreList[i].DoWork)));
			}
			
			int result = 0;
			// run the core threads until finished
			try {
				//Start all of the threads
				for(int i = 0; i < _numCores; i++){
					_coreThreadList[i].Start ();
				}
				
				//Increment the System time, and perform load balancing
				while(!stopProcessing){ //While the cores have work to do, OR, more processes are to be created
					
					LoadNewlyArrivedProcesses();
					
					//Check all of the cores, if all of them are complete, set stopProcessing to true;
					//stopProcessing = isFinishedProcessing();
					if(isFinishedProcessing() && (_generatedProcesses.Count == 0)){
						stopProcessing = true;
					}
					
					//Unblock all of the Core Threads, allowing them to execute
					manualEvent2.Reset ();
					manualEvent.Set ();
					
					if(stopProcessing){
						break;	
					}
					
					//Wait for all of the Core Threads to execute(Number of handles cannot be greater than 64!)
					WaitHandle.WaitAll(autoEvents);
					
					//Block all of the Core Threads, preventing them from executing
					manualEvent.Reset ();
					manualEvent2.Set ();
					
					//Print out the messages generated by the Cores
					GlobalVar.PrintMessages();
					//For some reason, a sleep is needed to prevent the program from hanging.
					Thread.Sleep (10);
						
					//Increment the systemTime variable
					systemTime++;
					
					//Reset the status of all of the Cores
					SetUnFinished(); 			
				}
				
				//Join all of the threads
				for(int i = 0; i < _numCores; i++){
					_coreThreadList[i].Join ();
				}
				
				//At this point all of the threads are complete
				
			}
			catch (ThreadStateException e)
			{
			 Console.WriteLine(e);  // Display text of exception
			 result = 1;            // Result says there was an error
			}
			catch (ThreadInterruptedException e)
			{
			 Console.WriteLine(e);  // This exception means that the thread
			                        // was interrupted during a Wait
			 result = 1;            // Result says there was an error
			}	
			Environment.ExitCode = result;
			
			//Notify user that processing is complete
			GlobalVar.OutputMessage("Processor Finished" + Environment.NewLine);
			
			//Display the turnaround time, response time, wait time 
			
		}
		
		// checks the processingTimeRemaining for all cores, returns the index of the core
		// with the minimum.
		public int checkLoadBalance() {
			int minProcessingTimeRemaining = _coreList[0].totalProcessingTimeRemaining();
			int tempCompare = minProcessingTimeRemaining;
			int coreIndex = 0;
			for( int i = 1; i < _coreList.Count; i++){
				tempCompare = _coreList[i].totalProcessingTimeRemaining();
				if( tempCompare < minProcessingTimeRemaining) {
					minProcessingTimeRemaining = tempCompare;
					coreIndex = i;
				}
			}
			
			return coreIndex;
		}
		
		//This allows the cores to tell Processor that they are done processing
		public static void SetFinished(int numOfFinishedCore){
			if(numOfFinishedCore >= 0){
				autoEvents[numOfFinishedCore].Set();
			}
		}
		
		//This tells the Processor that the cores must do work
		public static void SetUnFinished(){
		for(int i = 0; i < _numCores; i++){
				autoEvents[i] = new AutoResetEvent(false);
			}
		}
		
		//Check to see if all of the cores are finished processing
		public Boolean isFinishedProcessing(){
			Boolean isFinished = true;
			
			//If any of the cores still have processes, return false
			for(int i = 0; i < _numCores; i++){
				if(!_coreList[i].isFinishedProcessing()){
					isFinished = false;
					break;
				}
			}
			
			//Else, return true
			return isFinished;
			
		}
		
		// Checks the systemTime variable against the arrival times of the processes in
		// the _generatedProcesses queue. If the arrival time == the system time, then add
		// that process to the appropriate core using the checkLoadBalance() function.
		public void LoadNewlyArrivedProcesses() {
			
			if(_generatedProcesses.Count > 0){
			Process p = _generatedProcesses.Peek();	// peek at first generated process to get it's arrival time
			while ( p.ArrivalTime == systemTime  && _generatedProcesses.Count > 0) {
				int receivingCore = checkLoadBalance();
				 
				_generatedProcesses.Dequeue();		// actually remove the process p from the head of the queue
				_coreList[receivingCore].ProcessQueue.AddProcess( p );	// add process to appropriate core
				
				// Peek at next process to continue while loop or not
				if(_generatedProcesses.Count > 0){
					p = _generatedProcesses.Peek();
				}
			}
			}
		}
		
		//Add the finished process to the _finishedProcesses queue
		public static void AddFinishedProcess(Process finishedProcess){
			lock(_locker){
				_finishedProcesses.Add(finishedProcess);
			}
		}
		
	}
}

