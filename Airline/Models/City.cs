using System.Collections.Generic;
using System;
using Airport;
using MySql.Data.MySqlClient;

namespace Airport.Models
{
  public class City
  {
    private string _city;
    private string _state;
    private int _id;

    public City (string city, string state, int id = 0)
    {
      _city = city;
      _state = state;
      _id = id;
    }

    public override bool Equals(System.Object otherCity)
{
  if (!(otherCity is City))
  {
    return false;
  }
  else
  {
    City newCity = (City) otherCity;
    bool idEquality = this.GetId() == newCity.GetId();
    bool cityEquality = this.GetCity() == newCity.GetCity();
    bool stateEquality = this.GetState() == newCity.GetState();
    return (idEquality && cityEquality && stateEquality);
  }
}
public override int GetHashCode()
{
  return this.GetId().GetHashCode();
}

public string GetCity()
{
  return _city;
}

public string GetState()
{
  return _state;
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
  cmd.CommandText = @"INSERT INTO cities (city, state) VALUES (@cities, @state);";

  MySqlParameter city = new MySqlParameter();
  city.ParameterName = "@cities";
  city.Value = this._city;
  cmd.Parameters.Add(city);

  MySqlParameter state = new MySqlParameter();
  state.ParameterName = "@state";
  state.Value = this._state;
  cmd.Parameters.Add(state);

  cmd.ExecuteNonQuery();
  _id = (int) cmd.LastInsertedId;
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }
}

public static List<City> GetAll()
{
  List<City> allCity = new List<City> {};
  MySqlConnection conn = DB.Connection();
  conn.Open();
  var cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"SELECT * FROM cities;";
  var rdr = cmd.ExecuteReader() as MySqlDataReader;
  while(rdr.Read())
  {
    int cityId = rdr.GetInt32(0);
    string city = rdr.GetString(1);
    string state = rdr.GetString(2);
    City newCity = new City(city, state, cityId);
    allCity.Add(newCity);
  }
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }
  return allCity;
}
public static City Find(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM cities WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int CityId = 0;
        string CityName = "";
        string StateName = "";

        while(rdr.Read())
        {
          CityId = rdr.GetInt32(0);
          CityName = rdr.GetString(1);
          StateName = rdr.GetString(2);
        }
        City newCity = new City(CityName, StateName, CityId);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return newCity;
    }
    public void UpdateCity(string newCity, string newState)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE items SET city - @city, state = @state WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter city = new MySqlParameter();
            city.ParameterName = "@city";
            city.Value = newCity;
            cmd.Parameters.Add(city);

            MySqlParameter state = new MySqlParameter();
            state.ParameterName = "@state";
            state.Value = newState;
            cmd.Parameters.Add(state);

            cmd.ExecuteNonQuery();
            _city = newCity;
            _state = newState;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void AddNewFlight(int cityId, int flightId)
        {
          MySqlConnection conn = DB.Connection();
                  conn.Open();
                  var cmd = conn.CreateCommand() as MySqlCommand;
                  cmd.CommandText = @"INSERT INTO cities_flights (city_id, flight_id) VALUES (@CityId, @FlightId);";

                  MySqlParameter city_id = new MySqlParameter();
                  city_id.ParameterName = "@CityId";
                  city_id.Value = cityId;
                  cmd.Parameters.Add(city_id);

                  MySqlParameter flight_id = new MySqlParameter();
                  flight_id.ParameterName = "@FlightId";
                  flight_id.Value = flightId;
                  cmd.Parameters.Add(flight_id);



                  // MySqlParameter flight_id = new MySqlParameter();
                  // flight_id.ParameterName = "@FlightId";
                  // flight_id.Value = newFlight.GetId();
                  // cmd.Parameters.Add(flight_id);

                  cmd.ExecuteNonQuery();
                  conn.Close();
                  if (conn != null)
                  {
                      conn.Dispose();
                  }
              }

          public static List<Flight> GetFlightsByCity(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT flight_id FROM cities_flights WHERE city_id = @CityId;";

            MySqlParameter cityIdParameter = new MySqlParameter();
            cityIdParameter.ParameterName = "@CityId";
            cityIdParameter.Value = id;
            cmd.Parameters.Add(cityIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<int> departIds = new List<int> {};
            while(rdr.Read())
            {
                int departId = rdr.GetInt32(0);
                departIds.Add(departId);
            }
            rdr.Dispose();

            List<Flight> depart = new List<Flight> {};
            foreach (int departId in departIds)
            {
                var departQuery = conn.CreateCommand() as MySqlCommand;
                departQuery.CommandText = @"SELECT * FROM flights WHERE depart_id = @DepartId;";

                MySqlParameter departIdParameter = new MySqlParameter();
                departIdParameter.ParameterName = "@DepartId";
                departIdParameter.Value = departId;
                departQuery.Parameters.Add(departIdParameter);

                var departQueryRdr = departQuery.ExecuteReader() as MySqlDataReader;
                while(departQueryRdr.Read())
                {
                    int newFlightId = departQueryRdr.GetInt32(0);

                    int newFlightNumber = departQueryRdr.GetInt32(1);
                    string newFlightTime = departQueryRdr.GetString(2);
                    int newFlightDepartId = departQueryRdr.GetInt32(3);
                    int newFlightArriveId = departQueryRdr.GetInt32(4);
                    string newStatus = departQueryRdr.GetString(5);
                    Flight foundFlight = new Flight(newFlightNumber, newFlightTime, newFlightDepartId, newFlightArriveId, newStatus, newFlightId);
                    depart.Add(foundFlight);
                }
                departQueryRdr.Dispose();
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return depart;
             }


        public void Delete()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          MySqlCommand cmd = new MySqlCommand("DELETE FROM cities WHERE id = @CityId; DELETE FROM cities_departs WHERE city_id = @City_id;", conn);
          MySqlParameter categoryIdParameter = new MySqlParameter();
          categoryIdParameter.ParameterName = "@CategoryId";
          categoryIdParameter.Value = this.GetId();

          cmd.Parameters.Add(categoryIdParameter);
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
    cmd.CommandText = @"DELETE FROM cities;";
    cmd.ExecuteNonQuery();
    conn.Close();
    if (conn != null)
    {
        conn.Dispose();
    }
}




  }
}
