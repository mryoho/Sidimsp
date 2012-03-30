using System;
using Gtk;
using System.Collections.Generic;

namespace Sidimsp
{
	public static class GlobalVar
	{
		private static List<String> theMessages = new List<String>();
		
		static readonly object _locker = new object();
	
	    private static TextView _windowConsole;
	    public static TextView WindowConsole
	    {
			private get{return _windowConsole;}
			set{_windowConsole = value;}
	    }
		
		public static void OutputMessage( string text ){
			lock(_locker){
				//save the text message, to be displayed later
				theMessages.Add(text);
				
				//display the text message in the console
				Console.WriteLine (text);
			}
		}
		
		public static void PrintMessages(){
			//print all of the messages
			for(int i = 0; i < theMessages.Count; i++){
				_windowConsole.Buffer.Text += (theMessages[i] + Environment.NewLine);
			}
			
			//delete all of the messages
			theMessages.Clear();
			    
		}
	}
}

