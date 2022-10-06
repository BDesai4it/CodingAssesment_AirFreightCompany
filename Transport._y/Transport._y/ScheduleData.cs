using System;
using System.Collections.Generic;
using System.Text;

namespace Transport_ly
{
    public class ScheduleData
    {
        public int flight_Num { get; set; } // Flight number must be unique related to the each flight
        public string origin_From { get; set; } // Origin of the flight
        public string destination_To { get; set; } // Destination of the flight
        public int Daynum { get; set; } // this will display the Day Number on which the flights are scheduled
        public bool is_flight_Loaded { get; set; } // this property will be set to true when the flight has 20 boxes loaded in it

    }
}
