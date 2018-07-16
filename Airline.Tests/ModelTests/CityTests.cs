using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Airport.Models;

namespace Airport.Tests
{
  [TestClass]
  public class CityTests : IDisposable
  {
    public void Dispose()
    {
    City.DeleteAll();
    //Flight.DeleteAll();
    }


    public CityTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airport_tests;";
    }

    [TestMethod]
    public void GetAll_CityEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = City.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesCityToDatabase_CityList()
    {
      //Arrange
      City testCity = new City("Seattle", "WA");
      testCity.Save();

      //Act
      List<City> result = City.GetAll();
      List<City> newCity = new List<City>{testCity};

      //Assert
      CollectionAssert.AreEqual(newCity, result);
    }

    [TestMethod]
public void Find_FindsCityInDatabase_Category()
{
  //Arrange
  City testCity = new City("Seattle", "WA");
  testCity.Save();

  //Act
  City foundCity = City.Find(testCity.GetId());

  //Assert
  Assert.AreEqual(testCity, foundCity);
}



}
}
