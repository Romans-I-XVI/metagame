using System;

namespace metagame
{
	public class Player
	{
		public int width = 260;
		public int height = 5;
		public int position = 0;
		public int xpos;
		public int x_offset;
		public int ypos;
		public Player ()
		{
			this.xpos = Game.WIDTH / 2 - this.width / 2;
			this.ypos = Game.HEIGHT - Game.HEIGHT/4;
			this.x_offset = 0;
		}
		public void UpdatePosition ()
		{
			int new_xpos = (Game.WIDTH / 2 - this.width / 2)+20*this.position;
			if (this.xpos < new_xpos) {
				this.xpos += 5;
				this.x_offset = Game.WIDTH / 2 - (this.xpos + this.width / 2);
			} else if (this.xpos > new_xpos) {
				this.xpos -= 5;
				this.x_offset = Game.WIDTH / 2 - (this.xpos + this.width / 2);
			}
		}
	}
}

