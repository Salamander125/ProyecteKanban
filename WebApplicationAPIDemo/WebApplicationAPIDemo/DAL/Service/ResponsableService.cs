using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPIDemo.Model;
using WebApplicationAPIDemo.Persistence;

namespace WebApplicationAPIDemo.DAL.Service
{
    public class ResponsableService
    {
        /// <summary>
        /// Obté totes els responsables
        /// </summary>
        public List<Responsable> GetAll()
        {
            var result = new List<Responsable>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Responsable";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Responsable
                            {
                                Codi = Convert.ToInt32(reader["Codi"].ToString()),
                                Usuari = reader["Usuari"].ToString(),
                                Contrasenya = reader["Contrasenya"].ToString(),
                                Admin = Convert.ToBoolean(reader["Admin"]),
                            });
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Obté un responsable segons l'ID
        /// </summary>
        public Responsable GetById(int Codi)
        {
            Responsable responsable = null;

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Responsable WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Codi", Codi));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            responsable = new Responsable()
                            {
                                Codi = Convert.ToInt32(reader["Codi"].ToString()),
                                Usuari = reader["Usuari"].ToString(),
                                Contrasenya = reader["Contrasenya"].ToString(),
                                Admin = Convert.ToBoolean(reader["Admin"]),
                            };
                        }
                    }
                }
            }
            return responsable;
        }

        /// <summary>
        /// Obté la contrasenya d'un responsable segons l'username que li passem
        /// </summary>
        public string GetPassword(string Usuari)
        {
            string Contrasenya = null;

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT Contrasenya FROM Responsable WHERE Usuari = @Usuari";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Usuari", Usuari));
                    using (var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {                           
                            Contrasenya = reader["Contrasenya"].ToString();                        
                        }
                    }
                }
            }
            return Contrasenya;
        }

        /// <summary>
        /// Afegeix un nou responsable a la base de dades
        /// </summary>
        public Responsable Add(Responsable responsable)
        {
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Responsable (Usuari, Contrasenya, Admin) VALUES (@Usuari, @Contrasenya, @Admin)";
                using (var command = new System.Data.SQLite.SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Usuari", responsable.Usuari));
                    command.Parameters.Add(new SQLiteParameter("Contrasenya", responsable.Contrasenya));
                    command.Parameters.Add(new SQLiteParameter("Admin", responsable.Admin));

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";

                    responsable.Codi = (Int64)command.ExecuteScalar();
                }
            }

            return responsable;
        }

        /// <summary>
        /// Actualitza la contrasenya del responsable
        /// </summary>
        public int UpdateContrasenya(Responsable responsable)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE Responsable SET Contrasenya = @Contrasenya WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Contrasenya", responsable.Contrasenya));
                    command.Parameters.Add(new SQLiteParameter("Codi", responsable.Codi));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        /// <summary>
        /// Elimina un responsable
        /// </summary>
        public int Delete(int Codi)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM Responsable WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Codi", Codi));
                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }
    }
}
