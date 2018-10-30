using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyTripTracker.BackService.Models;

namespace MyTripTracker.UI.Services
{
    public interface IApiClient
    {
        // These extension methods here are used
        // for encapsulating utility methods on top of http
        // client. We are creating specific extensions which
        // make it easier to do stuff. For example: go get
        // json and deserialize it as a trip. 

        Task<List<BackService.Models.Trip>> GetTripsAsync();
        Task<BackService.Models.Trip> GetTripAsync(int id);
        Task PutTripAsync(Trip tripToUpdate);
        Task AddTripAsync(Trip tripToAdd);
    }

    public class ApiClient : IApiClient

    {
        private readonly HttpClient _HttpClient;

        public ApiClient(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }


        public async Task<Trip> GetTripAsync(int id)
        {
            var response = await _HttpClient.GetAsync($"/api/Trips{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsJsonAsync<Trip>();
        }

        public async Task<List<Trip>> GetTripsAsync()
        {
            var response = await _HttpClient.GetAsync("/api/Trips");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsJsonAsync<List<Trip>>();
        }

        public async Task PutTripAsync(Trip tripToUpdate)
        {
            var response = await _HttpClient.PutJsonAsync($"api/Trips/{tripToUpdate.Id}", tripToUpdate);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddTripAsync(Trip tripToAdd)
        {
            var response = await _HttpClient.PostJsonAsync("/api/Trips", tripToAdd);
            response.EnsureSuccessStatusCode();
        }
    }


}
