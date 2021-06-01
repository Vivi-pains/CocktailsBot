using CocktailsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsBot.Clients
{
    class ApiClient
    {
        private HttpClient _client;
        private static string _adress;
        private static string _apikey;

        public ApiClient()
        {
            _adress = Constants.adress;
            _apikey = Constants.apikey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
        }

        public async Task<Cocktails> GetCocktailByName(string name)
        {
            var responce = await _client.GetAsync($"/Cocktails/search?name={name}");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Cocktails>(content);

            return result;
        }

        public async Task<FilterCocktails> GetCocktailsByIngridients(string noun)
        {
            var responce = await _client.GetAsync($"/Cocktails/filter?str={noun}");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<FilterCocktails>(content);

            return result;

        }

        public async Task<Cocktails> GetRandomCocktail()
        {
            var responce = await _client.GetAsync($"/Cocktails/random");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Cocktails>(content);

            return result;
        }

        public async Task AddToFavorite(Favorites favorites)
        {
            var stringJson = JsonConvert.SerializeObject(favorites);
            var stringContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
            await _client.PostAsync($"/Cocktails/addtofavorite", stringContent);
        }
        public async Task DeleteFromFavorite(Favorites favorites)
        {
            await _client.DeleteAsync($"/Cocktails/deletefromfavorites?UserId={favorites.UserId}&CocktailName={favorites.CocktailName}");
        }
        public async Task<IEnumerable<Favorites>> GetFavorites(int mes)
        {
            var responce = await _client.GetAsync($"/Cocktails/getfavorites?user={mes}");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<IEnumerable<Favorites>>(content);

            return result;
        }
    }
}
