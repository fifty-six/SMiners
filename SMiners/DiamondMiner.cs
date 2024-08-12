﻿using System;
using Microsoft.Xna.Framework;

namespace SMiners
{
    internal class DiamondMiner : Miner
    {
        public DiamondMiner(Color col, int worldX, int worldY, int x, int y)
        {
            color = col;
            direction = Direction.Up;
            type = MinerType.Diamond;
            xMax = worldX;
            yMax = worldY;
            position = new Point(x, y);
        }

        public override Point GetNext(Miner[,] world, Random rand)
        {
            int jumpsize = 1;
            Miner next = DecideMove(world, rand);

            while (next.type != MinerType.Ore && next.position != position)
            {
                jumpsize++;
                next = GetFront(world, jumpsize);
            }

            return next.position;
        }

        private Miner DecideMove(Miner[,] world, Random rng)
        {
            direction = (Direction)(((int)direction + 1) % 4);

            Miner m_pos = GetFront(world, 1);
            if (m_pos.type != MinerType.Diamond) return m_pos;

            direction = (Direction)(((int)direction + 2) % 4);
            Miner m_neg = GetFront(world, 1);
            if (m_neg.type != MinerType.Diamond) return m_neg;

            return (rng.Next(2) == 1) ? m_pos : m_neg ;
        }
    }
}
