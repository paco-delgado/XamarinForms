﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FluToDoApp.Models;

namespace FluToDoApp.Services
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetAllToDoItemsAsync();
    }
}