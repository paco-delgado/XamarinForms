using FluToDoApp.Config;
using FluToDoApp.Data.Helper;
using FluToDoApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluToDoApp.Data
{
    public class ToDoApiServiceAgent : IToDoApiServiceAgent
    {
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
    }
}
