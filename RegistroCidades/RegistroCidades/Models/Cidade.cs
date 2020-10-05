using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCidades.Models
{
    public class Cidade
    {
        public int CodIBGE { get; set; }
        public string Nome { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Regiao Regiao { get; set; } = new Regiao();
        public UF UF { get; set; } = new UF();
        
        //public int Regiao { get; set; }        
        //public int UF { get; set; } 
    }

    
}
