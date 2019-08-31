using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//Создайте приложение, которое может быть запущено только в одном экземпляре(используя именованный Mutex). 

//method блокируетяс на вложенные мьютексы
namespace Task3
{
    class Program
    {
        static readonly Mutex mutex = new Mutex(false, "1234790-qwertyuio");
        static void Main(string[] args)
        {
            mutex.WaitOne();
            Thread[] threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Method);
                threads[i].Start();
                threads[i].Name = i.ToString();
            }
            mutex.ReleaseMutex();            
            Console.ReadKey();
        }
        static void Method()
        {
            Console.WriteLine("Поток " + Thread.CurrentThread.Name + " Запущен");
            mutex.WaitOne();
            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                Thread.Sleep(50);
            }
            Console.WriteLine("Поток " + Thread.CurrentThread.Name + " Окончен");
            mutex.ReleaseMutex();
        }
    }
}
