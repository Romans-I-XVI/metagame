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
		private Game game = new Game();

		public MainForm(int block_speed)
		{	
			this.MinimumSize = new Size(Game.WIDTH, Game.HEIGHT);
			this.MaximumSize = new Size(Game.WIDTH, Game.HEIGHT);
			Graphics g = CreateGraphics ();
			game.startGraphics (g);
		}

		private void MainForm_FormClosing(object sender, FormClosedEventArgs e)
		{
			game.stopGame ();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((keyData == Keys.Left)&&(game.player.position > -4))
			{
				game.player.position--;
				return true;
			}
			//capture right arrow key
			if ((keyData == Keys.Right)&&(game.player.position < 4) )
			{
				game.player.position++;
				return true;
			}
			return false;
		}
	}




}

