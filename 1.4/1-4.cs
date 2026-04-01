using System;

namespace Lab1_4_Var3
{
    class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int GroupNumber { get; set; }
        public string Faculty { get; set; }

        public Student(string lastName, string firstName, int groupNumber, string faculty)
        {
            LastName = lastName;
            FirstName = firstName;
            GroupNumber = groupNumber;
            Faculty = faculty;
        }

        public string GetFormattedInfoSingleCriteria()
        {
            return $"Group: {GroupNumber} | Name: {LastName} {FirstName}, Faculty: {Faculty}";
        }

        public string GetFormattedInfoDoubleCriteria()
        {
            return $"Group: {GroupNumber}, Last Name: {LastName,-10} | First Name: {FirstName}, Faculty: {Faculty}";
        }
    }

    class Program
    {
        static void Main()
        {
            Student[] studentsTask1 = {
                new Student("Kovalenko", "Ivan", 102, "FIOT"),
                new Student("Boiko", "Anna", 101, "FPM"),
                new Student("Melnyk", "Petro", 103, "FIOT"),
                new Student("Shevchenko", "Mariia", 101, "FPM"),
                new Student("Tkachenko", "Oleh", 102, "IPSA")
            };

            PrintArray(studentsTask1, 1);
            InsertionSort(studentsTask1);
            PrintArray(studentsTask1, 1);

            Student[] studentsTask2 = {
                new Student("Kovalenko", "Ivan", 102, "FIOT"),
                new Student("Boiko", "Anna", 101, "FPM"),
                new Student("Melnyk", "Petro", 103, "FIOT"),
                new Student("Avramenko", "Ihor", 101, "FPM"),
                new Student("Shevchenko", "Mariia", 101, "FPM"),
                new Student("Tkachenko", "Oleh", 102, "IPSA")
            };

            PrintArray(studentsTask2, 2);
            int[] sortedIndices = IndexSort(studentsTask2);
            PrintByIndices(studentsTask2, sortedIndices);
        }

        static void InsertionSort(Student[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                Student key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j].GroupNumber > key.GroupNumber)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        static int[] IndexSort(Student[] arr)
        {
            int[] indices = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                indices[i] = i;
            }

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (CompareStudents(arr[indices[j]], arr[indices[j + 1]]) > 0)
                    {
                        int temp = indices[j];
                        indices[j] = indices[j + 1];
                        indices[j + 1] = temp;
                    }
                }
            }
            return indices;
        }

        static int CompareStudents(Student a, Student b)
        {
            int groupComparison = a.GroupNumber.CompareTo(b.GroupNumber);
            if (groupComparison != 0)
            {
                return groupComparison;
            }
            
            return string.Compare(a.LastName, b.LastName, StringComparison.OrdinalIgnoreCase);
        }

        static void PrintArray(Student[] arr, int formatMode)
        {
            foreach (var student in arr)
            {
                Console.WriteLine(formatMode == 1 ? student.GetFormattedInfoSingleCriteria() : student.GetFormattedInfoDoubleCriteria());
            }
            Console.WriteLine();
        }

        static void PrintByIndices(Student[] arr, int[] indices)
        {
            foreach (int index in indices)
            {
                Console.WriteLine(arr[index].GetFormattedInfoDoubleCriteria());
            }
            Console.WriteLine();
        }
    }
}