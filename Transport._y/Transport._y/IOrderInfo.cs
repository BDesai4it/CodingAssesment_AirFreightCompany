using System.Collections.Generic;

namespace Transport_ly
{
    internal interface IOrderInfo
    {
        public Dictionary<string, OrderInfo> LoadOrdersInFlights();
    }
}
