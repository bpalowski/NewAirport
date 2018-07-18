using System.Collections.Generic;
using System;
using Airport;
using MySql.Data.MySqlClient;

namespace Airport.Models
{
  public class Flight
  {
    private int  _flight_number;
    private string _depart_time;
    private int _depart_id;
    private int _arrive_id;
    private string _status;
    private int _id;

    public Flight(int flightNumber, string departTime, int departId, int arriveId, string status, int id = 0)
    {
      _flight_number = flightNumber;
      _depart_time = departTime;
      _depart_id = departId;
      _arrive_id = arriveId;
      _status = status;
      _id = id;
    }

    public override bool Equals(System.Object otherFlight)
    {
      if (!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool idEquality = this.GetId() == newFlight.GetId();
        bool flightNumberEquality = this.GetFlightNumber() == newFlight.GetFlightNumber();
        bool departEquality = this.GetDepartTime() == newFlight.GetDepartTime();
        bool departIdEquality = this.GetArriveId() == newFlight.GetArriveId();
        bool arriveIdEquality = this.GetDepartId() == newFlight.GetDepartId();
        bool statusEquality = this.GetStatus() == newFlight.GetStatus();
        return (flightNumberEquality && departEquality && arriveIdEquality && statusEquality && idEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public int GetFlightNumber()
    {
      return _flight_number;
    }
    public string GetDepartTime()
    {
      return _depart_time;
    }
    public int GetDepartId()
    {
      return _depart_id;
    }
    public int GetArriveId()
    {
      return _arrive_id;
    }
    public string GetStatus()
    {
      return _status;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO flights (flight_number, depart_time, depart_id, arrive_id, status) VALUES (@flight_number, @depart_time, @depart_id, @arrive_id, @status);";

      MySqlParameter flightNumber = new MySqlParameter();
      flightNumber.ParameterName = "@flight_number";
      flightNumber.Value = this._flight_number;
      cmd.Parameters.Add(flightNumber);

      MySqlParameter departTime = new MySqlParameter();
      departTime.ParameterName = "@depart_time";
      departTime.Value = this._depart_time;
      cmd.Parameters.Add(departTime);

      MySqlParameter departId = new MySqlParameter();
      departId.ParameterName = "@depart_id";
      departId.Value = this._depart_id;
      cmd.Parameters.Add(departId);

      MySqlParameter arriveId = new MySqlParameter();
      arriveId.ParameterName = "@arrive_id";
      arriveId.Value = this._arrive_id;
      cmd.Parameters.Add(arriveId);

      MySqlParameter status = new MySqlParameter();
      status.ParameterName = "@status";
      status.Value = this._status;
      cmd.Parameters.Add(status);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Flight> GetAll()
    {
      List<Flight> allCity = new List<Flight> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM flights;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        int flightNumber = rdr.GetInt32(1);
        string flightTime = rdr.GetString(2);
        int flightDepartId = rdr.GetInt32(3);
        int flightArriveId = rdr.GetInt32(4);
        string status = rdr.GetString(5);

        Flight newFlight = new Flight(flightNumber, flightTime, flightDepartId, flightArriveId, status, flightId);
        allCity.Add(newFlight);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCity;
    }

    public static Flight Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM flights WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int flightId = 0;
      int flightNumber = 0;
      string flightTime = "";
      int flightDepartId = 0;
      int flightArriveId = 0;
      string status = "";

      while(rdr.Read())
      {
        flightId = rdr.GetInt32(0);
        flightNumber = rdr.GetInt32(1);
        flightTime = rdr.GetString(2);
        flightDepartId = rdr.GetInt32(3);
        flightArriveId = rdr.GetInt32(4);
        status = rdr.GetString(5);
      }
      Flight newFlight = new Flight(flightNumber, flightTime, flightDepartId, flightArriveId, status, flightId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newFlight;
    }
    public void UpdateFlight(int newFlight, string newDepartTime, string newArriveTime, int newDepartId, int newArriveId, string newStatus)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE flights SET flight_number = @flight_number, depart_time = @depart_time, depart_id = @depart_id, arrive_id = @arrive_id, status = @status WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter flightnumber = new MySqlParameter();
      flightnumber.ParameterName = "@flight_number";
      flightnumber.Value = newFlight;
      cmd.Parameters.Add(flightnumber);

      MySqlParameter departtime = new MySqlParameter();
      departtime.ParameterName = "@depart_time";
      departtime.Value = newDepartTime;
      cmd.Parameters.Add(departtime);

      MySqlParameter departid = new MySqlParameter();
      departid.ParameterName = "@depart_id";
      departid.Value = newDepartId;
      cmd.Parameters.Add(departid);

      MySqlParameter arriveid = new MySqlParameter();
      arriveid.ParameterName = "@arrive_id";
      arriveid.Value = newArriveId;
      cmd.Parameters.Add(arriveid);

      MySqlParameter status = new MySqlParameter();
      status.ParameterName = "@status";
      status.Value = newStatus;
      cmd.Parameters.Add(status);

      cmd.ExecuteNonQuery();
      _flight_number = newFlight;
      _depart_time = newDepartTime;

      _arrive_id = newArriveId;
      _status = newStatus;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }



    // public static List<Flight> FindFlightsByCity(int id)
    //     {
    //       List<Flight> allFlights = new List<Flight> {};
    //         MySqlConnection conn = DB.Connection();
    //         conn.Open();
    //         var cmd = conn.CreateCommand() as MySqlCommand;
    //         cmd.CommandText = @"SELECT * FROM flights WHERE depart_id = (@searchId);";
    //
    //         MySqlParameter searchId = new MySqlParameter();
    //         searchId.ParameterName = "@searchId";
    //         searchId.Value = id;
    //         cmd.Parameters.Add(searchId);
    //
    //         var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //         int flightId = 0;
    //         int flightNumber = 0;
    //         string flightTime = "";
    //         int flightDepartId = 0;
    //         int flightArriveId = 0;
    //         string status = "";
    //
    //         while(rdr.Read())
    //         {
    //           flightId = rdr.GetInt32(0);
    //           flightNumber = rdr.GetInt32(1);
    //           flightTime = rdr.GetString(2);
    //           flightDepartId = rdr.GetInt32(3);
    //           flightArriveId = rdr.GetInt32(4);
    //           status = rdr.GetString(5);
    //
    //           Flight newFlight = new Flight(flightNumber, flightTime, flightDepartId, flightArriveId, status, flightId);
    //           allFlights.Add(newFlight);
    //
    //         }
    //         conn.Close();
    //         if (conn != null)
    //         {
    //             conn.Dispose();
    //         }
    //         return allFlights;
    //     }


    // public static List<Flight> FindFlightsByCity(int id)
    // {
    //   List<Flight> allFlights = new List<Flight> {};
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM flights WHERE depart_id = (@searchId);";
    //
    //   MySqlParameter searchId = new MySqlParameter();
    //   searchId.ParameterName = "@searchId";
    //   searchId.Value = id;
    //   cmd.Parameters.Add(searchId);
    //
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   int flightId = 0;
    //   int flightNumber = 0;
    //   string flightTime = "";
    // //  int flightDepartId = 0;
    //   int flightArriveId = 0;
    //   string status = "";
    //
    //   while(rdr.Read())
    //   {
    //     flightId = rdr.GetInt32(0);
    //     flightNumber = rdr.GetInt32(1);
    //     flightTime = rdr.GetString(2);
    // //    flightDepartId = rdr.GetInt32(3);
    //     flightArriveId = rdr.GetInt32(3);
    //     status = rdr.GetString(4);
    //
    //     Flight newFlight = new Flight(flightNumber, flightTime, flightArriveId, status, flightId);
    //
    //     allFlights.Add(newFlight);
    //
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return allFlights;
    // }

    // public void AddNewCity(City newCity)
    // {
    //   MySqlConnection conn = DB.Connection();
    //           conn.Open();
    //           var cmd = conn.CreateCommand() as MySqlCommand;
    //           cmd.CommandText = @"INSERT INTO cities_flights (city_id, flight_id) VALUES (@CityId, @FlightId);";
    //
    //           MySqlParameter city_id = new MySqlParameter();
    //           city_id.ParameterName = "@CityId";
    //           city_id.Value = newCity.GetId();
    //           cmd.Parameters.Add(city_id);
    //
    //           MySqlParameter flight_id = new MySqlParameter();
    //           flight_id.ParameterName = "@FlightId";
    //           flight_id.Value = _id;
    //           cmd.Parameters.Add(flight_id);
    //
    //           cmd.ExecuteNonQuery();
    //           conn.Close();
    //           if (conn != null)
    //           {
    //               conn.Dispose();
    //           }
    //       }

          public List<City> GetCityByflights()
            {
              MySqlConnection conn = DB.Connection();
                      conn.Open();
                      var cmd = conn.CreateCommand() as MySqlCommand;
                      cmd.CommandText = @"SELECT city_id FROM cities_flights WHERE flight_id = @FlightId;";

                      MySqlParameter flightIdParameter = new MySqlParameter();
                      flightIdParameter.ParameterName = "@FlightId";
                      flightIdParameter.Value = _id;
                      cmd.Parameters.Add(flightIdParameter);

                      var rdr = cmd.ExecuteReader() as MySqlDataReader;

                      List<int> cityIds = new List<int> {};
                      while(rdr.Read())
                      {
                          int cityId = rdr.GetInt32(0);
                          cityIds.Add(cityId);
                      }
                      rdr.Dispose();

                List<City> cities = new List<City> {};
                foreach (int cityId in cityIds)
                      {
                          var cityQuery = conn.CreateCommand() as MySqlCommand;
                          cityQuery.CommandText = @"SELECT * FROM cities WHERE id = @CityId;";

                          MySqlParameter cityIdParameter = new MySqlParameter();
                          cityIdParameter.ParameterName = "@CityId";
                          cityIdParameter.Value = cityId;
                          cityQuery.Parameters.Add(cityIdParameter);

                          var cityQueryRdr = cityQuery.ExecuteReader() as MySqlDataReader;
                          while(cityQueryRdr.Read())
                          {
                              int thisCityId = cityQueryRdr.GetInt32(0);
                              string cityName = cityQueryRdr.GetString(1);
                              string stateName = cityQueryRdr.GetString(2);
                              City foundCity = new City(cityName,stateName, thisCityId);
                              cities.Add(foundCity);
                          }
                          cityQueryRdr.Dispose();
                      }
                      conn.Close();
                      if (conn != null)
                      {
                          conn.Dispose();
                      }
                      return cities;
                  }







    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM flights WHERE id = @FlightId; DELETE FROM cities_flights WHERE flight_id = @FlightId;";

      MySqlParameter flightIdParameter = new MySqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = this.GetId();
      cmd.Parameters.Add(flightIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM flights;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }




  }
}
