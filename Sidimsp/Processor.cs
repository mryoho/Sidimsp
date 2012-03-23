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
		}
		
		private List<Core> _coreList;
		private int _numCores;
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
		
		public void StartSimulation() {
			// generate core objects
			Core newCore;
			for(int i = 0; i < _numCores; i++) {
				newCore = new Core(i, _queueTypes, _quantums);
				_coreList.Add( newCore );
			}
			
			int selectedCore = 0;
			int cpuBurstTime;
			int ioBurstTime;
			int pid;
			int priority;
			int arrivalTime = 0;
			int lastArrivalTime;
			string processState = "Waiting";
			Random random = new Random();
			Process p;				// reference to process objects created below
			
			// generate processes
			for(int i = 0; i <_numProcesses; i++) {
				selectedCore = i % _numCores;		// every time a process is generated, assign it to a different core
				cpuBurstTime = random.Next( _minCpuBurstTimeRemaining, _maxCpuBurstTimeRemaining );
				ioBurstTime = random.Next( _minIOBurstTimeRemaining, _maxIOBurstTimeRemaining );
				pid = i;
				priority = random.Next(0,9);
				lastArrivalTime = arrivalTime;
				arrivalTime = lastArrivalTime + random.Next(0, _arrivalTimeRange);
				
				p = new Process(pid, priority, arrivalTime, processState, cpuBurstTime, ioBurstTime);
				
				_coreList[selectedCore].ProcessQueue.AddProcess(p);
			}
			
			Thread coreThread;
			// generate core threads
			for(int i = 0; i < _numCores; i++){
				coreThread = new Thread(new ThreadStart(_coreList[i].DoWork));	// generate the thread (will not start it)
				_coreThreadList.Add(coreThread);
			}
			
			int result = 0;
			// run the core threads until finished
			try {
				//Start all of the threads
				for(int i = 0; i < _numCores; i++){
					coreThread = _coreThreadList[i];
					coreThread.Start ();
				}
				
				//Join all of the threads
				for(int i = 0; i < _numCores; i++){
					coreThread = _coreThreadList[i];
					coreThread.Join();
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
			
			// message displayed when threads are done
			GlobalVar.WindowConsole.Buffer.Text += ("Processor Finished" + Environment.NewLine);
				
		}
		
		public void LoadBalance() {
			
		}
	}
}

