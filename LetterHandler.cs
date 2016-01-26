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
	}
}

