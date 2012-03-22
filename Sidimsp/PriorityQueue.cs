using System;

namespace Sidimsp
{
	public class PriorityQueue : ProcessQueue
	{
		public PriorityQueue ()
		{
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

