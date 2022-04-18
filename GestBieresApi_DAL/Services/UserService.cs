using GestBieresApi_DAL.Entities;
using GestBieresApi_DAL.Mappers;
using ZToolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestBieresApi_DAL.Services
{
    public class UserService
    {
        private readonly Connection _connection;

        public UserService(Connection connection)
        {
            _connection = connection;
        }

        public AppUser Login(string email, string password)
        {
            Command cmd = new Command("LoginUser", true);
            cmd.AddParameter("email", email);
            cmd.AddParameter("passwrd", password);

            try
            {
                AppUser toto = _connection.ExecuteReader(cmd, DataReader.ToUser).First();
                return toto;
            }
            catch (Exception ex)
            {
                throw new Exception("User does not exist");
            }

        }

        public AppUser GetById(int Id)
        {
            Command cmd = new Command("Select * FROM AppUser WHERE Id = @Id");
            cmd.AddParameter("Id", Id);

            return _connection.ExecuteReader(cmd, DataReader.ToUser).FirstOrDefault();
        }

        public IEnumerable<AppUser> GetAll()
        {
            Command cmd = new Command("Select * FROM AppUser");

            return _connection.ExecuteReader(cmd, DataReader.ToUser);
        }
    }
}
