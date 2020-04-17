using System;

namespace SMRenderer.ManagerIntergration.Base
{
    /// <summary>
    /// Used to report something special to SMManager.
    /// </summary>
    public class DataEntryReport
    {
        /// <summary>
        /// If the SMManager should stop with create a instance, is that true.
        /// </summary>
        public bool IsStop;
        /// <summary>
        /// Contains the action when the loading stops.
        /// </summary>
        public Action StopAction = () => { };

        /// <summary>
        /// Create a report to stop the loading
        /// </summary>
        /// <param name="action">The action what happens after it stopped</param>
        /// <returns>The report</returns>
        public static DataEntryReport StopLoading(Action action)
        {
            return new DataEntryReport
            {
                IsStop = true,
                StopAction = action
            };
        }
    }
}