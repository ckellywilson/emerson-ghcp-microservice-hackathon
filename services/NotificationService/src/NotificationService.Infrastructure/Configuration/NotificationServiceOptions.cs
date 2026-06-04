namespace NotificationService.Infrastructure.Configuration;

public sealed class NotificationServiceOptions
{
    public const string SectionName = "NotificationService";

    public int DefaultPageSize { get; set; } = 20;

    public int MaxPageSize { get; set; } = 100;
}