using System;
using System.Collections.Generic;
using System.Text;

namespace Transport_ly
{
    internal interface IFlightInfo
    {
        public Dictionary<int, ScheduleData> InitializeSchedule(Dictionary<int, ScheduleData> flightSchedules);
        public void DisplaySchedule(Dictionary<int, ScheduleData> scheduleDatas, bool isloaded);
        public bool LoadfileforSchedule(int flightnum, Dictionary<int, ScheduleData> flightSchedules);
        public void ResetAllParameters(Dictionary<int, ScheduleData> flightSchedules);
        public void LoadOrderstoScheduledFlights(Dictionary<string, OrderInfo> OrderData, Dictionary<int, ScheduleData> FlightSchedule);
        public void DisplayOrderData(SortedDictionary<int, OrderInfo> orderInfoByPriority, Dictionary<int, ScheduleData> flightSchedule);
    }
}
