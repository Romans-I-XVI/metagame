using System;
using System.Collections.Generic;

namespace metagame
{
	public class BlockHandler
	{
		Random rnd = new Random();
		int ticker = 0;
		float block_speed = 0;
		float block_accel;
		public List<Block> BlockList = new List<Block>();

		//Create the block handler with the appropriate block acceleration
		public BlockHandler(float block_accel)
		{
			this.block_accel = block_accel;
		}

		// Perform block related actions on a regular interval (called within the gEngine rendering section)
		public int? tick(int spawn_interval)
		{
			int? removed_block_position = null; 
			this.ticker += 1;

			// Move Block positions
			for (int i = BlockList.Count - 1; i >= 0; i--)
			{

				BlockList[i].speed += this.block_accel;
				BlockList[i].ypos += (int) BlockList[i].speed;
				if (BlockList [i].ypos > Game.LOWER_LINE_POSITION - 20) {
					removed_block_position = BlockList [i].position;
					BlockList.RemoveAt (i);
				}
			}

			// Add a new Block to the list
			if (this.ticker >= spawn_interval) 
			{
				int rnd_position = rnd.Next(5);
				Block block = new Block (rnd_position, Game.WIDTH/2-50+rnd_position*20, -20, 20, 20, this.block_speed);
				this.BlockList.Add (block);
				this.ticker = 0;
			}
			return removed_block_position;
		}
	}
}

