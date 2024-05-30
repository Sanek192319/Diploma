using AltairCA.EntityFrameworkCore.PostgreSQL.ColumnEncryption.EfExtension;
using AltairCA.EntityFrameworkCore.PostgreSQL.ColumnEncryption.Utils;
using EasyStudy.Shared.Entities.Domain;
using EasyStudy.Shared.Entities.Settings;
using Microsoft.EntityFrameworkCore;

namespace EasyStudy.DB;

public class EasyStudyDbContext : DbContext
{
    private readonly SecuritySettings _settings;
    
    public DbSet<Teacher> Teachers { get; set; }
    
    public DbSet<Subject> Subjects { get; set; }
    
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Group> Groups { get; set; }
    
    public DbSet<Class> Classes { get; set; }
    
    public EasyStudyDbContext(DbContextOptions<EasyStudyDbContext> options, SecuritySettings settings)
    {
        _settings = settings;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseEncryptionFunctions(_settings.EncryptionKey, EncKeyLength.L128);
    }
}