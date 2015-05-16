using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InscripcionCompetenciaFacebookLogin.Models
{
    public class Registro
    {
        public Registro()
        {
            Votos = new List<Voto>();
        }

        public int Id { get; set; }

        [Required]
        public virtual User Usuario { get; set; }

        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string ImgUrl { get; set; }
        
        public virtual List<Voto> Votos { get; set; }
    }

    public class Voto
    {
        public int Id { get; set; }

        [Required]
        public User Usuario { get; set; }
        [Required]
        public Registro Registro { get; set; }
    }
}