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
		
		//Take in the current time
		public Boolean Run() {

			Console.WriteLine("MultiLevelFeedbackQueue Running");
			
			Process returnedProcess = null;
			
			//Get the optimal process
			returnedProcess = getProcess();
			
			//If the returned process is null, then we don't have any work to do.
			if(returnedProcess != null){
				
				//set the status to "running"
				returnedProcess.ProcessState = "Running";
				
				//increment the "timeWorkedOnQuantum" within the returned Process, this will keep track of how long this process has been worked on by the queue
				
				//decrement the "cpuBurstTimeRemaining," this keeps track of how much longer the process needs to be worked on
				
				//if the "timeWorkedOnQuantum" equals the queue's "timeQuantum" 
					//then reset the "timeWorkedOnQuantum"
					
				
				
				
				return true;
			}else{
				return false;	
			}
			
		}
		
		public Process getProcess(){
			Process returnedProcess = null;
			return returnedProcess;
		}
		
		public Boolean isFinishedProcessing(){
			
			Boolean isFinished = true;
			
			//Check each of the Queues
			for(int i = 0; i < _queues.Count; i++){
				
				//if any of the queues still have Processes, then we aren't done processing this MultiLevelFeedbackQueue
				if(!_queues[i].isFinishedProcessing()){
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

