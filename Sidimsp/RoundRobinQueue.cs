using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class RoundRobinQueue : ProcessQueue
	{
		public RoundRobinQueue() {}
		
		public RoundRobinQueue (int quantum)
		{
			this.quantum = quantum;
		}
		
		private int quantum{ get; set; }
		
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

