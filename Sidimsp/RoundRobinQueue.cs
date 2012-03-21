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
		
		private Queue< Process > processes{ get; set;}
		private int quantum{ get; set; }
		
		// Methods
		public override void AddProcess(Process p) {
				processes.Enqueue( p );
		}
		
		public override void RemoveProcess(Process p) {
		}
		
		public override void Run(){}
		
	}
}

