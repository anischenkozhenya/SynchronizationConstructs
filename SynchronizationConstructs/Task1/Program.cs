using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
//Создайте Semaphore, осуществляющий контроль доступа 
//к ресурсу из нескольких потоков.Организуйте упорядоченный 
//вывод информации о получении доступа в специальный*.log файл.
//!!! Так как одновренно нельзя из разных потоков писать в один файл
// то в реализации Main одновременно освобождается только один поток
// Если закоментить в Method строки пишущие в файл, то можно освободить большее количество потоков

namespace Task1
{
    class Program
    {

        private static Semaphore semaphore;        
        /// <summary>
        /// Метод для выполнения в потоке, имеет сигнатуру делегата ParameterizeThreadStart
        /// </summary>
        /// <param name="fileName"></param>
        static public void Method(object fileName)
        {
            string path = (string)fileName;
            //Блокируем поток
            semaphore.WaitOne();
            //Выводим ID потока который выполняет метод
            Console.WriteLine("in " + Thread.CurrentThread.ManagedThreadId);
            //-//-Записываем в файл
            File.AppendAllText(path, "in " + Thread.CurrentThread.ManagedThreadId);
            //имитация работы потока 1 секунда
            Thread.Sleep(1000);
            //Выводит ID Потока который окончил работу
            Console.WriteLine("out " + Thread.CurrentThread.ManagedThreadId);
            //-//-Записываем в файл
            File.AppendAllText(path, "out " + Thread.CurrentThread.ManagedThreadId);
            //Освобоздаем поток
            semaphore.Release();
        }
        static void Main(string[] args)
        {    
            //С записью в файл
            semaphore = new Semaphore(1, 8);
            //Без зааписи в файл
            //semaphore = new Semaphore(3, 8);
            string name ="log.txt";
            string path = @"..\..\";
            string fileName = path + name;
            Process.Start("explorer.exe", path);            
            //Массив 50 потоков
            Thread[] threads = new Thread[50];
            for (int i = 0; i < threads.Length; i++)
            {
                //Инициализируем поток который вытолняет переданный делегат
                threads[i] = new Thread(Method);
                //Стартуем поток
                threads[i].Start(fileName);
            }

            //Без зааписи в файл раскоментировать 2 строки снизу
            //Приостанаиваем работу потока main на 3 секунды
            //Thread.Sleep(3000);
            //Освобождаем 5 дополнительных потоков(Одновременно может выполнятся 8 ток) 
            //semaphore.Release(5);
            Console.ReadKey();
        }
    }
}
