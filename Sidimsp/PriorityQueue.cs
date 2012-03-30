using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class PriorityQueue : ProcessQueue
	{
		public PriorityQueue (int timeQuantum)
		{
			this._timeQuantum = timeQuantum;
		}
		
		// inherited methods
		public override void RemoveProcess (Process p)
		{
			throw new NotImplementedException ();
		}
		
		private static int SortByPriority(Process x, Process y)
   	 	{	
			if(x.Priority > y.Priority){
				return 1;
			}else if(x.Priority == y.Priority){
				return 0;
			}else{
				return -1;
			}
		}
		
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
				
				//Sort the Processes by priority
				_processesQ.Sort(SortByPriority);
				
				returnedProcess = _processesQ[0];
				_processesQ.RemoveAt(0);
			}
			
			
			 return returnedProcess;
		}
		
		public override int Run() {
			int time = 0;
			
			return time;
		}
	}
}

