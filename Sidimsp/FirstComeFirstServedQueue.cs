using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class FirstComeFirstServedQueue : ProcessQueue
	{
		public FirstComeFirstServedQueue (int timeQuantum)
		{
			_processesQ = new Queue<Process>();
			this._timeQuantum = timeQuantum;
		}
		
		private Queue<Process> _processesQ; //Used to store the processes
		public Queue<Process> ProcessesQ{
			get{ return this._processesQ; }
			set{ this._processesQ = value; }
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
		
		// inherited methods
		public override void AddProcess (Process p)
		{
			//if the process is running
			_processesQ.Enqueue( p );
		}
		
		public override void RemoveProcess (Process p)
		{
			throw new NotImplementedException ();
		}
		
		//Return the process at the top of the queue
		public override Process getProcess ()
		{
			 return _processesQ.Peek();
		}
		
		public override int Run() {
			int time = 0;
			
			return time;
		}
	}
}

