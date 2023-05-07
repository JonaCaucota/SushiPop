﻿using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class Descuento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int Dia { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public double Porcentaje { get; set; }

        public double? DescuentoMax { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Activo { get; set; } = true;
        
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

    }
}
