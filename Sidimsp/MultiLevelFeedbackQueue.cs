using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Sidimsp
{
	public class MultiLevelFeedbackQueue
	{
		private List<ProcessQueue> _queues{ get; set;}
		
		public MultiLevelFeedbackQueue ( List<int> queueTypes, List<int> quantums)
		{
			//Stores each of the queues (PriorityQueue, RoundRobinQueue, FCFSQueue)
			_queues = new List<ProcessQueue>();
			
			// Create the multilevelfeedbackqueue based on the number of queue types provided
			for( int i = 0; i < queueTypes.Count; i++ ){
				switch( queueTypes[i] ) {
				case 0:
					_queues.Add (new PriorityQueue(quantums[i]));
					break;
				case 1:
					_queues.Add (new RoundRobinQueue(quantums[i], 2));
					break;
				case 2:
					_queues.Add (new FirstComeFirstServedQueue(quantums[i]));
					break;
				}
			}
		}
		
		// inherited methods
		public void AddProcess(Process p){
			_queues[0].AddProcess(p);
		}
		
		//Do work on a process, if work is done successfully, return true.
		public Boolean Run() {

			Console.WriteLine("MultiLevelFeedbackQueue Running");
			
			//The first part of the pair is the process, the second is the index of the queue it came from with regards to the "_queues"
			//Get the optimal process from one of the queues
			Pair<Process,int> returnedProcess = getProcess ();
			
			//If the returned process is null, then we don't have any work to do.
			if(returnedProcess.First != null){
				
				//set the status to "Running"
				returnedProcess.First.ProcessState = "Running";
				
				//increment the "timeWorkedOnQuantum" within the returned Process, this will keep track of how long this process has been worked on by the queue
				returnedProcess.First.timeWorkedOnQuantum++;
				
				//decrement the "cpuBurstTimeRemaining," this keeps track of how much longer the process needs to be worked on
				returnedProcess.First.CpuBurstTimeRemaining--;
				
				
				if(returnedProcess.First.CpuBurstTimeRemaining > 0){
					if(returnedProcess.First.timeWorkedOnQuantum == _queues[returnedProcess.Second].timeQuantum){
						
						returnedProcess.First.timeWorkedOnQuantum = 0;
						
						//check if the there is another queue beneath the current one
						if(_queues.Count > (returnedProcess.Second +1)){
							//then, add the process to that queue
							_queues[returnedProcess.Second+1].AddProcess(returnedProcess.First);
						}else{
							//else, add to the same queue, defined in the pair
							_queues[returnedProcess.Second].AddProcess(returnedProcess.First);
						}
						
					}else{
					_queues[returnedProcess.Second].AddProcess(returnedProcess.First);
					}
					
				}else{
						//set the status to "Finished"
						returnedProcess.First.ProcessState = "Finished";
						
						//then add the Process to the Processor's completedProcesses ArrayList
						Processor.AddFinishedProcess(returnedProcess.First);	
				}
				
				return true;
			}else{
				return false;	
			}
		}
		
		public Pair<Process,int> getProcess(){
			Pair<Process,int> returnedProcess = new Pair<Process, int>(null,0);
			
			//Continue to search for a queue that has processes requiring more work
			for(int i = 0; i < _queues.Count; i++){
				
				//if we find a queue with processes to be worked on, grab the process
				 if(_queues[i].ProcessesQ.Count > 0){
					returnedProcess.First = _queues[i].getProcess();
					returnedProcess.Second = i;
					break;
				}
				
			}
			
			//if no process is found, the returnedProcess.First will be null
			return returnedProcess;
		}
		
		public Boolean isFinishedProcessing(){
			
			Boolean isFinished = true;
			
			//Check each of the Queues
			for(int i = 0; i < _queues.Count; i++){
				
				//if any of the queues still have Processes, then we aren't done processing this MultiLevelFeedbackQueue
				if(_queues[i].ProcessesQ.Count > 0){
					isFinished = false;
					break;
				}
			}
			return isFinished;
		}
		
		
		// other methods
		public void AddQueue(ProcessQueue pq){
			//_queues.Add(pq);
		}
	}
}

