using System;
using System.Collections.Generic;
using System.Text;

namespace RegretionBasedDll
{
    public class Circle
    {
        Point center;
        double radius;
        public Circle(Point c, double r)
        {
            this.center = c;
            this.radius = r;
        }

        public Circle()
        {
            this.center = new Point(0, 0);
            this.radius = 0;
        }

        public Point GetCenter() { return this.center; }
        public double GetRadius() { return this.radius; }
    }
}
