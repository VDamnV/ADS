using System;
using System.Diagnostics;

namespace AlgorithmAnalysisLab
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizes = { 100, 10000, 1000000 };
            int iterations = 3; 

            Console.WriteLine("{0,-15} | {1,-25} | {2,-25}", "Size (N)", "Merge Sort (ns)", "Bubble Sort (ns)");
            Console.WriteLine(new string('-', 70));

            foreach (int size in sizes)
            {
                int[] originalArray = GenerateRandomArray(size);
                long mergeTime = MeasureAverageTime(MergeSortWrapper, originalArray, iterations);

                string bubbleTimeStr;
                if (size <= 10000)
                {
                    long bubbleTime = MeasureAverageTime(BubbleSort, originalArray, iterations);
                    bubbleTimeStr = bubbleTime.ToString();
                }
                else
                {
                    bubbleTimeStr = "Skipped (too long)"; 
                }

                Console.WriteLine("{0,-15} | {1,-25} | {2,-25}", size, mergeTime, bubbleTimeStr);
            }
            
            Console.ReadLine();
        }

        static long MeasureAverageTime(Action<int[]> sortMethod, int[] originalArray, int iterations)
        {
            long totalTimeNs = 0;

            for (int i = 0; i < iterations; i++)
            {
                int[] arrayCopy = (int[])originalArray.Clone();

                Stopwatch sw = Stopwatch.StartNew();
                sortMethod(arrayCopy);
                sw.Stop();

                totalTimeNs += (sw.ElapsedTicks * 1_000_000_000L) / Stopwatch.Frequency;
            }

            return totalTimeNs / iterations;
        }

        static int[] GenerateRandomArray(int size)
        {
            Random rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(1, 100000);
            }
            return array;
        }
        
        static void MergeSortWrapper(int[] array)
        {
            MergeSort(array, 0, array.Length - 1);
        }

        static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                MergeSort(array, left, mid);
                MergeSort(array, mid + 1, right);
                Merge(array, left, mid, right);
            }
        }

        static void Merge(int[] array, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(array, left, leftArray, 0, n1);
            Array.Copy(array, mid + 1, rightArray, 0, n2);

            int i = 0, j = 0, k = left;

            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                    array[k++] = leftArray[i++];
                else
                    array[k++] = rightArray[j++];
            }

            while (i < n1) array[k++] = leftArray[i++];
            while (j < n2) array[k++] = rightArray[j++];
        }

        static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }
    }
}