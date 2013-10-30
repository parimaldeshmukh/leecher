using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    public enum Direction {Up, Down, Left, Right, None};

    public static class DirectionExtension
    {
        public static Direction keyToDirection(this Keys key)
        {
            if (key == Keys.Up) return Direction.Up;
            if (key == Keys.Down) return Direction.Down;
            if (key == Keys.Left) return Direction.Left;
            if (key == Keys.Right) return Direction.Right;
            return Direction.None;
        }
    }
}
