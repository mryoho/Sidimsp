using System;
using System.Collections;
using System.Collections.Generic;

namespace Sidimsp
{
	public class MultiLevelFeedbackQueue : ProcessQueue
	{
		public MultiLevelFeedbackQueue ( List<int> queueTypes, List<int> quantums)
		{
			// initialize member variables
			_queues = new List<Pair<ProcessQueue, int>>();
			
			// Create the multilevelfeedbackqueues based on the number of queue types provided
			Pair< ProcessQueue, int > newPair; 			// reference used to create new pairs
			for( int i = 0; i < queueTypes.Count; i++ ){
				switch( queueTypes[i] ) {
				case 0:
					FirstComeFirstServedQueue newFCFS = new FirstComeFirstServedQueue();
					newPair = new Pair<ProcessQueue, int>(newFCFS, quantums[i]);
					_queues.Add( newPair );
					break;
				case 1:
					PriorityQueue newPQ = new PriorityQueue();
					newPair = new Pair<ProcessQueue, int>(newPQ, quantums[i]);
					_queues.Add( newPair );
					break;
				case 2:
					RoundRobinQueue newRRQ = new RoundRobinQueue();
					newPair = new Pair<ProcessQueue, int>(newRRQ, quantums[i]);
					_queues.Add( newPair );
					break;
				}
			}
		}
		
		private List< Pair< ProcessQueue, int > > _queues{ get; set;}
		
		// inherited methods
		public override void AddProcess(Process p){
			//queues[0].GetType
		}
		
		public override void RemoveProcess(Process p){}
		
		public override void Run() {}
		
		// other methods
		public void AddQueue(ProcessQueue pq){
			//_queues.Add(pq);
		}
		public void RemoveQueue(ProcessQueue pq){}
	}
}

