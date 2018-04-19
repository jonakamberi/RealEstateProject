using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateProject.Data;
using RealEstateProject.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEstateProject.Controllers
{
    [Route("api/penthouses")]
    public class PenthousesAPIController : Controller
    {
        Penthouse[] penthouses = new Penthouse[]
        {
            new Penthouse { Id = 1, Name = "Miami Sky Penthouse #1", Price = 4309485, latitude = 25.822573, longitude= -80.195987 },
            new Penthouse { Id = 2, Name = "Miami Sky Penthouse #2", Price = 4109485, latitude = 27.822573, longitude= -80.195987 },
            new Penthouse { Id = 3, Name = "Miami Sky Penthouse #3", Price = 4309485, latitude = 21.822573, longitude= -80.195987 },
            new Penthouse { Id = 4, Name = "Miami Sky Penthouse #4", Price = 4309485, latitude = 22.822573, longitude= -80.195987 }
        };


        public IEnumerable<Penthouse> GetAllPenthouses() { return penthouses; }
    }
}
