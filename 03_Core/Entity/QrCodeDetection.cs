using System;

namespace Core.Entity;

public class QrCodeDetection
{
    public string Content { get; private set; }
    public double TimestampSeconds { get; private set; }

    public QrCodeDetection(string content, double timestampSeconds)
    {
        Content = content;
        TimestampSeconds = timestampSeconds;
    }
}
