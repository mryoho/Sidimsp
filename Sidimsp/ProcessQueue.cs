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
		
		protected Queue<Process> _processesQ;
		public Queue< Process > ProcessesQ{
			get{ return this._processesQ; }
			set{ this._processesQ = value; }
		}
		
		public abstract void AddProcess(Process p);
		public abstract void RemoveProcess(Process p);
		public abstract int Run();
		
	}
}

