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
	{
		textview1.Buffer.Text += "Simulation started!\n";
		ThreadPool.QueueUserWorkItem(startSimulation);
	}
	
	protected void OnStopButtonClicked (object sender, System.EventArgs e)
	{
		StopProcessing = true;
		textview1.Buffer.Clear ();
		textview1.Buffer.Text += "Simulation stopped!\n";
	}
	
	protected void startSimulation(object ob){
		
		StopProcessing = false;
		NumGeneratedThreads = 50;
		
		//randomly generate process here with different arrival times
		for(int i=1; i <= 20; i++){
			if(!StopProcessing){
				textview1.Buffer.Text += ((i) + Environment.NewLine);
				Thread.Sleep (100);
			}
		}
	}
	
	protected void startFCFS(object ob){
			
		
	}
	
}