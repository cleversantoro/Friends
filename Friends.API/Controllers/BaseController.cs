using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.API.Controllers
{
    public class BaseController : ControllerBase
    {
        //public readonly IConfiguration _config;
        protected IConfiguration _config { get; private set; }


        public BaseController(IConfiguration config)
        {
            _config = config;
        }
    }
}
