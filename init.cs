using System;
using System.Windows.Forms;

namespace testform
{
	public class MainClass
	{
		[STAThread]
		public static void Main()
		{
			Application.Run(new MainForm(600, 600, 5));
		}
	}
}
