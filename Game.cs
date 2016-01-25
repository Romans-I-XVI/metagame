using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
namespace metagame
{
	public class Game
	{
		//----------Constants----------//
		public const int HEIGHT = 600;
		public const int WIDTH = 600;
		public const int BLOCK_ZONE_LEFT = WIDTH/2-50;

		//-----------Game Objects---------//
		public BlockHandler block_handler = new BlockHandler (5);
		public Player player = new Player ();
		public SolidBrush black_brush = new SolidBrush(Color.Black);
		public SolidBrush white_brush = new SolidBrush(Color.White);
		public SolidBrush blue_brush = new SolidBrush (Color.Blue);

		private GEngine gEngine;

		public void startGraphics(Graphics g)
		{
			gEngine = new GEngine(g);
			gEngine.init(this);
		}

		public void stopGame()
		{
			gEngine.stop ();
		}
	}
}

