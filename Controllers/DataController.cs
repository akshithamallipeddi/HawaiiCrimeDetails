using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HawaiiCrimeDetails.DataAccess;
using HawaiiCrimeDetails.Models;

namespace HawaiiCrimeDetails.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class DataController : Controller
    {
        //public ApplicationDbContext dbContext;
        //private readonly IMapper _mapper;

        //public DataController(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}

        //public DataController(ApplicationDbContext context)
        //{
        //    dbContext = context;
        //}

        //[HttpGet()]
        //public IActionResult GetData()
        //{
        //    List<RootData> list = dbContext.data.ToList();

        //    var listToReturn = _mapper.Map<List<RootData>>(list);
        //    return Ok(listToReturn);
        //}
    }
}
