using System;

namespace LabHashTables
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X:F1}; {Y:F1})";
    }

    public class Rectangle
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }
        public Point D { get; set; }

        public Rectangle(Point a, Point b, Point c, Point d)
        {
            A = a; B = b; C = c; D = d;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        public double GetPerimeter()
        {
            return Distance(A, B) + Distance(B, C) + Distance(C, D) + Distance(D, A);
        }

        public double GetArea()
        {
            return Distance(A, B) * Distance(B, C);
        }

        public override string ToString()
        {
            return $"Rectangle [{A}, {B}, {C}, {D}], Area: {GetArea():F2}";
        }

        public static Rectangle GenerateRandom(Random rnd)
        {
            double x = rnd.NextDouble() * 10;
            double y = rnd.NextDouble() * 10;
            double width = rnd.NextDouble() * 5 + 1;
            double height = rnd.NextDouble() * 5 + 1;

            return new Rectangle(
                new Point(x, y),
                new Point(x + width, y),
                new Point(x + width, y - height),
                new Point(x, y - height)
            );
        }
    }

    public class SimpleHashTable
    {
        private Rectangle[] table;
        private int size;

        public SimpleHashTable(int size)
        {
            this.size = size;
            table = new Rectangle[size];
        }

        private int Hash(double key)
        {
            int intKey = (int)(key * 100);
            return Math.Abs(intKey) % size;
        }

        public bool Insert(Rectangle rect)
        {
            double key = rect.GetPerimeter();
            int index = Hash(key);

            if (table[index] == null)
            {
                table[index] = rect;
                return true;
            }
            return false;
        }

        public void Print()
        {
            Console.WriteLine("\n--- Simple Hash Table (No Collision Resolution) ---");
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null)
                    Console.WriteLine($"[{i,2}] Key (P): {table[i].GetPerimeter(),-6:F2} | {table[i]}");
                else
                    Console.WriteLine($"[{i,2}] --- Empty ---");
            }
        }
    }

    public class DoubleHashingHashTable
    {
        private Rectangle[] table;
        private int size;
        private int primeForHash2;

        public DoubleHashingHashTable(int size)
        {
            this.size = size;
            table = new Rectangle[size];
            primeForHash2 = GetPrimeLessThan(size);
            if (primeForHash2 < 1) primeForHash2 = 1;
        }

        private int GetPrimeLessThan(int n)
        {
            for (int i = n - 1; i >= 2; i--)
                if (IsPrime(i)) return i;
            return 3;
        }

        private bool IsPrime(int n)
        {
            if (n <= 1) return false;
            for (int i = 2; i * i <= n; i++)
                if (n % i == 0) return false;
            return true;
        }

        private int Hash1(int key) => Math.Abs(key) % size;
        private int Hash2(int key) => primeForHash2 - (Math.Abs(key) % primeForHash2);

        public bool Insert(Rectangle rect)
        {
            int key = (int)(rect.GetPerimeter() * 100);
            int h1 = Hash1(key);
            int h2 = Hash2(key);

            for (int i = 0; i < size; i++)
            {
                int index = (h1 + i * h2) % size;

                if (table[index] == null)
                {
                    table[index] = rect;
                    return true;
                }
            }
            return false;
        }

        public void Print()
        {
            Console.WriteLine("\n--- Hash Table (Double Hashing) ---");
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null)
                    Console.WriteLine($"[{i,2}] Key (P): {table[i].GetPerimeter(),-6:F2} | {table[i]}");
                else
                    Console.WriteLine($"[{i,2}] --- Empty ---");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter hash table size: ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0)
            {
                Console.WriteLine("Invalid size!");
                return;
            }

            SimpleHashTable htSimple = new SimpleHashTable(size);
            DoubleHashingHashTable htDouble = new DoubleHashingHashTable(size);
            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                Rectangle rect = Rectangle.GenerateRandom(rnd);
                
                htSimple.Insert(rect);
                htDouble.Insert(rect);
            }

            htSimple.Print();
            htDouble.Print();
            
            Console.ReadLine();
        }
    }
}