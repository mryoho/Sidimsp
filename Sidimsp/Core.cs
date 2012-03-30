using System;
using System.Collections.Generic;
using System.Threading;

//using MonoDevelop.Core.Services;
//using MonoDevelop.Services;
//using MonoDevelop.Core.Properties;

using Gtk;


namespace Sidimsp
{
	public class Core
	{
		public Core (int coreNumber, List<int> queueTypes, List<int> quantums)
		{
			processingTimeRemaining = 0;
			_processQueue = new MultiLevelFeedbackQueue( queueTypes, quantums );
			_coreNumber = coreNumber;
			
		}
		
		private MultiLevelFeedbackQueue _processQueue;
		public MultiLevelFeedbackQueue ProcessQueue{
			get{return this._processQueue;}
			set{this._processQueue = value;}
		}
		
		//This stores the total amount of processingTimeRemaining
		//Increase when adding a process to this core
		//Decrement when doing work on the processes
		public int processingTimeRemaining;
		
		private int _coreNumber;						// private variable so the core can identify itself
		private int _totalTime;							// total time elapsed on processes worked on by core
		public int TotalTime{
			get{return this._totalTime;}
		}
		
		
		// methods
		
		// right now all this is supposed to do is write a message to the GUI console and then run the actions on
		// the multiLevelFeedback queue. The problem is that I can't get the GUI to update from inside this thread.
		// I have tried methods from the following websites:
		// http://eric.extremeboredom.net/2004/12/25/113
		// http://www.mono-project.com/Responsive_Applications
		// Note: The GuiDispatch apparently only works for Mono add-ins
		// Note!! The logic for moving through processes DOES NOT GO HERE...it goes in the MultiLevelFeedbackQueue class's
		//  run method.
		public void DoWork() {
			//Continue to do work while the Processor desires
			while(!Processor.stopProcessing){
				
				//Wait for the processor to tell us to do work
				Processor.manualEvent.WaitOne();
				
				//Do Work ****HERE****
					//GlobalVar.OutputMessage ("we are in core" + this._coreNumber + " and systemTime is: " + Processor.systemTime);
					GlobalVar.OutputMessage ("we are in core" + this._coreNumber + " and systemTime is: " + Processor.systemTime);
				
					//If we successfully performed work on a process, then decrement this Core's "processingTimeRemaining"
					//This is assuming that there is not cost associated with context switches
				
					//if(_processQueue.Run ()){
					//	processingTimeRemaining--;
					//}
				//Do Work ****HERE****
				
				//Tell the processor that this core is finished processing
				Processor.SetFinished(_coreNumber);
				
				//Wait for the Processor to be finished working on all of the cores
				Processor.manualEvent2.WaitOne();
			}

			Processor.SetFinished(_coreNumber);
			
		}
		
		public Boolean isFinishedProcessing(){
			if(_processQueue.isFinishedProcessing()){
				return true;	
			}else{
				return false;
			}
			
		}
		

	}
}

