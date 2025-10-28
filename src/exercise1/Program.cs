using System;

class AreaConvexQuadrilateral
{
    static void Main()
    {
        var coordinates = new double[8];
        int coordinates_сount = 0;
        while (coordinates_сount < 8)
        {
            var input = Console.ReadLine();
            input = input?.Replace('.', ',');
            if (double.TryParse(input, out double value))
            {
                coordinates[coordinates_сount]=value;
                coordinates_сount++;
            }
            else
            {
                Console.Write("Couldn't parse a number. Please, try again ");
            }
        }
    
        double x1 = coordinates[0], y1 = coordinates[1];
        double x2 = coordinates[2], y2 = coordinates[3];
        double x3 = coordinates[4], y3 = coordinates[5];
        double x4 = coordinates[6], y4 = coordinates[7];
        var area1 = AreaTriangleByHeron((x1, y1), (x2, y2), (x3, y3));
        var area2 = AreaTriangleByHeron((x1, y1), (x3, y3), (x4, y4));
        double total = area1 + area2;
        Console.WriteLine("Square = " + total.ToString("0.####"));
    }
    static double Dist((double x,double y) A, (double x,double y) B) =>
        Math.Sqrt((A.x-B.x)*(A.x-B.x) + (A.y-B.y)*(A.y-B.y));

    static double AreaTriangleByHeron((double x,double y) A, (double x,double y) B, (double x,double y) C)
    {
        double a = Dist(B, C);
        double b = Dist(A, C);
        double c = Dist(A, B);
        double p = 0.5 * (a + b + c);
        double under = p * (p - a) * (p - b) * (p - c);
        return under <= 0 ? 0.0 : Math.Sqrt(under);
    }
}
