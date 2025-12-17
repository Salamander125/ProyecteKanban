using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfAppTestAPIClient.Model;
using System.Configuration;

namespace WpfAppTestAPIClient.APIClient
{
    public class UsersApiClient
    {
        string BaseUri;
        
        
        public UsersApiClient()
        {
            BaseUri = ConfigurationManager.AppSettings["BaseUri"];
        }
        
        /// <summary>
        /// Obté un Responsable a partir del id
        /// </summary>        
        public async Task<Responsable> GetResponsableAsyncbyid(int Id)
        {
            Responsable responsable = new Responsable();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició GET al endpoint /users/{Id}
                HttpResponseMessage response = await client.GetAsync($"responsable/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    //Reposta 204 quan no ha trobat dades
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        responsable = null;
                    }
                    else
                    {
                        //Obtenim el resultat i el carreguem al Objecte User
                        responsable = await response.Content.ReadAsAsync<Responsable>();
                        response.Dispose();
                    }
                }
                else
                {
                    //TODO: que fer si ha anat malament? retornar null? 
                }
            }
            return responsable;
        }

        /// <summary>
        /// Obté una contrasenya a partir d'un usuari
        /// </summary>
        public async Task<string> GetPasswordAsyncbyusername(string usuari)
        {
            string contrasenya;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició GET al endpoint /users/{Id}
                HttpResponseMessage response = await client.GetAsync($"responsable/usuari/{usuari}");
                if (response.IsSuccessStatusCode)
                {
                    //Reposta 204 quan no ha trobat dades
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        contrasenya = null;
                    }
                    else
                    {
                        //Obtenim el resultat i el carreguem al Objecte User
                        contrasenya = await response.Content.ReadAsAsync<string>();
                        response.Dispose();
                    }
                }
                else
                {
                    contrasenya = null;
                    //TODO: que fer si ha anat malament? retornar null? 
                }
            }
            return contrasenya;
        }

        /// <summary>
        /// Obté una llista de tots els Responsables de la base de dades
        /// </summary>
        public async Task<List<Responsable>> GetAllResponsableAsync()
        {
            List<Responsable> users = new List<Responsable>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició GET al endpoint /users}
                HttpResponseMessage response = await client.GetAsync("responsable");
                if (response.IsSuccessStatusCode)
                {
                    //Obtenim el resultat i el carreguem al objecte llista d'usuaris
                    users = await response.Content.ReadAsAsync<List<Responsable>>();
                    response.Dispose();
                }
                else
                {
                    //TODO: que fer si ha anat malament? retornar null? missatge?
                }
            }
            return users;
        }

        /// <summary>
        /// Afegeix un nou Responsable
        /// </summary>
        /// <param name="responsable">Usuari que volem afegir</param>
        /// <returns></returns>
        public async Task AddResponsableAsync(Responsable responsable)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició POST al endpoint /users}
                HttpResponseMessage response = await client.PostAsJsonAsync("responsable", responsable);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Modificar un usuari
        /// </summary>
        public async Task UpdateResponsableAsync(Responsable responsable)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició PUT al endpoint /users/Id
                HttpResponseMessage response = await client.PutAsJsonAsync($"responsable/{responsable.Codi}", responsable);
                response.EnsureSuccessStatusCode();
            }
        }


        /// <summary>
        /// Modificar un usuari
        /// </summary>
        public async Task DeleteResponsableAsync(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició DELETE al endpoint /responsable/Id
                HttpResponseMessage response = await client.DeleteAsync($"responsable/{Id}");
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<Tasca> GetTascaAsyncbyid(int Id)
        {
            Tasca tasca = new Tasca();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició GET al endpoint /users/{Id}
                HttpResponseMessage response = await client.GetAsync($"tasca/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    //Reposta 204 quan no ha trobat dades
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        tasca = null;
                    }
                    else
                    {
                        //Obtenim el resultat i el carreguem al Objecte tasca
                        tasca = await response.Content.ReadAsAsync<Tasca>();
                        response.Dispose();
                    }
                }
                else
                {
                    //TODO: que fer si ha anat malament? retornar null? 
                }
            }
            return tasca;
        }
        
        /// <summary>
        /// Obté una llista de tots els Responsables de la base de dades
        /// </summary>
        public async Task<List<Tasca>> GetAllTascaAsync()
        {
            List<Tasca> tasca = new List<Tasca>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició GET al endpoint /tasca}
                HttpResponseMessage response = await client.GetAsync("tasca");
                if (response.IsSuccessStatusCode)
                {
                    //Obtenim el resultat i el carreguem al objecte llista de tasca
                    tasca = await response.Content.ReadAsAsync<List<Tasca>>();
                    response.Dispose();
                }
                else
                {
                    //TODO: que fer si ha anat malament? retornar null? missatge?
                }
            }
            return tasca;
        }

        /// <summary>
        /// Afegeix un nova tasca
        /// </summary>

        public async Task AddTascaAsync(Tasca tasca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició POST al endpoint /tasca}
                HttpResponseMessage response = await client.PostAsJsonAsync("tasca", tasca);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Modificar una descripcio d'una tasca
        /// </summary>
        public async Task UpdateDecripcioTascaAsync(Tasca tasca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició PUT al endpoint /users/Id
                HttpResponseMessage response = await client.PutAsJsonAsync($"responsable/{tasca.Codi}/descripcio", tasca);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Modificar un responsable d'una tasca
        /// </summary>
        public async Task UpdateResponsableTascaAsync(Tasca tasca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició PUT al endpoint /users/Id
                HttpResponseMessage response = await client.PutAsJsonAsync($"responsable/{tasca.Codi}/responsable", tasca);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Modificar la prioritat 
        /// </summary>
        public async Task UpdatePrioridadTascaAsync(Tasca tasca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició PUT al endpoint /users/Id
                HttpResponseMessage response = await client.PutAsJsonAsync($"responsable/{tasca.Codi}/prioritat", tasca);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Modificar l'estat d'una tasca
        /// </summary>
        public async Task UpdateEstatTascaAsync(Tasca tasca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició PUT al endpoint /tasca/Id/estat
                HttpResponseMessage response = await client.PutAsJsonAsync($"responsable/{tasca.Codi}/estat", tasca);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Eliminar Tasca
        /// </summary>
        public async Task DeleteTascaAsync(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició DELETE al endpoint /tasca/Id
                HttpResponseMessage response = await client.DeleteAsync($"tasca/{Id}");
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
