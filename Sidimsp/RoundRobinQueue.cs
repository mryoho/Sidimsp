using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class RoundRobinQueue : ProcessQueue
	{
		
		public RoundRobinQueue (int timeQuantum, int RRQuantum)
		{
			this._timeQuantum = timeQuantum;
			this._RRquantum = RRQuantum;
		}
		
		private int _RRquantum;
		
		// Methods
		
		public override void RemoveProcess(Process p) {
		}
		
		private static int SortByTimeWorkedOnQuantum(Process x, Process y)
   	 	{	
			if(x.timeWorkedOnQuantum > y.timeWorkedOnQuantum){
				return 1;
			}else if(x.timeWorkedOnQuantum == y.timeWorkedOnQuantum){
				return 0;
			}else{
				return -1;
			}
		}
		
		public override Process getProcess ()
		{
			Process returnedProcess = null;
			Boolean runningProcessFound = false;
			
			//Sort the processes by their timeWorkedOnQuantum
			_processesQ.Sort(SortByTimeWorkedOnQuantum);
			
			for(int i = 0; i < _processesQ.Count; i++){
				//Look for a process that still has work to be done on it
				if(_processesQ[i].timeWorkedOnQuantum % _RRquantum != 0){
					returnedProcess = _processesQ[i];
					_processesQ.RemoveAt(i);
					runningProcessFound = true;
					break;	
				}
			}
			
			//If we can't find one that is already running, then choose one with the lowest timeWorkedOnQuantum
			if(!runningProcessFound){
				//Sort the Processes by timeWorkedOnQuantum
				//ALREADY SORTED FROM EARLIER
				
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
		
		public override int Run(){
			int time = 0;
			
			return time;
		}
		
	}
}

