using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MapGen
{
    public class Map
    {
        public List<Region> Regions { get; private set; } = new List<Region>(100);
        public Random Ring { get; private set; }

        public int SystemsCount
        {
            get
            {
                var ret = 0;
                foreach (var region in Regions)
                {
                    ret += region.SystemCount;
                }
                return ret;
            }
        }
        public Map(int seed=-1)
        {
            if(seed == -1) { Ring=new Random(Environment.TickCount); } else { Ring=new Random(seed); }
            for (int i = 0; i < 99; i++)
            {
                Regions.Add(new Region(i,Ring));
            }
        }

        public void Generate()
        {
            var bmp = new Bitmap(1000, 1000);
            var gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.Black);
            gfx.SmoothingMode = SmoothingMode.HighQuality;

            foreach (var mapRegion in Regions)
            {
                mapRegion.Generate();

                foreach (var mapRegionStar in mapRegion.Stars)
                {
                    var loc = mapRegion.GetStarMapPosition(mapRegionStar);
                    bmp.SetPixel(loc.X, loc.Y, Color.White);
                    mapRegion.SystemCount++;
                }
            }
            gfx.Dispose();
            Image = bmp;
        }

        public Bitmap Image { get; private set; }
    }
}
