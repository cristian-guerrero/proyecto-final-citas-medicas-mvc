﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CitasMedicasMVC
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;

  public  class LoginModel
  {


    //[ScaffoldColumn (false)]
    [Required(ErrorMessage = "Nombre de usuario requerido") ]
    [DisplayName("Nombre de Usuario.")]
    public string usuario { get; set; }

    [DisplayName("Contraseña")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "La contraseña es requerida")]
    public string password { get; set; }
  }
}