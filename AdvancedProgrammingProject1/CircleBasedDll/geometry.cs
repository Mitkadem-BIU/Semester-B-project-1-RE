using System;
using System.Collections.Generic;
using System.Text;

namespace CircleBasedDll
{
    public class Line 
    {
        double a, b;
        public Line()
        {
            this.a = 0;
            this.b = 0;
        }

        public Line(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double GetA() { return this.a; }
        public double GetB() { return this.b; }

        public double F(double x)
        {
            return this.a * x + b;
        }
    }

    public class Point
    {
        double x, y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double GetX() { return this.x; }
        public double GetY() { return this.y; }
    }
}
