using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RayTracer
{
    /// <summary>
    /// From: https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?redirectedfrom=MSDN&view=net-5.0
    /// </summary>
    class MyStopwatch
    {
        private Stopwatch sw = new Stopwatch();

        public void Start()
        {
            this.sw.Start();
        }

        public void Stop()
        {
            this.sw.Stop();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = this.sw.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Runtime: " + elapsedTime);
        }
    }
}
