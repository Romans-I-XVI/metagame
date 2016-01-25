using System;

namespace metagame
{
	public class Block
	{
		public int position;
		public int xpos;
		public int ypos;
		public int width;
		public int height;
		public int speed;

		public Block(int position, int xpos, int ypos, int width, int height, int speed)
		{
			this.position = position;
			this.xpos = xpos;
			this.ypos = ypos;
			this.width = width;
			this.height = height;
			this.speed = speed;
		}
	}
}

