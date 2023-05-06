﻿using SushiPopG5.Utils;
using System.ComponentModel.DataAnnotations;

namespace SushiPopG5.Models
{
    public class Usuario
    {
        [Key]
        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido) ]
        [MaxLength(30, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(2, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(30, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(2, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [MaxLength(10, ErrorMessage = ErrorMsg.ErrorMaxCaracteres)]
        [MinLength(10, ErrorMessage = ErrorMsg.ErrorMinCaracteres)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        [DataType(DataType.Date)]
        public DateTime FechaAlta = DateTime.Now;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public bool Activo { get; set; } = true;

        [Required(ErrorMessage = ErrorMsg.ErrorCampoRequerido)]
        public string Email { get; set; }
    }
}