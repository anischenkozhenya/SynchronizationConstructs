using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//Преобразуйте пример событийной блокировки таким образом, 
//чтобы вместо ручной обработки использовалась автоматическая.

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var auto = new AutoResetEvent(false);
            var thread = new Work("1", auto);
            Console.WriteLine("Основной поток ожидает событие.\n");
            auto.WaitOne();
            Console.WriteLine("\nОсновной поток получил уведомление о событии от первого потока.\n");

            thread = new Work("2", auto);
            Console.WriteLine("Основной поток ожидает событие.\n");
            auto.WaitOne();

            Console.WriteLine("\nОсновной поток получил уведомление о событии от второго потока.");

            Console.ReadKey();
        }
    }
}
