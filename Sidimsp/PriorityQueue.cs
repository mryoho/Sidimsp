using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class PriorityQueue : ProcessQueue
	{
		public PriorityQueue (int timeQuantum)
		{
			this._timeQuantum = timeQuantum;
			_processesQ = new List<Process>();
		}
		
		public override Boolean isFinishedProcessing(){
			if(ProcessesQ.Count == 0){
				Console.WriteLine("num processes is: " + ProcessesQ.Count.ToString());
				return true;	
			}else{
				Console.WriteLine ("num processes is " + ProcessesQ.Count.ToString());
				return false;
			}	
			
		}
		
		public override int getCount(){
			return _processesQ.Count;	
		}
		
		
		protected List<Process> _processesQ; //Used to store the processes
		public List<Process> ProcessesQ{
			get{ return this._processesQ; }
			set{ this._processesQ = value; }
		}
		
		// inherited methods
		public override void AddProcess (Process p)
		{
			_processesQ.Add( p );
		}
		
		public override void RemoveProcess (Process p)
		{
			throw new NotImplementedException ();
		}
		
		public override Process getProcess ()
		{
			Process returnedProcess = null;
			 return returnedProcess;
		}
		
		public override int Run() {
			int time = 0;
			
			return time;
		}
	}
}

