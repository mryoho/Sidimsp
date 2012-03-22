using System;
using Gtk;

namespace Sidimsp
{
	public static class GlobalVar
	{
	
	    private static TextView _windowConsole;
	    public static TextView WindowConsole
	    {
			get{return _windowConsole;}
			set{_windowConsole = value;}
	    }
	}
}

