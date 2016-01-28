using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace metagame
{
	public class MainForm : Form
	{
		private Game game;

		// Set form size limits and create a new game instance
		public MainForm(string game_word, float block_accel, int spawn_interval)
		{	
			this.MinimumSize = new Size(Game.WIDTH, Game.HEIGHT);
			this.MaximumSize = new Size(Game.WIDTH, Game.HEIGHT);
			this.game = new Game(game_word, block_accel, spawn_interval);
			Graphics g = CreateGraphics ();
			game.startGraphics (g);
		}

		// Stop the main game script execution if the form is closed
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Console.WriteLine ("Main game form has been closed");
			game.stopGame ();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// Handle left arrow key
			if ((keyData == Keys.Left)&&(game.player.position > -4))
			{
				game.player.position--;
			}

			// Handle right arrow key
			if ((keyData == Keys.Right)&&(game.player.position < 4) )
			{
				game.player.position++;
			}
			return true;
		}
	}




}

