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
    bool departEquality = this.GetDepart() == newFlight.GetDepart();
    bool departIdEquality = this.GetDepartId() == newFlight.GetDepartId();
    bool arriveIdEquality = this.GetArriveId() == newFlight.GetArriveId();
    bool statusEquality = this.GetStatus() == newFlight.GetStatus();
    return (idEquality && flightNumberEquality && departEquality && departIdEquality && arriveIdEquality && statusEquality);
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
public string GetDepart()
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
  cmd.CommandText = @"INSERT INTO flights (flight_number, depart_time, depart_Id, arrive_id, status) VALUES (@flight_number, @depart_time, @depart_Id, @arrive_id, @status);";

  MySqlParameter flightNumber = new MySqlParameter();
  flightNumber.ParameterName = "@flight_number";
  flightNumber.Value = this._flight_number;
  cmd.Parameters.Add(flightNumber);

  MySqlParameter departTime = new MySqlParameter();
  departTime.ParameterName = "@depart_time";
  departTime.Value = this._depart_time;
  cmd.Parameters.Add(departTime);

  MySqlParameter departId = new MySqlParameter();
  departId.ParameterName = "@depart_Id";
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

    public static List<Flight> FindFlightsByCity(int id)
        {
          List<Flight> allFlights = new List<Flight> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM flights WHERE depart_id = (@searchId);";

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

              Flight newFlight = new Flight(flightNumber, flightTime, flightDepartId, flightArriveId, status, flightId);
              allFlights.Add(newFlight);

            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allFlights;
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
