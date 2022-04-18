using System.Data;
using GestBieresApi_DAL.Entities;

namespace GestBieresApi_DAL.Mappers
{
    internal static class DataReader
    {
        internal static Biere ToBeer (this IDataReader reader)
        {
            return new Biere()
            {
                Id = (int)reader["Id"],
                Nom = (string)reader["Nom"],
                Degre = (decimal)reader["Degre"],
                Origine = (string)reader["Origine"],             
            };
        }

        internal static AppUser ToUser(this IDataReader reader)
        {
            return new AppUser
            {
                Id = (int)reader["Id"],
                Email = (string)reader["Email"],
                Nickname = (string)reader["Nickname"],
                IsAdmin = (bool)reader["isAdmin"]
            };
        }
    }
}
