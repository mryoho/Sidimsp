using System;
using Gtk;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

public partial class MainWindow : Gtk.Window
{
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
	}
	
	
	protected void OnStopButtonClicked (object sender, System.EventArgs e)
	{
		textview1.Buffer.Text += "Simulation stopped!\n";
	}
}