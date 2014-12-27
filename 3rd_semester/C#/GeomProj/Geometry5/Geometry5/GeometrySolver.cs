using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry5
{
    public class GeometrySolver
    {
        public static int checkIsAPointInsideOfATriangle(Tuple<int, int> point, Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>> triangle)
        {
            int p0x = point.Item1;
            int p0y = point.Item2;
            int p1x = triangle.Item1.Item1;
            int p1y = triangle.Item1.Item2;
            int p2x = triangle.Item2.Item1;
            int p2y = triangle.Item2.Item2;
            int p3x = triangle.Item3.Item1;
            int p3y = triangle.Item3.Item2;

            int a = (p1x - p0x) * (p2y - p1y) - (p2x - p1x) * (p1y - p0y);
            int b = (p2x - p0x) * (p3y - p2y) - (p3x - p2x) * (p2y - p0y);
            int c = (p3x - p0x) * (p1y - p3y) - (p1x - p3x) * (p3y - p0y);

            if ((a == 0) || (b == 0) || (c == 0))
            {
                return 0;           //The point lies on an edge
            }
            else if (((a > 0) && (b > 0) && (c > 0)) || ((a < 0) && (b < 0) && (c < 0)))
            {
                return 1;          //The point lies inside of an triangle
            }
            else
            {
                return -1;           //The point lies outside of an triangle
            }
        }
    }
}