using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnomalyDetectionDll
{
    public class minCircle
    {
    public static double Distance(Point pointA, Point pointB)
    {
        return Math.Sqrt(Math.Pow(pointA.GetX() - pointB.GetX(), 2) + Math.Pow(pointA.GetY() - pointB.GetY(), 2)); 
    }
    public static bool IsPointInsideCircle(Circle circle, Point point)
    {
        return Distance(circle.GetCenter(), point) <= circle.GetRadius();
    }

    public static Point CreateCircleCenter(Point pointA, Point pointB, Point pointC)
    {
        double var1 = pointC.GetX() * (pointA.GetY() - pointB.GetY());
        var1 += pointB.GetX() * (pointC.GetY() - pointA.GetY());
        var1 += pointA.GetX() * (pointB.GetY() - pointC.GetY());
        double[] arr = { (pointA.GetX() * pointA.GetX() + pointA.GetY() * pointA.GetY()), (pointB.GetX() * pointB.GetX() + pointB.GetY() * pointB.GetY()), (pointC.GetX() * pointC.GetX() + pointC.GetY() * pointC.GetY()) };
        double coordinateXSum = (arr[0] * (pointB.GetY() - pointC.GetY()) + arr[1] * (pointC.GetY() - pointA.GetY()) + arr[2] * (pointA.GetY() - pointB.GetY())) / 2;
        double coordinateYSum = (arr[0] * (pointC.GetX() - pointB.GetX()) + arr[1] * (pointA.GetX() - pointC.GetX()) + arr[2] * (pointB.GetX() - pointA.GetX())) / 2;
        return new Point(coordinateXSum / var1, coordinateYSum / var1);
    }

    public static double CreateCircleRadius(Point pointA, Point pointB, Point pointC)
    {
        double var1 = pointC.GetX() * (pointA.GetY() - pointB.GetY());
        var1 += pointB.GetX() * (pointC.GetY() - pointA.GetY());
        var1 += pointA.GetX() * (pointB.GetY() - pointC.GetY());
        double[] arr = { (pointA.GetX() * pointA.GetX() + pointA.GetY() * pointA.GetY()), (pointB.GetX() * pointB.GetX() + pointB.GetY() * pointB.GetY()), (pointC.GetX() * pointC.GetX() + pointC.GetY() * pointC.GetY()) };
        double var2 = arr[0] * (pointB.GetX() * pointC.GetY() - pointC.GetX() * pointB.GetY()) + arr[1] * (pointC.GetX() * pointA.GetY() - pointA.GetX() * pointC.GetY()) + arr[2] * (pointA.GetX() * pointB.GetY() - pointA.GetY() * pointB.GetX());
        double coordinateXSum = (arr[0] * (pointB.GetY() - pointC.GetY()) + arr[1] * (pointC.GetY() - pointA.GetY()) + arr[2] * (pointA.GetY() - pointB.GetY())) / 2;
        double coordinateYSum = (arr[0] * (pointC.GetX() - pointB.GetX()) + arr[1] * (pointA.GetX() - pointC.GetX()) + arr[2] * (pointB.GetX() - pointA.GetX())) / 2;
        return Math.Sqrt(var1 * var2 + coordinateXSum * coordinateXSum + coordinateYSum * coordinateYSum) / Math.Abs(var1);
    }

    public static Circle CreateCircleFromTwoPoint(Point pointA, Point pointB)
    {
        Point pointC = new Point((pointA.GetX() + pointB.GetX()) / 2, (pointA.GetY() + pointB.GetY()) / 2);
        return new Circle( pointC, Distance(pointA, pointB) / 2);
    }

    public static Circle CreateCircleFromThreePoint(Point pointA, Point pointB, Point pointC)
    {
        Point center = CreateCircleCenter(pointA, pointB, pointC);
        double rad = CreateCircleRadius(pointA, pointB, pointC);
        return new Circle(center, rad);
    }

    public static bool IsValidCircle(Circle circle, List<Point> mainPoints)
    {
        foreach (Point point in mainPoints) 
        if (!IsPointInsideCircle(circle, point))
            return false;
        return true;
    }

    public static Circle CreateTrivialMinCircle(List<Point> mainPoints)
    {
        if (!mainPoints.Any())
        {
            return new Circle(new Point(0,0),0);
        }
        else if (mainPoints.Count == 1)
        {
            return new Circle(mainPoints[0], 0);
        }
        else if (mainPoints.Count == 2)
        {
            return CreateCircleFromTwoPoint(mainPoints[0], mainPoints[1]);
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = i + 1; j < 3; j++)
            {

                Circle circle = CreateCircleFromTwoPoint(mainPoints[i], mainPoints[j]);
                if (IsValidCircle(circle, mainPoints))
                    return circle;
            }
        }
        return CreateCircleFromThreePoint(mainPoints[0], mainPoints[1], mainPoints[2]);
    }

    public static void Swap(Point first, Point second)
    {
        Point temp=first;
        first = second;
        second = temp;
    }

    public static Circle Helper(List<Point> mainPoints, List<Point> secondryPoints, int n)
    {
        if (n == 0 || secondryPoints.Count == 3)
        {
            return CreateTrivialMinCircle(secondryPoints);
        }
        int index=0;
        Random rand = new Random();
        index = rand.Next() % n;
        Point point = mainPoints[index];

        Swap(mainPoints[index], mainPoints[n - 1]);

        Circle d = Helper(mainPoints, secondryPoints, n - 1);

        if (IsPointInsideCircle(d, point))
        {
            return d;
        }

        secondryPoints.Add(point);

        return Helper(mainPoints, secondryPoints, n - 1);
    }

    public static Circle MinCircle(List<Point> mainPoints)
    {
        return Helper(mainPoints, new List<Point>(), mainPoints.Count);
    }

    public static Circle FindMinCircle(Point[] points, int size)
    {
        List<Point> listOfPoints = new List<Point>();
        int index = 0;
        while (index < size)
        {
            listOfPoints.Add(points[index]);
            index++;
        }
        return MinCircle(listOfPoints);
    }
    }
}
