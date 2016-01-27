using System;
using System.Windows.Forms;

namespace metagame
{
	public class MainClass
	{
		[STAThread]
		public static void Main(string[] args)
		{	
			string game_word = "metacommunications";
			float block_accel = 10;
			int spawn_interval = 60;

			switch (args.Length) 
			{
			case 3:
				try {
					spawn_interval = int.Parse(args[2]);
				}
				catch {
					Console.WriteLine ("Non-integer argument entered, using default spawn interval");
				}
				goto case 2;
			case 2:
				try {
					block_accel = float.Parse(args[1]);
				}
				catch {
					Console.WriteLine ("Non-float argument entered, using default block acceleration");
				}
				goto case 1;
			case 1:
				game_word = args [0];
				break;	
			default:
				break;
			
			}

			block_accel = block_accel / 100;
				
			Application.Run(new MainForm(game_word, block_accel, spawn_interval));
		}
	}
}
