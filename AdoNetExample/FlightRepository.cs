using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetExample
{
    public class FlightRepository
    {
        public IEnumerable<Flight> GetFlights()
        {
            IEnumerable<Flight> flights = new List<Flight>();
            var airportSystemConnectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=AirportSystemAdoNetDemo; Integrated Security=true";

            var selectFlightQueryString = "SELECT Id, Number, Date, AirportTo, Carrier from dbo.Flights;";

            //using System.Data.SqlClient;
            using (SqlConnection airportSystemConnection = new SqlConnection(airportSystemConnectionString))
            {
                SqlCommand selectAllFlightsCommand = new SqlCommand(selectFlightQueryString, airportSystemConnection);

                try
                {
                    airportSystemConnection.Open();
                    SqlDataReader flightsReader = selectAllFlightsCommand.ExecuteReader();
                    while (flightsReader.Read())
                    {
                        var flight = new Flight();
                        flight.Id = int.Parse(flightsReader["ID"].ToString());
                        flight.Number = flightsReader["Number"].ToString();
                        flight.Date = DateTime.Parse(flightsReader["Date"].ToString());
                        flight.AirportTo = flightsReader["AirportTo"].ToString();
                        flight.Carrier = flightsReader["Carrier"].ToString();
                        flights.Append(flight);
                    }
                    flightsReader.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return flights;
        }

        public void CreateFlight(Flight flight)
        {
            var airportSystemConnectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=AirportSystemAdoNetDemo; Integrated Security=true";

            var insertFlightQueryString = "INSERT INTO Flights VALUES(@Number, @Date, @AirportTo, @Carrier);";

            //using System.Data.SqlClient;
            using (SqlConnection airportSystemConnection = new SqlConnection(airportSystemConnectionString))
            {
                try
                {
                    SqlCommand insertFlightCommand = new SqlCommand(insertFlightQueryString, airportSystemConnection);
                    insertFlightCommand.Parameters.Add("@Number", SqlDbType.VarChar, 6).Value = flight.Number;
                    insertFlightCommand.Parameters.Add("@Date", SqlDbType.DateTime2, 7).Value = flight.Date;
                    insertFlightCommand.Parameters.Add("@AirportTo", SqlDbType.VarChar, 3).Value = flight.AirportTo;
                    insertFlightCommand.Parameters.Add("@Carrier", SqlDbType.VarChar, 3).Value = flight.Carrier;

                    airportSystemConnection.Open();
                    insertFlightCommand.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                
            }
        }
    }
}

