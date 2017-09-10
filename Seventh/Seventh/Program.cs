using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seventh
{
    /*
      Программа генерирует неполные булевы функции, проверяет их на монотонность,
      и, при необходимости, доопределяет их до монотонности.
    */
    class Program
    {
        static int[] MyFunction;//Неполная булева функция
        static int[] FunctionGenerator(int size)
        {
            Random rnd = new Random();
            int[] function = new int[size];
            for (int i = 0; i < size; i++)
            {
                if (rnd.Next(-1, 2) == 0) { function[i] = 0; }
                if (rnd.Next(-1, 2) == 1) { function[i] = 1; }
                if (rnd.Next(-1, 2) == -1) { function[i] = -1; }//Пустое место (или *)
                if (function[i] != -1) Console.Write(function[i]); else Console.Write("*");
            }
            Console.WriteLine();
            return function;
        }//Генератор неполных булевых функций
        static void ShowFunction (int [] func)
        {
            for (int i =0; i < func.Length; i++)
            {
                Console.Write(func[i] + " ");
            }
        }//Выводит функцию на экран
        static bool MonotonyCheking(int[] func)
        {
            bool ok = true;
            int max = 0;
            for (int i = 0; i < func.Length; i++)
            {
                if (func[i] > max) { max = func[i]; }
                if (func[i] < max && func[i] != -1) { ok = false; }
            }
            return ok;
        }//Проверка на монотонность
        static void FunctionAddition(int[] func)
        {
            
            int PassValues = 0;//Количество пропусков
            int[] temp = new int [func.Length];//копия функции для дальнейшего дополнения
            for (int i = 0; i < temp.Length; i++)//копирование значений функции
            {
                temp[i] = func[i];
            }

            if (MonotonyCheking(func) == false) { Console.WriteLine("Данную функцию невозможно доопределить до монотонной."); return; }

            for (int i = 0; i < func.Length; i++)//Поиск пропусков
            {
                if (func[i] == -1) { PassValues++; }
            }

            if (PassValues==0) { Console.WriteLine("Функция уже доопределена."); return; }

            string[] vectors = new string[(int)Math.Pow(2, PassValues)];//На основе количества пропусков определяется количество вариантов их заполнения
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = Convert.ToString(i, 2);//Создание и заполнение массива вариантов заполнения пропусков
                while (vectors[i].Length % PassValues != 0)
                {
                    vectors[i] = "0" + vectors[i];
                }                
            }
            
            for (int i = 0; i < vectors.Length; i++)
            {
                string str = vectors[i];//i-ый вариант заполнения пропусков                       
               
                for (int j = 0; j< PassValues; j++)
                {
                                    
                    for(int k =0;k<temp.Length;k++)
                    {
                   
                        if (temp[k] == -1) { temp[k] = int.Parse(String.Concat(str[j])); break; }//Если пропуск, то он заполняется j-ым символом i-го варианта заполнения                                  
                    }
                    
                }

                if (MonotonyCheking(temp) == true) { ShowFunction(temp); Console.WriteLine(); }
                
                for (int l = 0; l < temp.Length; l++)
                {
                    temp[l] = func[l];//Повторное копирование первоначальных значений для проверки следующих вариантов
                }
            }
            
        }//Доопределение до монотонной
        static void Main(string[] args)
        {
            MyFunction = FunctionGenerator(8);
            FunctionAddition(MyFunction);
            Console.ReadLine();
        }
    }
}
