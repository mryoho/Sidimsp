using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class FirstComeFirstServedQueue : ProcessQueue
	{
		public FirstComeFirstServedQueue (int timeQuantum)
		{
			this._timeQuantum = timeQuantum;
		}
		
		// inherited methods
		
		public override void RemoveProcess (Process p)
		{
			throw new NotImplementedException ();
		}
		
		private static int SortByArrivalTime(Process x, Process y)
   	 	{	
			if(x.ArrivalTime > y.ArrivalTime){
				return 1;
			}else if(x.ArrivalTime == y.ArrivalTime){
				return 0;
			}else{
				return -1;
			}
		}
		
		//Return the process at the top of the queue
		public override Process getProcess ()
		{
			Process returnedProcess = null;
			Boolean runningProcessFound = false;
			
			//Search the list for a process that is already running
			for(int i = 0; i < _processesQ.Count; i++){
				if(_processesQ[i].ProcessState == "Running"){
					returnedProcess = _processesQ[i];
					_processesQ.RemoveAt(i);
					runningProcessFound = true;
					break;
				}
			}
			
			//If we can't find one that is already running, then choose one with the lowest arrival time
			if(!runningProcessFound){
				
				//Sort the Processes by arrival time
				_processesQ.Sort(SortByArrivalTime);
				
				returnedProcess = _processesQ[0];
				_processesQ.RemoveAt(0);
				
				if(returnedProcess.CpuBurstTimeRemaining > 0){
					//Add in costs for context switches, here we are switching to a task that wasn't "Running"
					returnedProcess.totalContextSwitchCosts++;
					returnedProcess.CpuBurstTimeRemaining++;
				}
			}
			
			return returnedProcess;
		}
		
		public override int Run() {
			int time = 0;
			
			return time;
		}
	}
}

