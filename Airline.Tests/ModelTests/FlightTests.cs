using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Airport.Models;

namespace Airport.Tests
{
  [TestClass]
  public class FlightTests : IDisposable
  {
    public void Dispose()
    {
      City.DeleteAll();
    Flight.DeleteAll();
    }


    public FlightTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airport_tests;";
    }

    [TestMethod]
    public void GetAll_FlightEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Flight.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesCityToDatabase_FlightList()
    {
      //Arrange
      Flight testFlight = new Flight(1, "Seattle", 1, 1, "WA");
      testFlight.Save();

      //Act
      List<Flight> result = Flight.GetAll();
      List<Flight> newFlight = new List<Flight>{testFlight};

      //Assert
      CollectionAssert.AreEqual(newFlight, result);
    }

    [TestMethod]
public void Find_FindsCityInDatabase_Category()
{
  //Arrange
  Flight testFlight = new Flight(1, "Seattle", 1, 1, "WA");
  testFlight.Save();

  //Act
  Flight foundFlight = Flight.Find(testFlight.GetId());

  //Assert
  Assert.AreEqual(testFlight, foundFlight);
}



}
}
