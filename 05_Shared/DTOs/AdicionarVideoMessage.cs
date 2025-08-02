using System;

namespace Shared.DTOs;

public class AdicionarVideoMessage
{
     public int Id { get; set; }
     public string Nome { get; set; } = string.Empty;
     public string Status { get; set; } = string.Empty;
     public DateTime CriadoEm { get; set; }
}
