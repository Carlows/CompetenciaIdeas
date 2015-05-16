using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InscripcionCompetenciaFacebookLogin.Models
{
    public class RegistroViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Necesitas especificar un titulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Tu idea necesita contenido")]
        public string Contenido { get; set; }
        public int Votos { get; set; }
        public string ImgURL { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public HttpPostedFileBase Imagen { get; set; }

        public bool Winning { get; set; }
    }
}