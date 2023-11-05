using System;
using System.Collections.Generic;

namespace Web_24BM.Models;

public partial class Contacto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? Email { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    public string? Objetivo { get; set; }

    public string? Foto { get; set; }
}
