using System;
using System.Collections.Generic;

namespace metagame
{
	public class LetterHandler
	{
		public string game_word;
		public string completed_characters;
		public char[] game_characters;
		public List<Letter> array_letters = new List<Letter> ();

		public LetterHandler (string game_word)
		{
			this.game_word = game_word.ToLower();
			this.game_characters = this.game_word.ToCharArray();
			this.SetLetters ();
		}

		public void SetLetters()
		{
			//Remove all objects in array before adding new
			for (int i = 0; i < this.array_letters.Count; i++) {
				this.array_letters.RemoveAt (i);
			}

			//Create new letter objects for each char in the game word
			for (int i = this.game_characters.Length - 1; i >= 0; i--){
				Console.WriteLine (this.game_characters [i]);
				if (Char.IsLetter (this.game_characters [i])) {
					this.array_letters.Add (new Letter (this.game_characters [i]));
				}
			}
		}

		public void check_removed_block_position (int removed_block_position, int player_position)
		{
			if (this.array_letters.Count > 0) {
				bool need_to_reset_line = true;
				Letter active_letter = this.array_letters [0];
				for (int c = 0; c <= 4; c++) {
					if ((removed_block_position == c + player_position) && (active_letter.letter_positions [active_letter.line_position, c] == 1)) {
						need_to_reset_line = false;
						active_letter.letter_positions [active_letter.line_position, c] = 2;
						active_letter.update_line_position ();
					}
				}

				if (need_to_reset_line) {
					active_letter.reset_active_line ();
				}
					
			}
		}
	}
}

