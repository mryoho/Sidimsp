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
		// the multiLevelFeedback queue. The problem is that I can't get the GUI to update from inisde this thread.
		// I have tried methods from the following websites:
		// http://eric.extremeboredom.net/2004/12/25/113
		// http://www.mono-project.com/Responsive_Applications
		// Note: The GuiDispatch apparently only works for Mono add-ins
		// Note!! The logic for moving through processes DOES NOT GO HERE...it goes in the MultiLevelFeedbackQueue class's
		//  run method.
		public void DoWork() {
			//Console.WriteLine( "In Core " + _coreNumber );
			//GlobalVar.WindowConsole.Buffer.Text += ("Core " + _coreNumber + " Active " + Environment.NewLine + "  ");
			//Gtk.Application.Invoke(delegate( GlobalVar.WindowConsole.Buffer.Text += ("Core " + _coreNumber + " Active " + Environment.NewLine + "  "));
			//Runtime.DispatchService.GuiDispatch( GlobalVar.WindowConsole.Buffer.Text += ("Core " + _coreNumber + " Active " + Environment.NewLine + "  ") ); // (new StatefulMessageHandler (UpdateGui), n);
			/*
			Gtk.Application.Invoke(delegate {
				GlobalVar.WindowConsole.Buffer.Text += ("Core " + _coreNumber + " Active " + Environment.NewLine + "  ");
				//GlobalVar.WindowConsole.
			});
			*/
			//RunOnMainThread.Run(this, "OutputMessage", new object[] { "Core " + _coreNumber + " Active" });
			GlobalVar.OutputMessage ("we are in core" + this._coreNumber);
			Thread.Sleep (1000);
			//for(int i = 0; i < 5; i++){
				//Thread.Sleep (100);
				//OutputMessage ("we are in core" + this._coreNumber);
			//	_totalTime = _processQueue.Run();
			//}
			//Thread.Sleep(1);
		}

	}
}

