using api_notification.Models;
using Microsoft.EntityFrameworkCore;

namespace api_notification.Contexts;

public class MainDatabaseContext : DbContext
{
    public MainDatabaseContext(DbContextOptions<MainDatabaseContext> options) : base(options) { }

    public virtual DbSet<NotificationModel> Notifications { get; set; }
}
