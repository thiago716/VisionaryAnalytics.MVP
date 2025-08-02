using System;

namespace Shared.DTOs;

public class AtualizarVideoMessage
{
     public int Id { get; set; } 
     public string Nome { get; set; } = string.Empty;
     public string Status { get; set; } = string.Empty;
     public DateTime CriadoEm { get; set; }
     public List<QrCodeDetectionDto> QrCodes { get; set; } = new();
}

/// <summary>
/// Representa uma detecção de QR Code com o conteúdo e o tempo (em segundos) no vídeo.
/// </summary>
public class QrCodeDetectionDto
{
    public string Content { get; set; } = string.Empty;
    public double TimestampSeconds { get; set; }
}