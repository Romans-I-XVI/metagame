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
		int form_width;
		int form_height;

		public MainForm(int form_width, int form_height, int block_speed)
		{	
			this.form_width = form_width;
			this.form_height = form_height;
			this.MinimumSize = new Size(this.form_width, this.form_height);
			this.MaximumSize = new Size(this.form_width, this.form_height);
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

