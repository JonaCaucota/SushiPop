﻿using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiPopG5.Models
{
    public class Reserva
    { 
        [Key]
        public int Id { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public string Local { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public DateTime FechaYHora { get; set; }
        
        public bool? Confirmada { get; set; }

    }
}
