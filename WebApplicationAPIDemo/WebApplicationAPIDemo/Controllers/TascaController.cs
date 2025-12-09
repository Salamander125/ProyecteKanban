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
    [Route("api/Tasca")]
    [ApiController]
    public class TascaController : ControllerBase
    {
        // GET: users
        [HttpGet]
        public List<Tasca> Get()
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.GetAll();
        }

        // GET users/5
        [HttpGet("{Codi}")]
        public Tasca Get(int Codi)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.GetById(Codi);
        }

        // POST users
        [HttpPost]
        public Tasca Post([FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.Add(user);
        }

        // PUT users/5
        [HttpPut("{Codi}/Responsable")]
        public int PutResponsable(int Codi, [FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.UpdateResponsable(user);
        }

        // PUT users/5
        [HttpPut("{Codi}/Prioritat")]
        public int PutPrioritat(int Codi, [FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.UpdatePrioritat(user);
        }

        // PUT users/5
        [HttpPut("{Codi}/Estat")]
        public int PutEstat(int Codi, [FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.UpdateEstat(user);
        }

        // DELETE users/5
        [HttpDelete("{Codi}")]
        public void Delete(int Codi)
        {
            TascaService objTascaService = new TascaService();
            objTascaService.Delete(Codi);
        }
    }
}
