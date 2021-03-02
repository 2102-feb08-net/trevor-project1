using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Repositories;

namespace StoreApp.WebUI.Controllers
{
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreRepository _storeRepository;

        public StoreController(StoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
    }
}
