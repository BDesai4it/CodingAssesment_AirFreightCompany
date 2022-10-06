using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Transport_ly
{
    public class OrderInfo : IOrderInfo
    {
        public int PriorityNum { get; set; } // Order Number sequence that represents the priority order
        public string OrderNum { get; set; }
        public int flightNum { get; set; } = -1;
        public string destination { get; set; }
        public bool Is_Order_Scheduled { get; set; }


        public Dictionary<string,OrderInfo> LoadOrdersInFlights()
        {
            try
            {
                // Load Order For the first time without scheduling it
                Dictionary<string, OrderInfo> LoadOrders = JsonConvert.DeserializeObject<Dictionary<string, OrderInfo>>(File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "OrderData\\coding-assigment-orders.json")));
                LoadOrders.AsParallel().ForAll(pair => pair.Value.PriorityNum = int.Parse(pair.Key.Substring(pair.Key.LastIndexOf('-') + 1)));
                LoadOrders.AsParallel().ForAll(pair => pair.Value.OrderNum = pair.Key);
                return LoadOrders;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in LoadOrdersInFlights : " + ex.Message);
                return null;
            }
            
        }
    }
}
