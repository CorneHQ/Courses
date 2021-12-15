using System;
using System.Diagnostics;
using System.Threading;
using Exercise;

namespace Concurrent
{
    public class ConPrimeNumbers : PrimeNumbers
    {
        public ConPrimeNumbers()
        {
        }
        /// <summary>
        /// This method 
        /// </summary>
        /// <param name="m"> is the minimum number</param>
        /// <param name="M"> is the maximum number</param>
        /// <param name="nt"> is the number of threads. For simplicity assume two.</param>
        public void runConcurrent(int m, int M)
        {
            int halfNumbers = M / 2;

            Thread t1 = new Thread(() => this.runSequential(m, halfNumbers));
            Thread t2 = new Thread(() => this.runSequential(halfNumbers+1, M));

            t1.Start();
            t2.Start();

            Thread.Sleep(2000);

            t1.Join();
            t2.Join();
        }

    }
}
