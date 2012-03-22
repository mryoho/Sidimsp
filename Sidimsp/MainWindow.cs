using System;
using System.Threading;
using Gtk;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Sidimsp;

public partial class MainWindow : Gtk.Window
{
	
	//Holds the processes
	Queue FCFSQueue;
	
	//specifies the number of processes to be randomly generated
	int NumGeneratedThreads;
	
	//This is checked by the running threads to determine whether to stop executing
	private bool StopProcessing;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		GlobalVar.WindowConsole = console;
		Application.Init();
		Application.Run();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnAboutButtonClicked (object sender, System.EventArgs e)
	{
		MessageDialog md = new MessageDialog (this, Gtk.DialogFlags.Modal , MessageType.Info, ButtonsType.Close, "CPU Scheduler &amp; Dispatch Module Simulation Project \nCS475 - Operating Systems - Spring 2012 \nBy jblackwood12, pyoho11");
		md.Run ();
		md.Destroy ();
}

	protected void OnStartButtonClicked (object sender, System.EventArgs e)
	{;
		
		if(StartButton.Label == "Start"){
		//make sure the textviews are clear
			console.Buffer.Clear();
			
		//change the start button to a stop button
		StartButton.Label = "Stop";
			
		//disable scrolling on the textviews, scrolling while simulation is running causes application to crash
		
		startSimulation(console);
		}else{
			StopProcessing = true;
			console.Buffer.Clear ();
			//change the stop button to start
			StartButton.Label = "Start";
		}
	}
	
	protected void startSimulation(TextView console){
		
		StopProcessing = false;
		NumGeneratedThreads = 50;
		
		/*
		//randomly generate process here with different arrival times
		for(int i=1; i <= 20; i++){
			if(!StopProcessing){
				textview1_.Buffer.Text += ((i) + Environment.NewLine);
				Thread.Sleep (100);
			}
		}
		*/
		int nCores = Convert.ToInt32(numCores.Text);
		int nProcesses = Convert.ToInt32(numProcesses.Text);
		List<int> queues = new List<int>();
		queues.Add(0);
		queues.Add(1);
		queues.Add(2);
		List<int> quantums = new List<int>();
		quantums.Add(8);
		quantums.Add(16);
		quantums.Add(0);
		Processor processor = new Processor(nCores, nProcesses, 50, 0, 50, 0, 9, queues, quantums);
		processor.StartSimulation();
		
		//Return the label to "Start" since processing is done.
		StartButton.Label = "Start";
	}
	
	
}