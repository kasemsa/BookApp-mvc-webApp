using Newtonsoft.Json;
using NuGet.Protocol;
using System;
using System.Net;

namespace BookApp.Services
{
    public class StackOverflowService
    {
        private readonly HttpClient _httpClient;

        public StackOverflowService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            var response = await _httpClient.GetAsync("https://api.stackexchange.com/2.3/questions?order=desc&sort=creation&site=stackoverflow");
           
            response.EnsureSuccessStatusCode();

            var json =   response.Content.ReadAsStringAsync().Result;

            List<Question> result = JsonConvert.DeserializeObject<List<Question>>(json);

            return result;
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.stackexchange.com/2.3/questions?order=desc&sort=creation&site=stackoverflow/{id}?site=stackoverflow");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Question>(json);

            return result;
        }
    }



}
