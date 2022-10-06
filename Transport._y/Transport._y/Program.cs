using System;
using System.Collections.Generic;

namespace Transport_ly
{
    internal class Program
    {
        public static FlightInfo flights = new FlightInfo();
        public static OrderInfo orders = new OrderInfo();
        public static Dictionary<int, ScheduleData> FlightSchedules = new Dictionary<int, ScheduleData>();
        static void Main(string[] args)
        {
            Console.WriteLine("############### Welcome to Transport.ly ###################");
            // Initialize the flight schedule
            flights.InitializeSchedule(FlightSchedules);
            DisplayMainMenu();
            Console.ReadKey();
        }
        private static void DisplayMainMenu()
        {
            Console.WriteLine("::::: Main Menu :::::");
            Console.WriteLine("1. Display UnScheduled Flights");
            Console.WriteLine("2. Display Scheduled Flights");
            Console.WriteLine("3. Load Orders to the Scheduled Flights");
            Console.WriteLine("4. Reset Schedule");
            Console.WriteLine("5. Exit");
            MenuSelection(Convert.ToInt32(Console.ReadLine()));
        }

        private static void MenuSelection(int user_input)
        {
            try
            {
                switch (user_input)
                {
                    case 1: // Diplay all Unscheduled Flights
                        flights.DisplaySchedule(FlightSchedules, false);
                        break;
                    case 2: // Display the Schedules Flights
                        Console.WriteLine("Select the flight number that you want to load from the schedule");
                        if (flights.LoadfileforSchedule(Convert.ToInt32(Console.ReadLine()), FlightSchedules))
                        {
                            flights.DisplaySchedule(FlightSchedules, true);
                        }
                        else
                        {
                            Console.WriteLine(Environment.NewLine);
                        }
                        break;
                    case 3:
                        // Load the orders to the loaded(selected) flights
                        //orders.LoadOrdersInFlights();
                        flights.LoadOrderstoScheduledFlights(orders.LoadOrdersInFlights(), FlightSchedules);
                        break;
                    case 4:
                        // Reset all the Flights and make them available for scheduling
                        flights.ResetAllParameters(FlightSchedules);
                        Console.WriteLine("All Flights are reset to unscheduled flights !");
                        flights.DisplaySchedule(FlightSchedules, false); // Display all unscheduled flights
                        Console.WriteLine(Environment.NewLine);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default: Console.WriteLine("Please select a valid selection"); break;
                }
                DisplayMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ibn MenuSelection : " + ex.Message);
            }
        }
    }
}
