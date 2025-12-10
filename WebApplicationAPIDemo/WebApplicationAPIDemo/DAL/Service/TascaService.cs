using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPIDemo.Model;
using WebApplicationAPIDemo.Persistence;

namespace WebApplicationAPIDemo.DAL.Service
{
    public class TascaService
    {
        /// <summary>
        /// Obté totes les tasques
        /// </summary>
        /// <returns></returns>
        public List<Tasca> GetAll()
        {
            var result = new List<Tasca>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Tasca";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Tasca
                            {
                                Codi = Convert.ToInt64(reader["Codi"].ToString()),
                                Titol = reader["Titol"].ToString(),
                                Descripcio = reader["Descripcio"].ToString(),
                                Data_creacio = Convert.ToDateTime(reader["Data_Creacio"]),
                                Data_finalitzacio = Convert.ToDateTime(reader["Data_Finalitzacio"]),
                                Prioritat = Convert.ToInt32(reader["Prioritat"].ToString()),
                                Estat = Convert.ToInt32(reader["Estat"].ToString()),
                                Codi_responsable = Convert.ToInt64(reader["Codi_responsable"].ToString()),
                            });
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Obté les dades de la tasca
        /// </summary>
        /// <param name="Id">Identificador d'usuari</param>
        /// <returns>Dades de l'Usuari</returns>
        public Tasca GetById(int Codi)
        {
            Tasca tasca = null;

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Tasca WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Codi", Codi));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasca = new Tasca()
                            {
                                Codi = Convert.ToInt64(reader["Codi"].ToString()),
                                Titol = reader["Titol"].ToString(),
                                Descripcio = reader["Descripcio"].ToString(),
                                Data_creacio = Convert.ToDateTime(reader["Data_creacio"]),
                                Data_finalitzacio = Convert.ToDateTime(reader["Data_finalitzacio"]),
                                Prioritat = Convert.ToInt32(reader["Prioritat"].ToString()),
                                Estat = Convert.ToInt32(reader["Estat"].ToString()),
                                Codi_responsable = Convert.ToInt64(reader["Codi_responsable"].ToString()),
                            };
                        }
                    }
                }
            }
            return tasca;
        }

        /// <summary>
        /// Afegeix un nou usuari a la base de dades
        /// </summary>
        /// <param name="tasca">Entitat usuari</param>
        /// <returns>Id de l'usuari afegit</returns>
        public Tasca Add(Tasca tasca)
        {
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Tasca (Titol, Descripcio, Data_creacio, Data_finalitzacio, Prioritat, Estat) VALUES (@Titol, @Descripcio, @Data_creacio, @Data_finalitzacio, @Prioritat, @Estat)";
                using (var command = new System.Data.SQLite.SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Titol", tasca.Titol));
                    command.Parameters.Add(new SQLiteParameter("Descripcio", tasca.Descripcio));
                    command.Parameters.Add(new SQLiteParameter("Data_creacio", tasca.Data_creacio));
                    command.Parameters.Add(new SQLiteParameter("Data_finalitzacio", tasca.Data_finalitzacio));
                    command.Parameters.Add(new SQLiteParameter("Prioritat", tasca.Prioritat));
                    command.Parameters.Add(new SQLiteParameter("Estat", tasca.Estat));

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";

                    tasca.Codi = (Int64)command.ExecuteScalar();
                }
            }

            return tasca;
        }

        /// <summary>
        /// Actualitza la descripcio d'una tasca
        /// </summary>
        /// <param name="tasca">Entitat usuari que es vol modificar</param>
        /// <returns>Files afectades</returns>
        public int UpdateDescripcio(Tasca tasca)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE Tasca SET Descripcio = @Descripcio WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Descripcio", tasca.Descripcio));
                    command.Parameters.Add(new SQLiteParameter("Codi", tasca.Codi));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        /// <summary>
        /// Actualitza el responsable d'una tasca
        /// </summary>
        /// <param name="tasca">Entitat usuari que es vol modificar</param>
        /// <returns>Files afectades</returns>
        public int UpdateResponsable(Tasca tasca)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE Tasca SET Codi_responsable = @Codi_responsable WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Codi_responsable", tasca.Codi_responsable));
                    command.Parameters.Add(new SQLiteParameter("Codi", tasca.Codi));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        /// <summary>
        /// Actualitza la priotitat d'una tasca
        /// </summary>
        /// <param name="tasca">Entitat usuari que es vol modificar</param>
        /// <returns>Files afectades</returns>
        public int UpdatePrioritat(Tasca tasca)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE Tasca SET Prioritat = @Prioritat WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Prioritat", tasca.Prioritat));
                    command.Parameters.Add(new SQLiteParameter("Codi", tasca.Codi));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        /// <summary>
        /// Actualitza l'estat d'una tasca
        /// </summary>
        /// <param name="tasca">Entitat usuari que es vol modificar</param>
        /// <returns>Files afectades</returns>
        public int UpdateEstat(Tasca tasca)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE Tasca SET Estat = @Estat WHERE Codi = @Codi";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("Estat", tasca.Estat));
                    command.Parameters.Add(new SQLiteParameter("Codi", tasca.Codi));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        /// <summary>
        /// Elimina una tasca
        /// </summary>
        /// <param name="Id">Codi d'usuari que es vol eliminar</param>
        /// <returns>Files afectades</returns>
        public int Delete(int Codi)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM Tasca WHERE Codi = @Codi";
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
