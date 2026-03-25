using System;
using System.Collections.Generic;
using System.Text;

namespace LabBinaryTree
{
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Course { get; set; }
        public uint StudentID { get; set; }
        public bool ServedInArmy { get; set; }

        public Student(string lastName, string firstName, int course, uint studentId, bool servedInArmy)
        {
            LastName = lastName;
            FirstName = firstName;
            Course = course;
            StudentID = studentId;
            ServedInArmy = servedInArmy;
        }

        public override string ToString()
        {
            string armyStatus = ServedInArmy ? "Yes" : "No";
            return $"| {StudentID,-10} | {LastName,-15} | {FirstName,-12} | {Course,-6} | {armyStatus,-14} |";
        }
    }

    public class TreeNode
    {
        public Student Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(Student data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    public class BinaryTree
    {
        public TreeNode Root { get; private set; }

        public void Insert(Student student)
        {
            if (Root == null)
            {
                Root = new TreeNode(student);
                return;
            }

            TreeNode current = Root;
            while (true)
            {
                if (student.StudentID < current.Data.StudentID)
                {
                    if (current.Left == null)
                    {
                        current.Left = new TreeNode(student);
                        break;
                    }
                    current = current.Left;
                }
                else if (student.StudentID > current.Data.StudentID)
                {
                    if (current.Right == null)
                    {
                        current.Right = new TreeNode(student);
                        break;
                    }
                    current = current.Right;
                }
                else
                {
                    Console.WriteLine($"\n[Error] Student with ID {student.StudentID} already exists!");
                    break;
                }
            }
        }

        public void PrintBreadthFirst()
        {
            if (Root == null)
            {
                Console.WriteLine("The tree is empty.");
                return;
            }

            PrintTableHeader();

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                TreeNode current = queue.Dequeue();
                Console.WriteLine(current.Data.ToString());

                if (current.Left != null) queue.Enqueue(current.Left);
                if (current.Right != null) queue.Enqueue(current.Right);
            }
            PrintTableFooter();
        }

        public void SearchByCriteria(int targetCourse, bool targetArmyStatus)
        {
            if (Root == null)
            {
                Console.WriteLine("The tree is empty.");
                return;
            }

            bool isFound = false;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(Root);

            Console.WriteLine($"\nSearch Results (Course: {targetCourse}, Served in Army: {(targetArmyStatus ? "Yes" : "No")}):");
            
            while (queue.Count > 0)
            {
                TreeNode current = queue.Dequeue();

                if (current.Data.Course == targetCourse && current.Data.ServedInArmy == targetArmyStatus)
                {
                    if (!isFound)
                    {
                        PrintTableHeader();
                        isFound = true;
                    }
                    Console.WriteLine(current.Data.ToString());
                }

                if (current.Left != null) queue.Enqueue(current.Left);
                if (current.Right != null) queue.Enqueue(current.Right);
            }

            if (isFound)
            {
                PrintTableFooter();
            }
            else
            {
                Console.WriteLine("-> No students found matching the criteria.");
            }
        }

        private void PrintTableHeader()
        {
            string line = new string('-', 73);
            Console.WriteLine(line);
            Console.WriteLine($"| {"Student ID",-10} | {"Last Name",-15} | {"First Name",-12} | {"Course",-6} | {"Served in Army",-14} |");
            Console.WriteLine(line);
        }

        private void PrintTableFooter()
        {
            Console.WriteLine(new string('-', 73));
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            BinaryTree tree = new BinaryTree();

            tree.Insert(new Student("Kovalenko", "Oleksiy", 2, 2050, false));
            tree.Insert(new Student("Ivanov", "Ivan", 5, 1020, true));
            tree.Insert(new Student("Petrov", "Petro", 3, 3040, false));
            tree.Insert(new Student("Sydorov", "Oleh", 5, 1010, true));
            tree.Insert(new Student("Melnyk", "Andriy", 5, 4080, false));
            tree.Insert(new Student("Shevchenko", "Taras", 4, 2080, true));
            tree.Insert(new Student("Boyko", "Maksym", 5, 1050, true));

            Console.WriteLine("=== Binary Tree Content (Breadth-First Traversal) ===");
            tree.PrintBreadthFirst();

            Console.WriteLine("\n=== Search for Students ===");
            Console.Write("Enter course to search (e.g., 5): ");
            if (int.TryParse(Console.ReadLine(), out int searchCourse))
            {
                Console.Write("Did the student serve in the army? (yes/no): ");
                string armyInput = Console.ReadLine()?.Trim().ToLower();
                bool searchArmy = (armyInput == "yes" || armyInput == "y");

                tree.SearchByCriteria(searchCourse, searchArmy);
            }
            else
            {
                Console.WriteLine("Invalid course input.");
            }

            Console.ReadLine();
        }
    }
}