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
        // GET: Tasca
        [HttpGet]
        public List<Tasca> Get()
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.GetAll();
        }

        // GET Tasca/5
        [HttpGet("{Codi}")]
        public Tasca Get(int Codi)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.GetById(Codi);
        }

        // POST Tasca
        [HttpPost]
        public Tasca Post([FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.Add(user);
        }

        // PUT Tasca/5
        [HttpPut("{Codi}/Responsable")]
        public int PutResponsable(int Codi, [FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.UpdateResponsable(user);
        }

        // PUT Tasca/1
        [HttpPut("{Codi}/Prioritat")]
        public int PutPrioritat(int Codi, [FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.UpdatePrioritat(user);
        }

        // PUT Tasca/2
        [HttpPut("{Codi}/Estat")]
        public int PutEstat(int Codi, [FromBody] Tasca user)
        {
            TascaService objTascaService = new TascaService();
            return objTascaService.UpdateEstat(user);
        }

        // DELETE Tasca/5
        [HttpDelete("{Codi}")]
        public void Delete(int Codi)
        {
            TascaService objTascaService = new TascaService();
            objTascaService.Delete(Codi);
        }
    }
}
