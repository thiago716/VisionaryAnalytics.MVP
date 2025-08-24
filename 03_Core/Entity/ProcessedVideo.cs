using Core.Entity.Base;
using Core.Enums;

namespace Core.Entity;

public class ProcessedVideo : BaseEntity
{
     public required string FileName { get; set; }
     public required string OriginalName { get; set; }
     public required string FileExtension { get; set; }
     public long Size { get; set; }
     public DateTime? ProcessedOn { get; set; }
     public ProcessingStatus Status { get; set; }

     private readonly List<QrCodeDetection> _qrCodeDetections = new();
     public IReadOnlyCollection<QrCodeDetection> QrCodeDetections => _qrCodeDetections.AsReadOnly();

}
