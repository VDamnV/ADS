using System;

namespace ComputationalAlgorithms
{
    class Program
    {
        static double F_Integral(double x) => Math.Cos(Math.Exp(x / 3.0) + x);

        static double F_Root(double x) => Math.Pow(x, 3) - 2 * x;

        static double DF_Root(double x) => 3 * Math.Pow(x, 2) - 2;

        static void Main(string[] args)
        {
            Console.WriteLine("Numerical Integration");
            double a_int = GetInput("Enter start of interval a: ");
            double b_int = GetInput("Enter end of interval b: ");
            double h = GetInput("Enter step h: ");

            Console.WriteLine($"Rectangle Method: {RectangleMethod(a_int, b_int, h):F5}");
            Console.WriteLine($"Trapezoidal Method: {TrapezoidalMethod(a_int, b_int, h):F5}");
            Console.WriteLine($"Simpson's Method: {SimpsonMethod(a_int, b_int, h):F5}\n");

            Console.WriteLine("Root Finding");
            double a_root = GetInput("Enter start of interval a: ");
            double b_root = GetInput("Enter end of interval b: ");
            double eps = 0.001;

            Console.WriteLine($"Bisection Method: {BisectionMethod(a_root, b_root, eps):F5}");
            Console.WriteLine($"Chord Method: {ChordMethod(a_root, b_root, eps):F5}");
            Console.WriteLine($"Tangent Method (Newton): {NewtonMethod(b_root, eps):F5}");
        }

        static double RectangleMethod(double a, double b, double h)
        {
            double sum = 0;
            for (double x = a; x < b; x += h) sum += F_Integral(x);
            return sum * h;
        }

        static double TrapezoidalMethod(double a, double b, double h)
        {
            double sum = (F_Integral(a) + F_Integral(b)) / 2.0;
            for (double x = a + h; x < b; x += h) sum += F_Integral(x);
            return sum * h;
        }

        static double SimpsonMethod(double a, double b, double h)
        {
            int n = (int)Math.Round((b - a) / h);
            if (n % 2 != 0) n++;
            h = (b - a) / n;

            double sum = F_Integral(a) + F_Integral(b);
            double sumOdd = 0, sumEven = 0;

            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                if (i % 2 == 0) sumEven += F_Integral(x);
                else sumOdd += F_Integral(x);
            }
            return (h / 3.0) * (sum + 2 * sumEven + 4 * sumOdd);
        }

        static double BisectionMethod(double a, double b, double eps)
        {
            double c = a;
            while ((b - a) / 2.0 > eps)
            {
                c = (a + b) / 2.0;
                if (Math.Abs(F_Root(c)) < eps) break;
                if (F_Root(a) * F_Root(c) < 0) b = c;
                else a = c;
            }
            return c;
        }

        static double ChordMethod(double a, double b, double eps)
        {
            double c = a;
            while (Math.Abs(b - a) > eps)
            {
                c = a - F_Root(a) * (b - a) / (F_Root(b) - F_Root(a));
                if (Math.Abs(F_Root(c)) < eps) break;
                if (F_Root(a) * F_Root(c) < 0) b = c;
                else a = c;
            }
            return c;
        }

        static double NewtonMethod(double x0, double eps)
        {
            double x1 = x0 - F_Root(x0) / DF_Root(x0);
            while (Math.Abs(x1 - x0) > eps)
            {
                x0 = x1;
                x1 = x0 - F_Root(x0) / DF_Root(x0);
            }
            return x1;
        }

        static double GetInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (double.TryParse(input.Replace('.', ','), out double res)) return res;
            if (double.TryParse(input.Replace(',', '.'), out res)) return res;
            return 0;
        }
    }
}