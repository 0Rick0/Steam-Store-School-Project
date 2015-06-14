using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SteamUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test the connection to the database
        /// </summary>
        [TestMethod]
        public void TestDbProvider()
        {
            var inst = SteamStore.DbProvider.GetOracleConnection();
            var com = inst.CreateCommand();
            com.CommandText = "SELECT * FROM dual";
            var r = com.ExecuteReader();
            r.Read();
        }
    }
}
