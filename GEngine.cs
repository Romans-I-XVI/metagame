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
			int pt = 0;
			long startTime = Environment.TickCount;
			while (true) 
			{
				//This will limit the game to 60 FPS, the game uses frame based movement.
				//I know from a game development standpoint this is not good practice, but that's not the point of this project...
				if (Environment.TickCount >= pt+17)
				{
					pt = Environment.TickCount;

					//Update game object positions
					int? removed_block_position = game.block_handler.tick ();
					if (removed_block_position.HasValue)
						game.letter_handler.check_removed_block_position (removed_block_position.Value, game.player.position);
					game.player.UpdatePosition ();

					//Draw solid game background
					Rectangle rect = new Rectangle(0, 0, Game.WIDTH, Game.HEIGHT);
					drawHandle.FillRectangle(game.white_brush, rect);

					//Draw lower line
					Rectangle lower_line = new Rectangle(0, Game.LOWER_LINE_POSITION, Game.WIDTH, 150);
					drawHandle.FillRectangle(game.gray_brush, lower_line);


					//Draw the player rectangle
					Rectangle player_rectangle = new Rectangle (game.player.xpos, game.player.ypos, game.player.width, game.player.height);
					drawHandle.FillRectangle (game.black_brush, player_rectangle);

					//Draw letter blocks and change letter blocks if there was a collision
					for (int l = 0; l < game.letter_handler.array_letters.Count; l++) {
						for (int r = 0; r <= 4; r++){
							for (int c = 0; c <= 4; c++){
								Letter current_letter = game.letter_handler.array_letters [l];
								if (r >= current_letter.line_position) 
								{
									Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + c * 20)-game.player.x_offset, (game.player.ypos-20-r*20+game.letter_handler.array_letters [0].line_position*20)-l*120, 20, 20);
									if (current_letter.letter_positions [r, c] == 1) {
										drawHandle.FillRectangle (game.gray_brush, letter_block);
									} else if (current_letter.letter_positions [r, c] == 2) {
										drawHandle.FillRectangle (game.black_brush, letter_block);
									}
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
}

