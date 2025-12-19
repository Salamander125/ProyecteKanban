using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Configuration;
using KANBAN_INTERFICIE.Model;

namespace KANBAN_INTERFICIE
{
    public class API_REST
    {
        private readonly string _baseUri;
        private static API_REST _instance;

        // Propiedad para acceder a la instancia de forma segura
        public static API_REST Instance
        {
            get
            {
                if (_instance == null) _instance = new API_REST();
                return _instance;
            }
        }

        public API_REST()
        {
            // Leemos de AppSettings
            var uri = ConfigurationManager.AppSettings["BaseUri"];

            if (string.IsNullOrEmpty(uri))
            {
                // Esto te ayudará a debuguear si el config no se lee
                throw new Exception("No se ha encontrado la clave 'BaseUri' en el App.config");
            }

            _baseUri = uri;
        }

        // Método auxiliar para configurar el cliente y evitar repetición de código
        private HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public async Task<Responsable?> GetResponsableAsyncbyid(int Id)
        {
            using var client = GetClient();
            // GetFromJsonAsync reemplaza a GetAsync + ReadAsAsync
            return await client.GetFromJsonAsync<Responsable>($"responsable/{Id}");
        }

        public async Task<string?> GetPasswordAsyncbyusername(string usuari)
        {
            using var client = GetClient();
            return await client.GetFromJsonAsync<string>($"responsable/usuari/{usuari}");
        }

        public async Task<List<Responsable>> GetAllResponsableAsync()
        {
            using var client = GetClient();
            return await client.GetFromJsonAsync<List<Responsable>>("responsable") ?? new List<Responsable>();
        }

        public async Task AddResponsableAsync(Responsable responsable)
        {
            using var client = GetClient();
            var response = await client.PostAsJsonAsync("responsable", responsable);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateResponsableAsync(Responsable responsable)
        {
            using var client = GetClient();
            var response = await client.PutAsJsonAsync($"responsable/{responsable.Codi}", responsable);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteResponsableAsync(int Id)
        {
            using var client = GetClient();
            var response = await client.DeleteAsync($"responsable/{Id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Tasca?> GetTascaAsyncbyid(int Id)
        {
            using var client = GetClient();
            return await client.GetFromJsonAsync<Tasca>($"tasca/{Id}");
        }

        public async Task<List<Tasca>> GetAllTascaAsync()
        {
            using var client = GetClient();
            return await client.GetFromJsonAsync<List<Tasca>>("tasca") ?? new List<Tasca>();
        }

        public async Task AddTascaAsync(Tasca tasca)
        {
            using var client = GetClient();
            var response = await client.PostAsJsonAsync("tasca", tasca);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDecripcioTascaAsync(Tasca tasca)
        {
            using var client = GetClient();
            var response = await client.PutAsJsonAsync($"responsable/{tasca.codi}/descripcio", tasca);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEstatTascaAsync(Tasca tasca)
        {
            using var client = GetClient();
            var response = await client.PutAsJsonAsync($"responsable/{tasca.codi}/estat", tasca);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTascaAsync(long? Id)
        {
            using var client = GetClient();
            var response = await client.DeleteAsync($"tasca/{Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}