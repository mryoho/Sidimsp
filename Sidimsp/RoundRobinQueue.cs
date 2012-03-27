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
		public override void AddProcess(Process p) {
			_processesQ.Enqueue( p );
		}
		
		public override void RemoveProcess(Process p) {
		}
		
		public override int Run(){
			int time = 0;
			
			return time;
		}
		
	}
}

