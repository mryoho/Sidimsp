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
	
	//This is checked by the running threads to determine whether to stop executing
	static bool StopProcessing;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		
		//Set the console globally so that any class can output
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
			
			//Thist starts the simulation
			startSimulation();
		}else{
			StopProcessing = true;
			
			//clear the text
			console.Buffer.Clear ();
			
			//change the stop button to start
			StartButton.Label = "Start";
		}
	}
	
	protected void startSimulation(){
		
		StopProcessing = false;
		
		int nCores = 0;
		int nProcesses = 0;
		
		//wrap in try/catch so that the program doesn't crash with invalid input
		try{
		nCores = Convert.ToInt32(numCores.Text);
		nProcesses = Convert.ToInt32(numProcesses.Text);
		}catch(Exception e){
			Console.Write(e.StackTrace);
		}
		
		//if input is invalid, don't start the simulation
		if((nCores <= 0 || nCores > 64) || nProcesses <= 0){
			console.Buffer.Text += "Invalid input for # of cores or # of processes.";
		}else{
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
		}
		
		//Return the label to "Start" since processing is done.
		StartButton.Label = "Start";
	}
	
	
}