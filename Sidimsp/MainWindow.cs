using System;
using System.Threading;
using Gtk;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
			textview1.Buffer.Clear();
			textview2.Buffer.Clear();
			textview3.Buffer.Clear();
			
		//change the start button to a stop button
		StartButton.Label = "Stop";
			
		//disable scrolling on the textviews, scrolling while simulation is running causes application to crash
		
		startSimulation();
		}else{
			StopProcessing = true;
			textview1.Buffer.Clear ();
			//change the stop button to start
			StartButton.Label = "Start";
		}
	}
	
	protected void startSimulation(){
		
		StopProcessing = false;
		NumGeneratedThreads = 50;
		
		//randomly generate process here with different arrival times
		for(int i=1; i <= 20; i++){
			if(!StopProcessing){
				textview1.Buffer.Text += ((i) + Environment.NewLine);
				textview2.Buffer.Text += ((i) + Environment.NewLine);
				textview3.Buffer.Text += ((i) + Environment.NewLine);
				Thread.Sleep (100);
			}
		}
		
		//Return the label to "Start" since processing is done.
		StartButton.Label = "Start";
	}
	
	protected void startRoundRobin(object ob){
			
	}
	
	protected void startPriority(object ob){
		
	}
	
	protected void startFCFS(object ob){
		
	}
	
	
}