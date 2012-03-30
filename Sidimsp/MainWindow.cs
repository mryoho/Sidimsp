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
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{			
		Build ();
		
		// Add ComboBox Items
		numCores.AppendText("1");
		numCores.AppendText("2");
		numCores.AppendText("4");
		numCores.AppendText("8");
		numCores.AppendText("16");
		numCores.AppendText("32");
		numCores.AppendText("64");
		numCores.Active = 2;
		
		queue1Type.AppendText("First Come First Serve");
		queue1Type.AppendText("Priority");
		queue1Type.AppendText("Round Robin");
		queue1Type.AppendText("None");
		queue1Type.Active = 1;
		
		queue2Type.AppendText("First Come First Serve");
		queue2Type.AppendText("Priority");
		queue2Type.AppendText("Round Robin");
		queue2Type.AppendText("None");
		queue2Type.Active = 2;
		
		queue3Type.AppendText("First Come First Serve");
		queue3Type.AppendText("Priority");
		queue3Type.AppendText("Round Robin");
		queue3Type.AppendText("None");
		queue3Type.Active = 0;
		
		queue4Type.AppendText("First Come First Serve");
		queue4Type.AppendText("Priority");
		queue4Type.AppendText("Round Robin");
		queue4Type.AppendText("None");
		queue4Type.Active = 3;
		
		// Add default values to Quantums and such
		quantum1.Text = "8";
		quantum2.Text = "16";
		quantum3.Text = "0";
		
		//Set the console globally so that any class can output
		GlobalVar.WindowConsole = console;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	/*
	protected void OnAboutButtonClicked (object sender, System.EventArgs e)
	{
		MessageDialog md = new MessageDialog (this, Gtk.DialogFlags.Modal , MessageType.Info, ButtonsType.Close, "CPU Scheduler &amp; Dispatch Module Simulation Project \nCS475 - Operating Systems - Spring 2012 \nBy jblackwood12, pyoho11");
		md.Run ();
		md.Destroy ();
	}
	*/

	protected void OnStartButtonClicked (object sender, System.EventArgs e)
	{
		
		if(StartButton.Label == "Start Simulation"){
			//make sure the textviews are clear
			console.Buffer.Clear();
			
			//change the start button to a stop button
			StartButton.Label = "Stop Simulation";
			
			//Thist starts the simulation
			startSimulation();
		}else{			
			//clear the text
			console.Buffer.Clear ();
			
			//change the stop button to start
			StartButton.Label = "Start";
		}
	}
	
	protected void startSimulation(){
		
		int nCores = 0;
		int nProcesses = 0;
		
		//wrap in try/catch so that the program doesn't crash with invalid input
		try{
			nCores = Convert.ToInt32(numCores.ActiveText);
			nProcesses = Convert.ToInt32(numProcesses.Text);
		}catch(Exception e){
			Console.Write(e.StackTrace);
		}
		
		//if input is invalid, don't start the simulation
		if((nCores <= 0 || nCores > 64) || nProcesses <= 0){
			console.Buffer.Text += "Invalid input for # of cores or # of processes.";
		}else{
			List<int> queues = new List<int>();
			if(queue1Type.Active != 3 && queue1Type.Active != -1)
				queues.Add(queue1Type.Active);
			if(queue2Type.Active != 3 && queue2Type.Active != -1)
				queues.Add(queue2Type.Active);
			if(queue3Type.Active != 3 && queue3Type.Active != -1)
				queues.Add(queue3Type.Active);
			if(queue4Type.Active != 3 && queue4Type.Active != -1)
				queues.Add(queue4Type.Active);
			List<int> quantums = new List<int>();
			if(quantum1.Text != "")
				quantums.Add(Convert.ToInt32(quantum1.Text));
			if(quantum2.Text != "")
				quantums.Add(Convert.ToInt32(quantum2.Text));
			if(quantum3.Text != "")
				quantums.Add(Convert.ToInt32(quantum3.Text));
			if(quantum4.Text != "")
				quantums.Add(Convert.ToInt32(quantum4.Text));
			Processor processor = new Processor(nCores, nProcesses, 50, 0, 50, 0, 9, queues, quantums);
			processor.StartSimulation();	
		}
		
		//Return the label to "Start" since processing is done.
		StartButton.Label = "Start Simulation";
	}
	
	
}