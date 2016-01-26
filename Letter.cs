using System;

namespace metagame
{
	public class Letter
	{
		public int line_position = 0;
		public int[,] letter_positions = new int[5,5];
		public Letter (char letter)
		{
			switch (letter)
			{
			case 'a':
				break;
			default:
				letter_positions [0, 0] = 1;
				letter_positions [0, 1] = 1;
				letter_positions [0, 2] = 1;
				letter_positions [0, 3] = 1;
				letter_positions [0, 4] = 1;
				letter_positions [1, 0] = 0;
				letter_positions [1, 1] = 1;
				letter_positions [1, 2] = 0;
				letter_positions [1, 3] = 1;
				letter_positions [1, 4] = 0;
				letter_positions [2, 0] = 1;
				letter_positions [2, 1] = 0;
				letter_positions [2, 2] = 1;
				letter_positions [2, 3] = 0;
				letter_positions [2, 4] = 1;
				letter_positions [3, 0] = 1;
				letter_positions [3, 1] = 0;
				letter_positions [3, 2] = 1;
				letter_positions [3, 3] = 1;
				letter_positions [3, 4] = 0;
				letter_positions [4, 0] = 1;
				letter_positions [4, 1] = 1;
				letter_positions [4, 2] = 1;
				letter_positions [4, 3] = 1;
				letter_positions [4, 4] = 1;
				break;
			}
		}

		public void update_line_position()
		{
			bool all_blocks_covered = true;
			for (int i = 0; i < 4; i++) {
				if (this.letter_positions [this.line_position, i] == 1) {
					all_blocks_covered = false;
				}
			}
			if (all_blocks_covered)
				this.line_position++;
		}
	}
}

