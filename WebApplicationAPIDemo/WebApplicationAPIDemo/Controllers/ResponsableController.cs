using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPIDemo.DAL.Service;
using WebApplicationAPIDemo.Model;


namespace WebApplicationAPIDemo.Controllers
{
    [EnableCors]
    [Route("api/Responsable")]
    [ApiController]
    public class ResponsableController : ControllerBase
    {
        // GET: users
        [HttpGet]
        public List<Responsable> Get()
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.GetAll();
        }

        // GET users/5
        [HttpGet("{Codi}")]
        public Responsable Get(int Codi)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.GetById(Codi);
        }

        // POST users
        [HttpPost]
        public Responsable Post([FromBody] Responsable user)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.Add(user);
        }

        // PUT users/5
        [HttpPut("{Codi}")]
        public int PutContrasenya(int Codi, [FromBody] Responsable user)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.UpdateContrasenya(user);
        }

        // DELETE users/5
        [HttpDelete("{Codi}")]
        public void Delete(int Codi)
        {
            ResponsableService objResponsableService = new ResponsableService();
            objResponsableService.Delete(Codi);
        }
    }
}

