using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPIDemo.Model
{
    public class Responsable
    {
        public long Codi { get; set; }
        public string Nom { get; set; }
        public string Cognom { get; set; }
        public string Contrasenya { get; set; }
        public bool Admin {  get; set; }

    }
}