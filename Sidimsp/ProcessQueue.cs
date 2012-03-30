using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public abstract class ProcessQueue
	{
		
		public ProcessQueue ()
		{
			
		}
		
		protected int _timeQuantum; //Indicates time remaining before switching to a different process	
		public int timeQuantum{
			get{return this._timeQuantum;}
			private set{this._timeQuantum = value;}
		}
		
		public abstract Boolean isFinishedProcessing();
		public abstract int getCount();
		public abstract Process getProcess();
		public abstract void AddProcess(Process p);
		public abstract void RemoveProcess(Process p);
		public abstract int Run(); //This should return a Process.
									//If completed, put into a completed Processes List (which should be global and locked)
									//If not completed, put into the next queue down
									//If NULL, process still has timeQuantum remaining in Queue and is not finished processing. DO NOTHING.
		
	}
}

