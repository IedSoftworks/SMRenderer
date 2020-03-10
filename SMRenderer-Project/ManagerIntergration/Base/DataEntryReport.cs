using System;

namespace SMRenderer.ManagerIntergration.Base
{
    public class DataEntryReport
    {
        public bool IsStop;
        public Action StopAction = () => { };

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