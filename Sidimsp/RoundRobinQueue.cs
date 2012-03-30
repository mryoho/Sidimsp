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
			_processesQ = new Queue<Process>();
		}
		
		private int _RRquantum;
		
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
		
		// Methods
		public override void AddProcess(Process p) {
			_processesQ.Enqueue( p );
		}
		
		public override void RemoveProcess(Process p) {
		}
		
		public override Process getProcess ()
		{
			//
			
			
			
			Process returnedProcess = null;
			 return returnedProcess;
		}
		
		public override int Run(){
			int time = 0;
			
			return time;
		}
		
	}
}

