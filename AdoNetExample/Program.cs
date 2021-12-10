using System;

namespace AdoNetExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FlightRepository repository = new FlightRepository();
            //repository.GetFlights();
            Flight flight = new Flight
            {
                Number = "OU124",
                Date = new DateTime().Date,
                AirportTo = "ZAG",
                Carrier = "CTN"
            };
            repository.CreateFlight(flight);
            Console.WriteLine("Hello World!");
        }
    }
}
