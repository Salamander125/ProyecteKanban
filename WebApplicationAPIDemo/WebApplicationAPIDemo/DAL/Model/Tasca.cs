using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPIDemo.Model
{
    public class Tasca
    {
        public long Codi { get; set; }
        public string Titol { get; set; }
        public string Descripcio { get; set; }
        public DateTime Data_creacio { get; set; }
        public DateTime Data_finalitzacio { get; set; }
        public int Prioritat {  get; set; }
        public int Estat {  get; set; }
        public int Codi_responsable { get; set; }

    }
}
