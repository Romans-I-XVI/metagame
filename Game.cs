﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;


namespace metagame
{
	public class Game
	{
		//----------Constants----------//
		public static readonly int HEIGHT = Screen.PrimaryScreen.WorkingArea.Height-Screen.PrimaryScreen.WorkingArea.Height/10;
		public static readonly int WIDTH = Screen.PrimaryScreen.WorkingArea.Width-Screen.PrimaryScreen.WorkingArea.Width/10;
		public static readonly int BLOCK_ZONE_LEFT = WIDTH/2-50;
		public static readonly int LOWER_LINE_POSITION = HEIGHT - 150;

		//-----------Game Objects---------//

		// Public variables to be set by constructor
		public BlockHandler block_handler;
		public LetterHandler letter_handler;
		public int spawn_interval;

		// Create public objects for the game
		public Player player = new Player ();
		public SolidBrush black_brush = new SolidBrush(Color.Black);
		public SolidBrush white_brush = new SolidBrush(Color.White);
		public SolidBrush blue_brush = new SolidBrush (Color.FromArgb(0xff, 0x26, 0xa9, 0xda));
		public SolidBrush blue_brush_2 = new SolidBrush (Color.FromArgb(0xff, 0x21, 0x94, 0xbf));
		public SolidBrush gray_brush = new SolidBrush (Color.FromArgb(0xaf, 0xe6, 0xe7, 0xe8));
		public Font game_word_font = new Font("Ariel", 40, FontStyle.Bold);
		public Font default_font = new Font("Ariel", 24, FontStyle.Bold);

		private GEngine gEngine;

		// Set changeable game properties based on given arguments
		public Game (string game_word, float block_accel, int spawn_interval)
		{
			this.letter_handler = new LetterHandler (game_word);
			this.block_handler = new BlockHandler (block_accel);
			this.spawn_interval = spawn_interval;
		}

		// Create the game engine object
		public void startGraphics(Graphics g)
		{
			gEngine = new GEngine(g);
			gEngine.init(this);
		}

		// Destroy the game engine on form close
		public void stopGame()
		{
			gEngine.stop ();
		}
	}
}

