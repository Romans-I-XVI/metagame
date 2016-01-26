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
				//Update game object positions
				int removed_block_position = game.block_handler.tick ();
				game.player.UpdatePosition ();

				//Draw solid game background
				Rectangle rect = new Rectangle(0, 0, Game.WIDTH, Game.HEIGHT);
				drawHandle.FillRectangle(game.white_brush, rect);

				//Draw the player rectangle
				Rectangle player_rectangle = new Rectangle (game.player.xpos, game.player.ypos, game.player.width, game.player.height);
				drawHandle.FillRectangle (game.black_brush, player_rectangle);

				//Draw letter blocks
				bool already_changed_block_color = false;
				for (int l = 0; l < game.letter_handler.array_letters.Count; l++) {
					for (int r = 0; r <= 4; r++){
						for (int c = 0; c <= 4; c++){
							Letter current_letter = game.letter_handler.array_letters [l];
							Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + c * 20)-game.player.x_offset, (game.player.ypos-20-r*20+game.letter_handler.array_letters [0].line_position*20)-l*120, 20, 20);
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

				// Remove letter from array if it has been completed
				if ((game.letter_handler.array_letters.Count > 0) && (game.letter_handler.array_letters [0].line_position == 5)) {
					game.letter_handler.completed_characters = game.letter_handler.array_letters [0].letter_string + game.letter_handler.completed_characters;
					game.letter_handler.array_letters.RemoveAt (0);
				}

				//Draw falling blocks
				foreach (var block in game.block_handler.BlockList) {
					Rectangle block_rectangle = new Rectangle(block.xpos, block.ypos, block.width, block.height);
					drawHandle.FillRectangle(game.black_brush, block_rectangle);
				}

				//Draw acquired letters text
				SizeF font_dimensions = drawHandle.MeasureString(game.letter_handler.completed_characters, game.default_font);
				RectangleF text_rectangle = new RectangleF(Game.WIDTH-font_dimensions.Width-60, Game.HEIGHT-font_dimensions.Height-60, font_dimensions.Width, font_dimensions.Height);
				drawHandle.DrawString(game.letter_handler.completed_characters, game.default_font, game.black_brush, text_rectangle);

				//Force about 60FPS
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

