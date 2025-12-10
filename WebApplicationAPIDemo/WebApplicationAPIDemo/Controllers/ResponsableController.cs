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
        // GET: Responsable
        [HttpGet]
        public List<Responsable> Get()
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.GetAll();
        }

        // GET Responsable/5
        [HttpGet("{Codi}")]
        public Responsable Get(int Codi)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.GetById(Codi);
        }

        // GET Responsable/Proyecto
        [HttpGet("Usuari/{Usuari}")]
        public string Get(string Usuari)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.GetPassword(Usuari);
        }

        // POST Responsable
        [HttpPost]
        public Responsable Post([FromBody] Responsable user)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.Add(user);
        }

        // PUT Responsable/paloma
        [HttpPut("{Codi}/Contrasenya")]
        public int PutContrasenya(int Codi, [FromBody] Responsable user)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.UpdateContrasenya(user);
        }

        // DELETE Responsable/5
        [HttpDelete("{Codi}")]
        public void Delete(int Codi)
        {
            ResponsableService objResponsableService = new ResponsableService();
            objResponsableService.Delete(Codi);
        }
    }
}

