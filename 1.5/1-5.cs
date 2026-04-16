using System;
using System.Collections.Generic;

namespace Lab1_5_SearchAlgorithms
{
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int BirthDay { get; set; }
        public int BirthMonth { get; set; }
        public int BirthYear { get; set; }
        public string Hobby { get; set; }

        public Student(string lastName, string firstName, int day, int month, int year, string hobby)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDay = day;
            BirthMonth = month;
            BirthYear = year;
            Hobby = hobby;
        }

        public override string ToString()
        {
            return $"{LastName,-12} {FirstName,-10} | DOB: {BirthDay:D2}.{BirthMonth:D2}.{BirthYear} | Hobby: {Hobby}";
        }
    }

    public class TreeNode
    {
        public Student Data { get; set; }
        public int Key => Data.BirthDay;
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(Student data)
        {
            Data = data;
        }
    }

    public class BST
    {
        public TreeNode Root;

        private TreeNode RotateRight(TreeNode y)
        {
            TreeNode x = y.Left;
            TreeNode T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            return x;
        }

        private TreeNode RotateLeft(TreeNode x)
        {
            TreeNode y = x.Right;
            TreeNode T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            return y;
        }

        public TreeNode InsertAtRoot(TreeNode root, Student data)
        {
            if (root == null) return new TreeNode(data);

            if (data.BirthDay < root.Key)
            {
                root.Left = InsertAtRoot(root.Left, data);
                root = RotateRight(root);
            }
            else
            {
                root.Right = InsertAtRoot(root.Right, data);
                root = RotateLeft(root);
            }
            return root;
        }

        public void AddNode(Student data)
        {
            Root = InsertAtRoot(Root, data);
            Console.WriteLine($"\nAdded: {data.LastName} (Key: {data.BirthDay})");
            PrintBFS();
        }

        public TreeNode Search(TreeNode root, int key)
        {
            if (root == null || root.Key == key)
                return root;

            if (key < root.Key)
                return Search(root.Left, key);

            return Search(root.Right, key);
        }

        public void PrintBFS()
        {
            if (Root == null) return;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                TreeNode current = queue.Dequeue();
                Console.WriteLine(current.Data.ToString());

                if (current.Left != null) queue.Enqueue(current.Left);
                if (current.Right != null) queue.Enqueue(current.Right);
            }
        }
    }

    class Program
    {
        public static int SequentialSearch(Student[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                bool isSummer = array[i].BirthMonth >= 6 && array[i].BirthMonth <= 8;
                bool isTourism = array[i].Hobby.Equals("tourism", StringComparison.OrdinalIgnoreCase);

                if (isSummer && isTourism)
                {
                    return i;
                }
            }
            return -1;
        }

        static void PrintArray(Student[] array)
        {
            foreach (var student in array)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Student[] students = new Student[]
            {
                new Student("Kovalenko", "Ivan", 15, 6, 2003, "tourism"),
                new Student("Shevchenko", "Anna", 12, 1, 2004, "drawing"),
                new Student("Boiko", "Petro", 25, 7, 2003, "tourism"),
                new Student("Tkachenko", "Olena", 5, 5, 2002, "dancing"),
                new Student("Kravchenko", "Ihor", 18, 8, 2004, "sports"),
                new Student("Oliinyk", "Mariia", 22, 12, 2003, "reading"),
                new Student("Moroz", "Oleh", 10, 8, 2003, "tourism"),
                new Student("Lysenko", "Yana", 30, 3, 2004, "music"),
                new Student("Hrytsenko", "Vlad", 14, 7, 2003, "programming"),
                new Student("Savchenko", "Daria", 1, 6, 2004, "tourism"),
                new Student("Romanenko", "Maksym", 19, 10, 2002, "photography"),
                new Student("Kozak", "Yuliia", 27, 2, 2005, "sports"),
                new Student("Pavlenko", "Artem", 8, 8, 2003, "chess"),
                new Student("Marchenko", "Inna", 3, 9, 2004, "tourism"),
                new Student("Riabokon", "Serhii", 17, 7, 2003, "esports"),
                new Student("Volkov", "Denys", 21, 11, 2002, "music"),
                new Student("Zaitseva", "Alina", 9, 4, 2004, "drawing"),
                new Student("Melnyk", "Roman", 11, 6, 2003, "tourism"),
                new Student("Polishchuk", "Kateryna", 28, 5, 2004, "dancing"),
                new Student("Bondarenko", "Oleksandr", 4, 1, 2003, "tourism")
            };

            PrintArray(students);

            int indexToRemove;
            while ((indexToRemove = SequentialSearch(students)) != -1)
            {
                for (int i = indexToRemove; i < students.Length - 1; i++)
                {
                    students[i] = students[i + 1];
                }
                Array.Resize(ref students, students.Length - 1);
            }

            PrintArray(students);

            BST tree = new BST();
            int nodesToAdd = Math.Min(5, students.Length);
            for (int i = 0; i < nodesToAdd; i++)
            {
                tree.AddNode(students[i]);
            }

            int searchKey = students[2].BirthDay;
            TreeNode foundNode = tree.Search(tree.Root, searchKey);

            if (foundNode != null)
            {
                Console.WriteLine($"\nFound node by key {searchKey}:");
                Console.WriteLine(foundNode.Data);
            }
        }
    }
}