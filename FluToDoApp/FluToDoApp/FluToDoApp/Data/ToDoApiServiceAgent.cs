using FluToDoApp.Config;
using FluToDoApp.Data.Helper;
using FluToDoApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluToDoApp.Data
{
    public class ToDoApiServiceAgent : IToDoApiServiceAgent
    {
        private const string JsonMediaType = "application/json";
        private readonly IHttpClientWrapper _client;

        public ToDoApiServiceAgent(IHttpClientWrapper client)
        {
            _client = client;
        }

        public async Task<List<ToDoItem>> GetAllToDoItemsAsync()
        {
            var toDoItemsList = new List<ToDoItem>();
            try
            {
                var response = await _client.GetAsync(string.Format(Constants.ToDoApiUri, string.Empty));
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toDoItemsList = JsonConvert.DeserializeObject<List<ToDoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred calling ToDoApi: {ex.Message}");
            }
            return toDoItemsList;
        }

        public async Task SaveToDoItemAsync(ToDoItem item, bool isNewItem = false)
        {
            try
            {
                string jsonItem = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(jsonItem, Encoding.UTF8, JsonMediaType);
                string requestUri = string.Format(Constants.ToDoApiUri, string.Empty);
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(requestUri, content);
                }
                else
                {
                    response = await _client.PutAsync(requestUri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred calling ToDoApi: {ex.Message}");
            }
        }

        public async Task DeleteToDoItemAsync(string key)
        {
            try
            {
                var response = await _client.DeleteAsync(string.Format(Constants.ToDoApiUri, key));
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("TodoItem successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred calling ToDoApi: {ex.Message}");
            }
        }
    }
}
