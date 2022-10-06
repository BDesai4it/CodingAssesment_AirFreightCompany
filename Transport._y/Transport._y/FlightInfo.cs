using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transport_ly
{
    public class FlightInfo : IFlightInfo
    {
        private int flight_Capacity { get; set; } = 20; // flight capacity set to 20

        private Dictionary<int, ScheduleData> FlightSchedules = new Dictionary<int, ScheduleData>();
        private Dictionary<string, List<OrderInfo>> OrderInfosByDes = new Dictionary<string, List<OrderInfo>>();
        private SortedDictionary<int, OrderInfo> OrderInfoByPriority = new SortedDictionary<int, OrderInfo>();
        public Dictionary<int, ScheduleData> InitializeSchedule(Dictionary<int, ScheduleData> flightSchedules)
        {
            try
            {
                // This function load all the initial schedules
                int initial_flight_num = 0;
                for (int day = 1; day <= 2; day++)
                {
                    initial_flight_num++;
                    flightSchedules.Add(initial_flight_num, new ScheduleData { Daynum = day, flight_Num = initial_flight_num, origin_From = "YUL", destination_To = "YYZ", is_flight_Loaded = false });
                    initial_flight_num++;
                    flightSchedules.Add(initial_flight_num, new ScheduleData { Daynum = day, flight_Num = initial_flight_num, origin_From = "YUL", destination_To = "YYC", is_flight_Loaded = false });
                    initial_flight_num++;
                    flightSchedules.Add(initial_flight_num, new ScheduleData { Daynum = day, flight_Num = initial_flight_num, origin_From = "YUL", destination_To = "YVR", is_flight_Loaded = false });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Initializing the schedule : " + ex.Message);
            }
            return flightSchedules;
        }
        public void DisplaySchedule(Dictionary<int, ScheduleData> scheduleDatas, bool isloaded)
        {
            try
            {
                // this will display the schedules whether it is loaded or unloaded
                if (isloaded)
                {
                    Console.WriteLine(Environment.NewLine + ":::::::: List of Scheduled Flights ::::::::");
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + ":::::::: List of Unscheduled Flights ::::::::");
                }
                foreach (var keyvalpair in scheduleDatas)
                {
                    if (keyvalpair.Value.is_flight_Loaded == isloaded)
                    {
                        Console.WriteLine("Flight: " + keyvalpair.Value.flight_Num + ", departure: " + keyvalpair.Value.origin_From + ", arrival: " + keyvalpair.Value.destination_To + ", day: " + keyvalpair.Value.Daynum);
                    }
                }
                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Dispalying unloaded schedule : " + ex.Message);
            }
        }
        public bool LoadfileforSchedule(int flightnum, Dictionary<int, ScheduleData> flightSchedules)
        {
            try
            {
                // load the flight that passed by the user to this function
                if (flightSchedules.TryGetValue(flightnum, out ScheduleData scheduleData))
                {
                    scheduleData.is_flight_Loaded = true;
                    return true;
                }
                else
                {
                    Console.WriteLine("Flight " + flightnum + " doesn't exists !");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in LoadfileforSchedule : " + ex.Message);
                return false;
            }
            
        }
        public void ResetAllParameters(Dictionary<int, ScheduleData> flightSchedules)
        {
            try
            {
                flightSchedules.Clear();
                InitializeSchedule(flightSchedules);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in LoadfileforSchedule : " + ex.Message);
            }
            
        }
        public void LoadOrderstoScheduledFlights(Dictionary<string, OrderInfo> OrderData, Dictionary<int, ScheduleData> FlightSchedule)
        {
            try
            {
                OrderInfosByDes.Clear();
                foreach (KeyValuePair<string, OrderInfo> kvp in OrderData)
                {
                    if (!OrderInfosByDes.ContainsKey(kvp.Value.destination))
                    {
                        OrderInfosByDes.Add(kvp.Value.destination, new List<OrderInfo> { kvp.Value });
                    }
                    else
                    {
                        OrderInfosByDes[kvp.Value.destination].Add(kvp.Value);
                    }
                }
                foreach (KeyValuePair<int, ScheduleData> kvp in FlightSchedule)
                {
                    if (kvp.Value.is_flight_Loaded)
                    {
                        // flight is scheduled
                        List<OrderInfo> listofOrders = new List<OrderInfo>();
                        if (OrderInfosByDes.ContainsKey(kvp.Value.destination_To))
                        {
                            // Sorted list when the destination matched with the scheduled flight
                            listofOrders = OrderInfosByDes[kvp.Value.destination_To].OrderBy(a => a.PriorityNum).ToList();
                            for (int i = 1; i <= flight_Capacity; i++)
                            {
                                for (int j = 0; j < listofOrders.Count; j++)
                                {
                                    if (!listofOrders[j].Is_Order_Scheduled)
                                    {
                                        listofOrders[j].Is_Order_Scheduled = true;
                                        listofOrders[j].flightNum = kvp.Value.flight_Num;
                                        break;
                                    }
                                }
                            }
                            OrderInfosByDes[kvp.Value.destination_To] = listofOrders;
                        }
                    }
                }
                OrderInfoByPriority.Clear();
                foreach (KeyValuePair<string, List<OrderInfo>> kvp in OrderInfosByDes)
                {
                    foreach (OrderInfo order in kvp.Value)
                    {
                        if (!OrderInfoByPriority.ContainsKey(order.PriorityNum))
                        {
                            OrderInfoByPriority.Add(order.PriorityNum, order);
                        }
                    }
                }
                DisplayOrderData(OrderInfoByPriority, FlightSchedule);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in LoadOrderstoScheduledFlights : " + ex.Message);
            }
        }
        public void DisplayOrderData(SortedDictionary<int, OrderInfo> orderInfoByPriority, Dictionary<int, ScheduleData> flightSchedule)
        {
            try
            {
                foreach (KeyValuePair<int, OrderInfo> kvp in orderInfoByPriority)
                {
                    OrderInfo order = kvp.Value;
                    if (order.Is_Order_Scheduled)
                    {
                        ScheduleData getScheduleData = flightSchedule[order.flightNum];
                        //order: order-001, flightNumber: 1, departure: <departure_city>, arrival: <arrival_city>, day: x
                        Console.WriteLine("order: " + order.OrderNum + ", flightNumber: " + order.flightNum + ", departure: " + getScheduleData.origin_From + ", arrival: " + getScheduleData.destination_To + ", day: " + getScheduleData.Daynum);
                    }
                    else
                    {
                        //order: order-X, flightNumber: not scheduled
                        Console.WriteLine("order: " + order.OrderNum + ", flightNumber: not scheduled");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DisplayOrderData : " + ex.Message);
            }
        }
    }
}

