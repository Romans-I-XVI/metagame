using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace metagame
{
	public class GEngine
	{
		private Graphics drawHandle;
		private Thread renderThread;
		public Game game;

		public GEngine(Graphics g)
		{
			drawHandle = g;
		}

		public void init(Game game)
		{
			this.game = game;
			renderThread = new Thread (new ThreadStart (render));
			renderThread.Start ();
		}

		public void stop()
		{
			renderThread.Abort ();
		}

		private void render()
		{
			int framesRendered = 0;
			long startTime = Environment.TickCount;
			Letter letter = new Letter ("d");
			while (true) 
			{
				Rectangle rect = new Rectangle(0, 0, Game.WIDTH, Game.HEIGHT);
				drawHandle.FillRectangle(game.white_brush, rect);
				int removed_block_position = game.block_handler.tick ();
				game.player.UpdatePosition ();
				Rectangle player_rectangle = new Rectangle (game.player.xpos, game.player.ypos, game.player.width, game.player.height);
				drawHandle.FillRectangle (game.black_brush, player_rectangle);

				//Draw falling blocks
				foreach (var block in game.block_handler.BlockList) {
					Rectangle block_rectangle = new Rectangle(block.xpos, block.ypos, block.width, block.height);
					drawHandle.FillRectangle(game.black_brush, block_rectangle);
				}

				//Draw letter outlines
				for (int i = 0; i < 5; i++)
				{
					if (letter.letter_positions [0, i] == 1) {
						Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + i * 20) - game.player.x_offset, game.player.ypos - 20, 20, 20);
						drawHandle.FillRectangle (game.blue_brush, letter_block);
						if (removed_block_position == i)
							letter.letter_positions [0, i] = 2;
					}
					else if (letter.letter_positions[0,i] == 2) {
						Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + i * 20) - game.player.x_offset, game.player.ypos - 20, 20, 20);
						drawHandle.FillRectangle (game.black_brush, letter_block);
					}
				}
						


				Thread.Sleep (17);

				//Benchmarking
				framesRendered++;
				if (Environment.TickCount >= startTime + 1000) {
					Console.WriteLine ("GEngine: " + framesRendered + " fps");
					framesRendered = 0;
					startTime = Environment.TickCount;
				}
			}	
		}
	}
}

