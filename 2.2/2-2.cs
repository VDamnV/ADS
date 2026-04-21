using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LabWork2_2
{
    enum State
    {
        q0, q1, q2, q3, q4, qAccept, qError
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ExecuteRegexSearch();
            ExecuteFSMAnalysis();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void ExecuteRegexSearch()
        {
            string filePath = "words.txt";

            if (!File.Exists(filePath))
            {
                File.WriteAllLines(filePath, new[] { "+567A-", "-99-", "+456-", "+8G-", "invalid" });
            }

            string pattern = @"^[\+\-][5-9]+([5-9]+|[A-G]+)?-$";
            Regex regex = new Regex(pattern);

            Console.WriteLine("--- Words from file matching Regex ---");
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (regex.IsMatch(line))
                    {
                        Console.WriteLine($"Match found: {line}");
                    }
                }
            }
        }

        static void ExecuteFSMAnalysis()
        {
            Console.WriteLine("\n--- Finite State Machine Analysis ---");
            Console.Write("Enter string to validate: ");
            string input = Console.ReadLine();

            bool isValid = AnalyzeWithFSM(input);
            Console.WriteLine($"Result: {isValid}");
        }

        static bool AnalyzeWithFSM(string input)
        {
            State currentState = State.q0;

            foreach (char c in input)
            {
                switch (currentState)
                {
                    case State.q0:
                        if (c == '+' || c == '-') currentState = State.q1;
                        else currentState = State.qError;
                        break;
                    case State.q1:
                        if (c >= '5' && c <= '9') currentState = State.q2;
                        else currentState = State.qError;
                        break;
                    case State.q2:
                        if (c >= '5' && c <= '9') currentState = State.q2;
                        else if (c >= '0' && c <= '4') currentState = State.q3;
                        else if (c == 'A' || c == 'G') currentState = State.q4;
                        else if (c == '-') currentState = State.qAccept;
                        else currentState = State.qError;
                        break;
                    case State.q3:
                        if (c >= '0' && c <= '4') currentState = State.q3;
                        else if (c == '-') currentState = State.qAccept;
                        else currentState = State.qError;
                        break;
                    case State.q4:
                        if (c == 'A' || c == 'G') currentState = State.q4;
                        else if (c == '-') currentState = State.qAccept;
                        else currentState = State.qError;
                        break;
                    case State.qAccept:
                        currentState = State.qError;
                        break;
                    case State.qError:
                        break;
                }
            }

            return currentState == State.qAccept;
        }
    }
}