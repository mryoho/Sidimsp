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
			
	}
}

