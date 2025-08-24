using System;
using Core.Entity.Base;

namespace Core.Entity;

public class QrCodeDetection : BaseEntity
{
    public TimeSpan TimeSpan { get; set; }
    public required string DataContent { get; set; }
    public Guid VideoProcessId { get; set; }

    public virtual ProcessedVideo? VideoProcess { get; set; }
}
