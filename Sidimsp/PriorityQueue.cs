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
			throw new NotImplementedException ();
		}
		
		public override void RemoveProcess (Process p)
		{
			throw new NotImplementedException ();
		}
		
		public override void Run() {}
	}
}

