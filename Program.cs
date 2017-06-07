using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

namespace Sample03
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            try
            {
                foreach (int result in Work(10))
                    Console.WriteLine("result: " + result);
            }
            catch (AggregateException aex)
            {
                Console.WriteLine("AggregateException in Run: " + aex.Message);
                aex.Flatten();
                foreach (Exception ex in aex.InnerExceptions)
                    Console.WriteLine("  Exception in Run: " + ex.Message);
            }
            Console.ReadLine();
        }
        private IEnumerable<int> Work(int a)
        {
            int[] values = new int[] { 1, 0, 5, 0, 2 };
            List<Exception> list = new List<Exception>();
            int r;
            foreach (int value in values)
            {
                try
                {
                    r = a / value;
                }
                catch (Exception ex)
                {
                    r = -1;
                    list.Add(ex);
                }
                yield return r;
            }
            if (list.Count > 0)
                throw new AggregateException(list);
        }
    }
}
