using System.ComponentModel.DataAnnotations.Schema;

namespace api_notification.Models;

[Table("NOTIFICATIONS_SETTINGS")]
public partial class NotificationModel
{
    [Column("ID")]
    public int Id { get; set; }
    [Column("RECIPIENT")]
    public string Recipient { get; set; }
    [Column("SUBJECT")]
    public string Subject { get; set; }
    [Column("BODY")]
    public string Body { get; set; }
    [Column("DUE_DATE")]
    public DateTime? DueDate { get; set; }
}
