using System;
using System.Collections.Generic;
using System.Linq;
using AssignKudosContext.Entities;
using AssignKudosContext.Repositories.Parameters;
using AssignKudosContext.UseCases;
using EkudosAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EkudosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [AllowAnonymous]

    public class EkudosController : ControllerBase
    {
        IManageKudosService _manageKudosService;
        IReaderKudosService _readerKudosService;

        public EkudosController(
            IManageKudosService manageKudosService,
            IReaderKudosService readerKudosService)
        {
            _manageKudosService = manageKudosService;
            _readerKudosService = readerKudosService;
        }

        // GET api/ekudos/1/20/When/Asc?whom=bartosz.zaremba@rockwool.com,hubert.szczepanski@rockwool.com&donator=bnbd
        [HttpGet]
        [HttpGet("{howMany}")]
        [HttpGet("{fromIndex}/{howMany}")]
        [HttpGet("{fromIndex}/{howMany}/{sort}")]
        [HttpGet("{fromIndex}/{howMany}/{sort}/{direct}")]
        //public ActionResult<IEnumerable<EkudosSimple>> Get([FromBody] ViewModelFiltr filtr)
        public ActionResult<IEnumerable<EkudosSimple>> Get(
            int howMany = 0, 
            int fromIndex = 0, 
            string sort = "When",
            Order direct = Order.Desc,
            string whoms = "",
            string donators = "")
        {
            var filtr = new ViewModelFilter()
            {
                HowMany = howMany,
                From = fromIndex,
                Sort  = sort,
                Order = direct,
                FilterList = new List<(string, string)>()
                {
                    ("whoms", whoms),
                    ("donators", donators)
                }
            };

            return _readerKudosService
                .GetKudosSimpleList(filtr)
                .Select(kudos => new EkudosSimple
                {
                    Id = kudos.Id,
                    Who = kudos.Whom,
                    WhoFrom = kudos.WhoFrom,
                    Title = kudos.ForWhat,
                    When = kudos.When,
                    Avatar = "https://cdn.vuetifyjs.com/images/lists/1.jpg",
                    Giphy = kudos.Giphy
                    
                })
                .ToList();
        }

        // POST api/ekudos
        [HttpPost]
        public void Post([FromBody] Ekudos assignedEkudos)
        {
            _manageKudosService.AddKudos(
                new Kudos() { ForWhat = assignedEkudos.Description, Giphy= assignedEkudos.Giphy, When = DateTime.Now },
                new Donator() { Name = assignedEkudos.WhoFrom },
                new Recipient() { Name = assignedEkudos.Whom }
             );
        }


    }
}
