using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Parametro
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Impuestos { get; set; }

        [Required]
        public string Comisiones { get; set; }

    }
}
