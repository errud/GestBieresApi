using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestBieresApi_DAL.Services;
using GestBieresApi_DAL.Entities;
using System.Data.SqlClient;
using ZToolbox;
using System.Collections.Generic;
using System.Linq;

namespace ZzBiereApiTest
{
    [TestClass]
    public class BiereServiceTest
    {
 
        private const string CONNECTION_STRING = @"Data Source=5210;Initial Catalog=NetGestContactApiDB;Integrated Security=True;";
        private BiereService _biereService;

        [TestInitialize]
        public void Initialize()
        {
            Connection connection = new Connection(CONNECTION_STRING, SqlClientFactory.Instance);
            _biereService = new BiereService(connection);
        }

        [TestMethod]
        public void TestGet()
        {
            IEnumerable<Biere> bieres = _biereService.Get();
            Assert.AreEqual(bieres.Count(), 2);
        }
    }
    
}