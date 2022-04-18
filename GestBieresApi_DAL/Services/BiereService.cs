using GestBieresApi_DAL;
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
    public class BiereService
    {
        private readonly Connection _connection;

        public BiereService(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Biere> Get()
        {
            Command command = new Command("Select Id, Nom, Degre, Origine FROM Bieres;");
            return _connection.ExecuteReader(command, dr => dr.ToBeer());
        }

        public Biere? Get(int id)
        {
            Command command = new Command("Select Id, Nom, Degre, Origine FROM Bieres WHERE Id = @Id;");
            command.AddParameter("id", id);

            return _connection.ExecuteReader(command, dr => dr.ToBeer()).SingleOrDefault();
        }

        public int Insert(Biere entity)
        {
            Command command = new Command("Insert Into Bieres (Nom, Degre, Origine) OUTPUT Inserted.Id Values (@Nom, @Degre, @Origine);");
            command.AddParameter("Nom", entity.Nom);
            command.AddParameter("Degre", entity.Degre);
            command.AddParameter("Origine", entity.Origine);
           
            int? id = (int?)_connection.ExecuteScalar(command);

            if (!id.HasValue)
                throw new OperationCanceledException("Something wrong with database...");

            return id.Value;
        }

        public bool Update(Biere entity)
        {
            Command command = new Command("Update Bieres Set Nom = @Nom, Degre = @Degre, Origine = @Origine WHERE Id = @Id");
            command.AddParameter("Id", entity.Id);
            command.AddParameter("Nom", entity.Nom);
            command.AddParameter("Degre", entity.Degre);
            command.AddParameter("Origine", entity.Origine);
         
            return _connection.ExecuteNonQuery(command) == 1;
        }

        public Biere? Delete(int id)
        {
            Command command = new Command("Delete From Bieres OUTPUT Deleted.* WHERE Id = @Id");
            command.AddParameter("Id", id);

            return _connection.ExecuteReader(command, dr => dr.ToBeer()).SingleOrDefault();
        }
    }
}
