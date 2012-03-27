using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Sidimsp
{
	public class MultiLevelFeedbackQueue
	{
		private List<ProcessQueue> _queues{ get; set;}
		
		public MultiLevelFeedbackQueue ( List<int> queueTypes, List<int> quantums)
		{
			//Stores each of the queues (PriorityQueue, RoundRobinQueue, FCFSQueue)
			_queues = new List<ProcessQueue>();
			
			// Create the multilevelfeedbackqueue based on the number of queue types provided
			for( int i = 0; i < queueTypes.Count; i++ ){
				switch( queueTypes[i] ) {
				case 0:
					_queues.Add (new PriorityQueue(quantums[i]));
					break;
				case 1:
					_queues.Add (new RoundRobinQueue(quantums[i], 2));
					break;
				case 2:
					_queues.Add (new FirstComeFirstServedQueue(quantums[i]));
					break;
				}
			}
		}
		
		// inherited methods
		public void AddProcess(Process p){
			_queues[0].AddProcess(p);
		}
		
		//Take in the current time
		public void Run(int time) {
			//GlobalVar.WindowConsole.Buffer.Text += ("MultiLevelFeedbackQueue Running " + Environment.NewLine);
			//Gtk.Application.Invoke();
			//Thread.Sleep(500);
			Console.WriteLine("MultiLevelFeedbackQueue Running");
			
			Process returnedProcess = null;
			
			//For each of the queues, check if they are empty, if not, process on that process
			for(int i = 0; i < _queues.Count; i++){
				
				
			}
		

		}
		
		// other methods
		public void AddQueue(ProcessQueue pq){
			//_queues.Add(pq);
		}
	}
}

