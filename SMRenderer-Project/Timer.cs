using System;
using System.Collections.Generic;
using System.Linq;
using static System.Double;

namespace SMRenderer
{
    /// <summary>
    ///     Timer and interval-system connected with the renderer to save performance
    /// </summary>
    public class Timer
    {
        public delegate void TimerEventArgs(Timer sender);

        /// <summary>
        ///     Contains all timers
        /// </summary>
        private static readonly List<Timer> _timers = new List<Timer>();

        /// <summary>
        ///     It is a interval or only a normal timer?
        /// </summary>
        public bool Interval = false;

        /// <summary>
        ///     How long should the timer take
        /// </summary>
        public TimeSpan time;

        /// <summary>
        ///     Tell you how long does it take to trigger the finish event
        /// </summary>
        public double CurrentTime { get; private set; }

        /// <summary>
        ///     Tell you if the timer active or not
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        ///     Occurs when the timer reached the end or the interval begins new
        /// </summary>
        public event TimerEventArgs Finish;

        /// <summary>
        ///     Occurs when the currentTime change. !Even if the timer is done after it!
        /// </summary>
        public event TimerEventArgs Tick;

        /// <summary>
        ///     The 'clockwork' of the timer. Subtracts the renderTime with the currentTime of all active timers.
        /// </summary>
        /// <param name="rendertime">Requires 'e.Time'</param>
        public static void TickChange(double rendertime)
        {
            _timers.ToList().ForEach(a =>
            {
                a.Tick?.Invoke(a);
                if (a.CurrentTime <= 0) a.Stop(true);
                else a.CurrentTime -= rendertime;
            });
        }

        /// <summary>
        ///     Starts the timer (interval)
        /// </summary>
        public void Start()
        {
            CurrentTime = time.TotalSeconds;
            _timers.Add(this);
            Active = true;
        }

        private void Stop(bool triggered)
        {
            Finish?.Invoke(this);
            if (Interval) CurrentTime = time.TotalSeconds;
            else Stop();
        }

        /// <summary>
        ///     Stops the timer (interval)
        /// </summary>
        public void Stop()
        {
            CurrentTime = NaN;
            _timers.Remove(this);
            Active = false;
        }
    }
}