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

		// Set Graphics object from form window
		public GEngine(Graphics g)
		{
			drawHandle = g;
		}

		// Start a new gameplay rendering thread, 
		public void init(Game game)
		{
			this.game = game;
			renderThread = new Thread (new ThreadStart (render));
			renderThread.Start ();
		}

		// End the rendering Thread object on form close
		public void stop()
		{
			renderThread.Abort ();
		}

		// This is the main game loop, which is running on a thread separate from the Form
		// This is where the magic happens
		private void render()
		{
			// This is from frame timing and benchmarking
			long startTime = Environment.TickCount;

			// We will draw to this Bitmap first, and then draw the entire bitmap to the Form as a single frame
			Bitmap frame = new Bitmap (Game.WIDTH, Game.HEIGHT); 
			Graphics frameGraphics = Graphics.FromImage (frame);

			bool need_frame = true;
			while (true) 
			{
				// Handle the game objects updating and drawing to Bitmap during the offcycle (in between draws to the form)
				if (need_frame)
				{
					//Update game object positions
					int block_spawn_interval = game.spawn_interval;
					if (game.letter_handler.array_letters.Count == 0)
						block_spawn_interval = 10;
					int? removed_block_position = game.block_handler.tick (block_spawn_interval);
					if (removed_block_position.HasValue)
						game.letter_handler.check_removed_block_position (removed_block_position.Value, game.player.position);
					game.player.UpdatePosition ();

					//Draw solid game background
					Rectangle rect = new Rectangle(0, 0, Game.WIDTH, Game.HEIGHT);
					frameGraphics.FillRectangle(game.blue_brush, rect);

					//Draw lower portion
					Rectangle lower_rectangle = new Rectangle(0, Game.LOWER_LINE_POSITION, Game.WIDTH, 150);
					frameGraphics.FillRectangle(game.blue_brush_2, lower_rectangle);


					//Draw the player rectangle
//					Rectangle player_rectangle = new Rectangle (game.player.xpos, game.player.ypos, game.player.width, game.player.height);
//					frameGraphics.FillRectangle (game.white_brush, player_rectangle);

					//Draw letter blocks and change letter blocks if there was a collision
					for (int l = 0; l < game.letter_handler.array_letters.Count; l++) {
						for (int r = 0; r <= 4; r++){
							for (int c = 0; c <= 4; c++){
								Letter current_letter = game.letter_handler.array_letters [l];
								if (r >= current_letter.line_position) 
								{
									Rectangle letter_block = new Rectangle ((Game.BLOCK_ZONE_LEFT + c * 20)-game.player.offset, (game.player.ypos-20-r*20+game.letter_handler.array_letters [0].line_position*20)-l*120, 20, 20);
									if (current_letter.letter_positions [r, c] == 1) {
										frameGraphics.FillRectangle (game.gray_brush, letter_block);
									} else if (current_letter.letter_positions [r, c] == 2) {
										frameGraphics.FillRectangle (game.white_brush, letter_block);
									}
								}
							}
						}
					}

					// Remove letter from array if it has been completed
					if ((game.letter_handler.array_letters.Count > 0) && (game.letter_handler.array_letters [0].line_position == 5)) {
						game.letter_handler.completed_characters += game.letter_handler.array_letters [0].letter_string.ToUpper();
						game.letter_handler.array_letters.RemoveAt (0);
					}

					//Draw falling blocks
					foreach (var block in game.block_handler.BlockList) {
						Rectangle block_rectangle = new Rectangle(block.xpos, block.ypos, block.width, block.height);
						frameGraphics.FillRectangle(game.white_brush, block_rectangle);
					}

					//Draw acquired letters text
					SizeF font_dimensions = frameGraphics.MeasureString(game.letter_handler.completed_characters, game.game_word_font);
					RectangleF text_rectangle = new RectangleF(Game.WIDTH/2-font_dimensions.Width/2, Game.HEIGHT-font_dimensions.Height-60, font_dimensions.Width, font_dimensions.Height);
					frameGraphics.DrawString(game.letter_handler.completed_characters, game.game_word_font, game.white_brush, text_rectangle);

					//Draw winning text
					if (game.letter_handler.array_letters.Count == 0) {
						SizeF winning_font_dimensions = frameGraphics.MeasureString ((string)"You Win!", game.game_word_font);
						RectangleF winning_text_rectangle = new RectangleF (Game.WIDTH / 2 - winning_font_dimensions.Width / 2, Game.HEIGHT / 2 - winning_font_dimensions.Height / 2, winning_font_dimensions.Width, winning_font_dimensions.Height);
						frameGraphics.DrawString ("You Win!", game.game_word_font, game.white_brush, winning_text_rectangle);
					}

					//Make it so only one new frame is created in the offcycle
					need_frame = false;
				}

				//This will limit the game to 60 FPS, the game uses frame based movement.
				//I know from a game development standpoint this is not good practice, but that's not the point of this project...
				if (Environment.TickCount >= startTime+17) 
				{
					//Draw final frame to the screen as a single bitmap
					//Make sure this remains the first line executed within this 'if' statement in order to get a smooth framerate
					drawHandle.DrawImage(frame, 0, 0);

					// Make it so a new frame is created in between now and 17 milliseconds from now
					need_frame = true;
					startTime = Environment.TickCount;
				}
			}	
		}
	}
}

