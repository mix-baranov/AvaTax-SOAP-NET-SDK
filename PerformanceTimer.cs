using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter.Utililty
{
    internal class PerformanceTimer
    {
        private long start;
        private long stop;
        private static long frequency = 0;
        private long total;
        Decimal multiplier = new Decimal(1.0e9);

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        internal PerformanceTimer()
        {
            start = 0;
            stop = 0;
            total = 0;
            
            if (frequency == 0)
            {
                if (QueryPerformanceFrequency(out frequency) == false)
                {
                    // Frequency not supported
                    throw new Win32Exception();
                }
            }
        }

        internal void Start()
        {
            QueryPerformanceCounter(out start);
        }

        internal void Stop()
        {
            QueryPerformanceCounter(out stop);
            total += stop - start;
        }

        internal void Reset()
        {
            start = 0;
            stop = 0;
            total = 0;
        }

        internal long Nanoseconds
        {
            get
            {
                return (long)(((double)total * (double)multiplier) / (double)frequency);
            }
        }

        internal long Milliseconds
        {
            get
            {
                return (long)(((double)total * (double)multiplier) / (double)frequency / 1000000.0);
            }
        }

        internal static long QueryTicks()
        {
            long ticks;
            QueryPerformanceCounter(out ticks);
            return ticks;
        }
    }
}
