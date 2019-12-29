using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    /// <summary>
    /// Timer and interval-system connected with the renderer to save performance
    /// </summary>
    public class Timer
    {
        public delegate void TimerEventArgs(Timer sender);

        /// <summary>
        /// Contains all timers
        /// </summary>
        static List<Timer> timers = new List<Timer>();

        /// <summary>
        /// How long should the timer take
        /// </summary>
        public TimeSpan time;

        /// <summary>
        /// Tell you how long does it take to trigger the finish event
        /// </summary>
        public double currentTime { get; private set; }

        /// <summary>
        /// It is a interval or only a normal timer?
        /// </summary>
        public bool Interval = false;

        /// <summary>
        /// Tell you if the timer active or not
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        /// Occurs when the timer reached the end or the interval begins new
        /// </summary>
        public event TimerEventArgs Finish;

        /// <summary>
        /// Occurs when the currentTime change. !Even if the timer is done after it!
        /// </summary>
        public event TimerEventArgs Tick;

        /// <summary>
        /// The 'clockwork' of the timer. Subtracts the rendertime with the currentTime of all active timers.
        /// </summary>
        /// <param name="rendertime">Requires 'e.Time'</param>
        static public void TickChange(double rendertime)
        {
            timers.ToList().ForEach(a =>
            {
                a.Tick?.Invoke(a);
                if (a.currentTime <= 0) a.Stop(true);
                else a.currentTime -= rendertime;
            });
        }

        /// <summary>
        /// Starts the timer (interval)
        /// </summary>
        public void Start()
        {
            currentTime = time.TotalSeconds;
            timers.Add(this);
            Active = true;
        }

        private void Stop(bool triggered)
        {
            Finish?.Invoke(this);
            if (Interval) currentTime = time.TotalSeconds;
            else Stop();
        }

        /// <summary>
        /// Stops the timer (interval)
        /// </summary>
        public void Stop()
        {
            currentTime = Double.NaN;
            timers.Remove(this);
            Active = false;
        }
    }
}
