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
		public override void AddProcess (Process p)
		{
			_processesQ.Enqueue( p );
		}
		
		public override void RemoveProcess (Process p)
		{
			throw new NotImplementedException ();
		}
		
		public override int Run() {
			int time = 0;
			
			return time;
		}
	}
}

