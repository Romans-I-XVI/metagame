using System;
using System.Collections.Generic;

namespace metagame
{
	public class BlockHandler
	{
		Random rnd = new Random();
		int ticker = 0;
		int block_speed;
		public List<Block> BlockList = new List<Block>();

		public BlockHandler(int block_speed)
		{
			this.block_speed = block_speed;
		}

		public int tick()
		{
			int removed_block_position = 999; //my way of returning an invalid position, saying no block was removed
			this.ticker += 1;

			for (int i = BlockList.Count - 1; i >= 0; i--)
			{
				BlockList[i].ypos += BlockList[i].speed;
				if (BlockList [i].ypos > Game.HEIGHT - 150 - 20) {
					removed_block_position = BlockList [i].position;
					BlockList.RemoveAt (i);
				}
			}

			if (this.ticker >= 60) 
			{
				int rnd_position = rnd.Next(5);
				Block block = new Block (rnd_position, Game.WIDTH/2-50+rnd_position*20, 0, 20, 20, this.block_speed);
				this.BlockList.Add (block);
				this.ticker = 0;
			}
			return removed_block_position;
		}
	}
}

