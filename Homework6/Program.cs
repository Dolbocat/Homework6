using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Homework6
{
    class Program
    {
        static void Main()
        {
            bool f = true;

            while (f)
            {
                Console.Clear();
                Console.WriteLine("Тавризов Алексей, Домашняя работа 6");
                Console.WriteLine("Мои задачи");
                Console.WriteLine("=================================");
                Console.WriteLine("1 -> Задача 1");
                Console.WriteLine("2 -> Задача 2");
                Console.WriteLine("0 -> Завершение работы приложения");
                Console.WriteLine("=================================\n");

                Console.Write("Введите номер задачи: ");
                int number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 0:
                        f = false;
                        Console.WriteLine("Завершение работы приложения ...");
                        break;

                    case 1:
                        Console.Clear();                                        
                        Console.WriteLine("Таблица функции x^3");
                        Table(MyFunc, -2, 2);
                        Console.WriteLine("Таблица функции Sin:");
                        Table(Math.Sin, -2, 2);                       
                        Console.WriteLine("Таблица функции x^2:");                        
                        Table(delegate (double x) { return x * x; }, 0, 3);
                        Console.WriteLine("Таблица функции a*x^2:");
                        Table(delegate (double x, double a) { return a * x * x; }, 0, 3, 2);
                        Console.WriteLine("Таблица функции a*sin(x):");
                        Table(delegate (double x, double a) { return a * Sin(x); }, 0, 3, 2);
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.Clear();
                        Func[] func = { F, R, T };
                        Console.WriteLine("Выберите функцию, которая требуется: 1 - функция F, 2 - функция R, 3 - функция T");
                        int index = int.Parse(Console.ReadLine());
                        SaveFunc(func[index - 1], "data.bin", -100, 100, 0.5);
                        Console.WriteLine(Load("data.bin"));
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Некорректный номер задачи.\nПовторите ввод.");
                        break;

                }
            }
        }
        public delegate double Func(double x);

        public static double F(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static double R(double x)
        {
            return x * x + 20 * x - 10;
        }

        public static double T(double x)
        {
            return x * x - 50 / x + 10;
        }

        public static void SaveFunc(Func func, string fileName, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(func(x));
                x += h;
            }
            bw.Close();
            fs.Close();
        }

        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                d = bw.ReadDouble();
                if (d < min)
                    min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }



        public delegate double Fun(double x);

        public delegate double Fun2(double x, double a);

        public static void Table(Fun F, double x, double b)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }

        public static void Table(Fun2 F, double x, double b, double a)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b && a <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x,a));
                x += 1;
                a += 1;
            }
            Console.WriteLine("---------------------");
        }

        public static double MyFunc(double x)
        {
            return x * x * x;
        }                             
    } 
}

