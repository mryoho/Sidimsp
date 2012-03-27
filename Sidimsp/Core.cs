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
			_processQueue = new MultiLevelFeedbackQueue( queueTypes, quantums );
			_coreNumber = coreNumber;
			
		}
		
		private MultiLevelFeedbackQueue _processQueue;
		public MultiLevelFeedbackQueue ProcessQueue{
			get{return this._processQueue;}
			set{this._processQueue = value;}
		}
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
			//Wait for the Processor to tell us that we are ready to process
			while(true){
				if(Processor.systemTime > 3) break;
				
				//Wait for the processor to tell us to do work
				Processor.manualEvent.WaitOne();
				
				//Do Work
				GlobalVar.OutputMessage ("we are in core" + this._coreNumber + " and systemTime is: " + Processor.systemTime);
				Console.WriteLine ("we are in core" + this._coreNumber + " and systemTime is: " + Processor.systemTime);
				//_processQueue.Run(Processor.systemTime);
				Processor.SetFinished(_coreNumber);
				
				//Spin while we wait for the Processor to be reset
				//THIS NEEDS TO BE IMPROVED, MAY NOT ALWAYS WORK
				Thread.Sleep (100);

			}
			Processor.SetFinished(_coreNumber);
			
		}

	}
}

