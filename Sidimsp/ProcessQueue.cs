using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public abstract class ProcessQueue
	{
		
		public ProcessQueue ()
		{
			_processesQ = new Queue<Process>();
		}
		
		protected int _timeQuantum; //Use this instead of a pair, indicates time remaining before switching to a different process
		
		protected Queue<Process> _processesQ; //Used to store the processes
		public Queue< Process > ProcessesQ{
			get{ return this._processesQ; }
			set{ this._processesQ = value; }
		}
		
		public abstract void AddProcess(Process p);
		public abstract void RemoveProcess(Process p);
		public abstract int Run(); //This should return a Process.
									//If completed, put into a completed Processes List (which should be global and locked)
									//If not completed, put into the next queue down
									//If NULL, process still has timeQuantum remaining in Queue and is not finished processing. DO NOTHING.
		
	}
}

