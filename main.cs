using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace testform
{
	public class MainForm : Form
	{
		int form_width;
		int form_height;
		BlockHandler block_handler;
		SolidBrush black_brush = new SolidBrush(Color.Black);
		SolidBrush white_brush = new SolidBrush(Color.White);

		public MainForm(int form_width, int form_height, int block_speed)
		{	
			this.form_width = form_width;
			this.form_height = form_height;
			this.MinimumSize = new Size(this.form_width, this.form_height);
			this.MaximumSize = new Size(this.form_width, this.form_height);
			this.block_handler = new BlockHandler (form_width, form_height, block_speed);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics ;
			Rectangle rect = new Rectangle(0, 0, this.form_width, this.form_height);
			g.FillRectangle(this.white_brush, rect);
			this.block_handler.tick ();
			foreach (var block in this.block_handler.BlockList) {
				Rectangle block_rectangle = new Rectangle(block.xpos, block.ypos, block.width, block.height);
				g.FillRectangle(this.black_brush, block_rectangle);
			}
			Thread.Sleep (17);
			this.Refresh ();
		} 

//		void OnClick(object sender, System.EventArgs e)
//		{
//			if (DialogResult.OK == colorDialog.ShowDialog())
//				Console.WriteLine(colorDialog.Color);
//		}
	}

	public class BlockHandler
	{
		Random rnd = new Random();
		int ticker = 0;
		int form_width;
		int form_height;
		int block_speed;
		public List<Block> BlockList = new List<Block>();

		public BlockHandler(int form_width, int form_height, int block_speed)
		{
			this.form_width = form_width;
			this.form_height = form_height;
			this.block_speed = block_speed;
		}

		public void tick()
		{
			this.ticker += 1;

			for (int i = BlockList.Count - 1; i >= 0; i--)
			{
				BlockList[i].ypos += BlockList[i].speed;
				if (BlockList[i].ypos > this.form_height)
					BlockList.RemoveAt(i);
			}

			if (this.ticker >= 60) 
			{
				Block block = new Block (this.form_width/2-50+rnd.Next(5)*20, 0, 20, 20, this.block_speed);
				this.BlockList.Add (block);
				this.ticker = 0;
			}
		}
	}

	public class Block
	{
		public int xpos;
		public int ypos;
		public int width;
		public int height;
		public int speed;

		public Block(int xpos, int ypos, int width, int height, int speed)
		{
			this.xpos = xpos;
			this.ypos = ypos;
			this.width = width;
			this.height = height;
			this.speed = speed;
		}
	}
}

