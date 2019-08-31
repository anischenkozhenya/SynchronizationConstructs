using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Work
    {        
        readonly Thread thread;
        readonly AutoResetEvent auto;
        public Work(string name, AutoResetEvent auto)

        {
            this.thread = new Thread(this.Run) { Name = name };
            this.auto = auto;
            this.thread.Start();
        }

        void Run()
        {
            Console.WriteLine("Запущен поток " + thread.Name);

            for (int i = 0; i < 10; i++)
            {
                Console.Write(". ");
                Thread.Sleep(200);
            }

            Console.WriteLine("\nПоток " + thread.Name + " завершен");
            auto.Set();
        }
    }
}
