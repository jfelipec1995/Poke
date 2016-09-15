using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class Entrenador
    {
        [Key]
        public int Id { get; set; }
        public int Id_Pokemon { get; set; }
        public int Id_Rival { get; set; }
        [ForeignKey("Id_Pokemon")]
        public virtual Poke pokemon { get; set; }

        [ForeignKey("Id_Rival")]
        public virtual Poke rival { get; set; }

    }
}