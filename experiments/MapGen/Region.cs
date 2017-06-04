using System;
using System.Collections.Generic;
using System.Drawing;

namespace MapGen
{
    public class Region
    {
        /// <summary>
        /// The Matrix for map generation
        /// each cell contains probability of a star forming, also represents number of stars average in region
        /// </summary>
        private int[,] _chanceMatrix = new int[10, 10]
        {
            {01, 01, 01, 02, 05, 05, 02, 01, 01, 01}, //[__][__][01][02][05][05][02][01][__][__]
            {00, 10, 10, 10, 10, 10, 10, 10, 10, 01}, //[__][10][10][10][10][10][10][10][10][__]
            {01, 10, 25, 25, 25, 25, 25, 25, 10, 01}, //[01][10][25][25][25][25][25][25][10][01]
            {02, 25, 25, 50, 50, 50, 50, 25, 10, 02}, //[02][25][25][50][50][50][50][25][10][02]
            {25, 10, 25, 50, 99, 99, 50, 25, 10, 25}, //[25][10][25][50][99][99][50][25][10][25]
            {05, 10, 25, 50, 99, 99, 50, 25, 25, 05}, //[05][10][25][50][99][99][50][25][25][05]
            {02, 10, 25, 50, 50, 50, 50, 25, 10, 02}, //[02][10][25][50][50][50][50][25][10][02]
            {01, 10, 25, 25, 25, 25, 25, 25, 10, 01}, //[01][10][25][25][25][25][25][25][10][01]
            {01, 10, 10, 10, 10, 10, 10, 10, 10, 01}, //[__][10][10][10][10][10][10][10][10][__]
            {01, 01, 01, 02, 05, 05, 02, 01, 01, 01} //[__][__][01][02][05][05][02][01][__][__]
        };

        public Rectangle Location { get; private set; }
        public List<Point> Stars { get; private set; }
        public int Col { get; private set; }
        public int Row { get; private set; }
        public int Chance => _chanceMatrix[Col, Row];
        public Random Ring { get; private set; }
        public Bitmap Image { get; private set; }
        public int SystemCount { get; internal set; }

        public Region(int region, Random ring)
        {
            Ring = ring;
            Stars = new List<Point>(100);
            Row = (int) Math.Floor((double) (region / 10));
            Col = region - (Row * 10);
            var rect = new Rectangle()
            {
                Top = Row * 100,
                Left = Col * 100,
                Right = Col * 100 + 100,
                Bottom = Row * 100 + 100
            };
            Location = rect;
        }

        public Point GetStarMapPosition(Point star)
        {
            return new Point()
            {
                X = Location.Top + star.X,
                Y = Location.Left + star.Y
            };
        }

        /// <summary>
        /// This should prevent stars that are touching and limit stars per region
        /// </summary>
        /// <param name="star"></param>
        public void TryToAddStar(Point star)
        {

            foreach (var point in Stars)
            {
                if (Stars.Count >= Chance)
                {
                    return;
                }
                if (point.X == star.X || point.X == star.X + 1 || point.X == star.X - 1)
                {
                    return;
                }
                if (point.X == star.X || point.X == star.X + 1 || point.X == star.X - 1)
                {
                    return;
                }
            }
            Stars.Add(star);
        }

        public void Generate()
        {
            for (int i = 0; i < Chance * 100 + 1; i++)
            {
                var chance = 100 - Chance;
                if (Ring.Next(100) > chance)
                {
                    TryToAddStar(new Point { X = Ring.Next(100), Y = Ring.Next(100) });
                }

            }
        }
    }
}
