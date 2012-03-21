using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class Processor
	{
		public Processor (int numCores, int numProcesses, int maxCpuBurstTimeRemaining, 
			int minCpuBurstTimeRemaing, int maxIOBurstTimeRemaining, int minIOBurstTimeRemaining,
			int arrivalTimeRange, List<int> queueTypes, List<int> quantums)
		{
			_coreList = new List<Core>();
			_numCores = numCores;
			_numProcesses = numProcesses;
			_queueTypes = queueTypes;
			_quantums = quantums;
			_maxCpuBurstTimeRemaining = maxCpuBurstTimeRemaining;
			_minCpuBurstTimeRemaining = minCpuBurstTimeRemaing;
			_maxIOBurstTimeRemaining = maxIOBurstTimeRemaining;
			_minIOBurstTimeRemaining = minIOBurstTimeRemaining;
			_arrivalTimeRange = arrivalTimeRange;
		}
		
		private List<Core> _coreList;
		private int _numCores;
		private int _numProcesses;
		private int _maxCpuBurstTimeRemaining;
		private int _minCpuBurstTimeRemaining;
		private int _maxIOBurstTimeRemaining;
		private int _minIOBurstTimeRemaining;
		private int _simulationLength;
		private int _arrivalTimeRange;
		private List<int> _queueTypes;
		private List<int> _quantums;
		
		public void StartSimulation() {
			// generate cores
			for(int i = 0; i < _numCores; i++) {
				Core newCore = new Core(_queueTypes, _quantums);
				_coreList.Add( newCore );
			}
			
			int selectedCore = 0;
			int cpuBurstTime;
			int ioBurstTime;
			int pid;
			int priority;
			int arrivalTime = 0;
			int lastArrivalTime;
			string processState = "Waiting";
			Random random = new Random();
			Process p;				// reference to process objects created below
			// generate processes
			for(int i = 0; i <_numProcesses; i++) {
				selectedCore = i % _numCores;		// every time a process is generated, assign it to a different core
				cpuBurstTime = random.Next( _minCpuBurstTimeRemaining, _maxCpuBurstTimeRemaining );
				ioBurstTime = random.Next( _minIOBurstTimeRemaining, _minIOBurstTimeRemaining );
				pid = i;
				priority = random.Next(0,9);
				lastArrivalTime = arrivalTime;
				arrivalTime = lastArrivalTime + random.Next(0, _arrivalTimeRange);
				
				p = new Process(pid, priority, arrivalTime, processState, cpuBurstTime, ioBurstTime);
				
				//_coreList[selectedCore]
			}
		}
		
		public void LoadBalance() {
			
		}
	}
}

