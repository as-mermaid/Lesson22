using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите длину массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(GetSumm);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(GetMax);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{task1.Result[i]} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Сумма {task2.Result}");
            Console.WriteLine($"Максимум {task3.Result}");
            Console.ReadKey();
        }

        static int[] GetArray (object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(0, 100);
            }
            return array;
        }

        static int GetSumm(Task<int[]> task)
        {
            int[] array = task.Result;
            int summ = 0;
            for (int i = 0;i < array.Count();i++)
            {
                summ += array[i];
            }
            return summ;
        }

        static int GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i] > max)
                    max = array[i];
            }
            return max;
        }
    }
}
