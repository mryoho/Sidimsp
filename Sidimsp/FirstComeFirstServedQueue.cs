using System;

namespace Sidimsp
{
	public class FirstComeFirstServedQueue : ProcessQueue
	{
		public FirstComeFirstServedQueue ()
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

