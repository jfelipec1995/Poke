using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class Poke
    {
        [Key]
        public int Id_poke { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public int Vida { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }

        public static implicit operator int(Poke v)
        {
            throw new NotImplementedException();
        }
    }


}