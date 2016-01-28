using System;

namespace metagame
{
	public class Player
	{
		public int position = 0;
		public int xpos;
		public int offset;
		public int ypos;

		// Create the Player object in the default centered position
		public Player ()
		{
			this.xpos = Game.WIDTH / 2;
			this.ypos = Game.LOWER_LINE_POSITION;
			this.offset = 0;
		}

		// Move towards the new x position
		public void UpdatePosition ()
		{
			int new_xpos = Game.WIDTH/2+20*this.position;
			if (this.xpos < new_xpos) {
				this.xpos += 5;
				this.offset = Game.WIDTH / 2-this.xpos;
			} else if (this.xpos > new_xpos) {
				this.xpos -= 5;
				this.offset = Game.WIDTH / 2-this.xpos;
			}
		}
	}
}

