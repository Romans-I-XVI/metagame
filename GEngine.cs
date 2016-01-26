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
		private Game game;

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
			while (true) 
			{
				Rectangle rect = new Rectangle(0, 0, Game.WIDTH, Game.HEIGHT);
				drawHandle.FillRectangle(game.white_brush, rect);
				int removed_block_position = game.block_handler.tick ();
				game.player.UpdatePosition ();
				Rectangle player_rectangle = new Rectangle (game.player.xpos, game.player.ypos, game.player.width, game.player.height);
				drawHandle.FillRectangle (game.black_brush, player_rectangle);


				bool already_changed_block_color = false;
				for (int l = 0; l < game.letter_handler.array_letters.Count - 1; l++) {
					for (int r = 0; r < 4; r++){
						for (int c = 0; c < 4; c++){
							Letter current_letter = game.letter_handler.array_letters [l];
							Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + c * 20)-game.player.x_offset, (game.player.ypos-20-r*20+current_letter.line_position*20)-l*100, 20, 20);
							if (current_letter.letter_positions[r,c] == 1) {
								drawHandle.FillRectangle (game.blue_brush, letter_block);
									if ((removed_block_position == c + game.player.position) && (r == current_letter.line_position) && (l == 0) && (already_changed_block_color == false)) {
										current_letter.letter_positions [r, c] = 2;
										current_letter.update_line_position ();
										already_changed_block_color = true;
								}
							}
							else if (current_letter.letter_positions[r,c] == 2) {
								drawHandle.FillRectangle (game.black_brush, letter_block);
							}
						}
					}
				}
				if (game.letter_handler.array_letters [0].line_position == 4)
					game.letter_handler.array_letters.RemoveAt (0);







//				//Draw letter outlines
//				for (int i = 0; i < 5; i++)
//				{
//					if (letter.letter_positions [0, i] == 1) {
//						Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + i * 20) - game.player.x_offset, game.player.ypos - 20, 20, 20);
//						drawHandle.FillRectangle (game.blue_brush, letter_block);
//						Console.WriteLine (removed_block_position);
//						Console.WriteLine(i+game.player.position);
//						if (removed_block_position == i+game.player.position)
//							letter.letter_positions [0, i] = 2;
//					}
//					else if (letter.letter_positions[0,i] == 2) {
//						Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + i * 20) - game.player.x_offset, game.player.ypos - 20, 20, 20);
//						drawHandle.FillRectangle (game.black_brush, letter_block);
//					}
//				}

				//Draw falling blocks
				foreach (var block in game.block_handler.BlockList) {
					Rectangle block_rectangle = new Rectangle(block.xpos, block.ypos, block.width, block.height);
					drawHandle.FillRectangle(game.black_brush, block_rectangle);
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

