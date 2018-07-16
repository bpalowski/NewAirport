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
