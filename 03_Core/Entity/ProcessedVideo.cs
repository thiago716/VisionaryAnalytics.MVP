using Core.Entity.Base;
using Core.Enums;

namespace Core.Entity;

public class ProcessedVideo : BaseEntity
{
     public string FileName { get; set; }
     public ProcessingStatus Status { get; set; }

     private readonly List<QrCodeDetection> _qrCodeDetections = new();
     public IReadOnlyCollection<QrCodeDetection> QrCodeDetections => _qrCodeDetections.AsReadOnly();

     public ProcessedVideo(string fileName)
     {
          FileName = fileName;
          Status = ProcessingStatus.Queued;
     }

     public void UpdateStatus(ProcessingStatus newStatus)
     {
          Status = newStatus;
     }

     public void AddDetection(string content, double timestampSeconds)
     {
          var detection = new QrCodeDetection(content, timestampSeconds);
          _qrCodeDetections.Add(detection);
     }

}
