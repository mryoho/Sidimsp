using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public abstract class ProcessQueue
	{
		public ProcessQueue ()
		{
		}
		public abstract void AddProcess(Process p);
		public abstract void RemoveProcess(Process p);
		public abstract void Run();
		
	}
}

