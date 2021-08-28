using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RayTracer
{
    class MyStopwatch
    {
        private Stopwatch sw = new Stopwatch();
        public void Start()
        {
            this.sw.Start();
        }

        /// <summary>
        /// From: https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?redirectedfrom=MSDN&view=net-5.0
        /// </summary>
        public void Stop()
        {
            this.sw.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = this.sw.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime: " + elapsedTime);

            //String f = "time.txt";
            //using (StreamWriter sw = new StreamWriter(f))
            //{
            //    sw.WriteLine("RunTime: " + elapsedTime);
            //}
        }
    }
}
