using System;
using Gtk;

namespace Sidimsp
{
	public static class GlobalVar
	{
		static readonly object _locker = new object();
	
	    private static TextView _windowConsole;
	    public static TextView WindowConsole
	    {
			private get{return _windowConsole;}
			set{_windowConsole = value;}
	    }
		public static void OutputMessage( string text ){
			lock(_locker){
			WindowConsole.Buffer.Text += ( text + Environment.NewLine);
			}
		}
	}
}

