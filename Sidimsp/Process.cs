using System;

namespace Sidimsp
{
	public class Process
	{
		
	public Process(int PID, int Priority, int ArrivalTime, string ProcessState, int CpuBurstTimeRemaining, int IOBurstTimeRemaining){
			this.PID = PID;
			this.Priority = Priority;
			this.ArrivalTime = ArrivalTime;
			this.ProcessState = ProcessState;
			this.CpuBurstTimeRemaining = CpuBurstTimeRemaining;
			this.IOBurstTimeRemaining = IOBurstTimeRemaining;
			this.timeWorkedOnQuantum = 0;
			this.totalContextSwitchCosts = 0;
			this.CompletionTime = 0;
		}
	
	~Process(){}
		
	private int _PID;	
	public int PID{
			get{return this._PID;}
			private set{this._PID = value;}
		}	
	
	private int _Priority;
	public int Priority{
			get{return this._Priority;}
			private set {this._Priority = value;}
		}
		
	private int _ArrivalTime;
	public int ArrivalTime{
			get{return this._ArrivalTime;}
			private set {this._ArrivalTime = value;}
		}
		
	private int _CompletionTime;
	public int CompletionTime{
			get{return this._CompletionTime;}
			set {this._CompletionTime = value;}
		}
		
	
	private string _ProcessState;
	public string ProcessState{
			get{return this._ProcessState;}
			set{this._ProcessState = value;}
		}
		
		
	public int _CpuBurstTimeRemaining;
	public int CpuBurstTimeRemaining{
			get{return this._CpuBurstTimeRemaining;}
			set{this._CpuBurstTimeRemaining = value;}
		}

	public int _IOBurstTimeRemaining;
	public int IOBurstTimeRemaining{
			get{return this._IOBurstTimeRemaining;}
			set{this._IOBurstTimeRemaining = value;}
		}
		
	private int _timeWorkedOnQuantum;	
	public int timeWorkedOnQuantum{
			get{return this._timeWorkedOnQuantum;}
			set{this._timeWorkedOnQuantum = value;}
		}
		
	public int _totalContextSwitchCosts;	
	public int totalContextSwitchCosts{
			get{return this._totalContextSwitchCosts;}
			set{this._totalContextSwitchCosts = value;}
		}	
			
	}
}