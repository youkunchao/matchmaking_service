using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Data;

public class MatchmakingDbContext : DbContext
{
    public MatchmakingDbContext(DbContextOptions<MatchmakingDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Hongniang> Hongniang { get; set; }
    public DbSet<UserPhoto> UserPhotos { get; set; }
    public DbSet<UserVerification> UserVerifications { get; set; }
    public DbSet<MatchRecord> MatchRecords { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<MeetRequest> MeetRequests { get; set; }
    public DbSet<MemberPackage> MemberPackages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityParticipant> ActivityParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Photos)
            .WithOne()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Verifications)
            .WithOne()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.MatchRecords)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ChatMessages)
            .WithOne(c => c.FromUser)
            .HasForeignKey(c => c.FromUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.MeetRequests)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ActivityParticipants)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hongniang>()
            .HasMany(h => h.Users)
            .WithOne(u => u.Hongniang)
            .HasForeignKey(u => u.HongniangId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Activity>()
            .HasMany(a => a.Participants)
            .WithOne(p => p.Activity)
            .HasForeignKey(p => p.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Package)
            .WithMany()
            .HasForeignKey(o => o.PackageId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Activity)
            .WithMany()
            .HasForeignKey(o => o.ActivityId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<MatchRecord>()
            .HasOne(m => m.Hongniang)
            .WithMany()
            .HasForeignKey(m => m.HongniangId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<MeetRequest>()
            .HasOne(m => m.Hongniang)
            .WithMany()
            .HasForeignKey(m => m.HongniangId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<User>()
            .Property(u => u.Status)
            .HasDefaultValue(1);

        modelBuilder.Entity<User>()
            .Property(u => u.MemberLevel)
            .HasDefaultValue("普通会员");

        modelBuilder.Entity<Hongniang>()
            .Property(h => h.Status)
            .HasDefaultValue(1);

        modelBuilder.Entity<Hongniang>()
            .Property(h => h.SuccessCount)
            .HasDefaultValue(0);

        modelBuilder.Entity<Activity>()
            .Property(a => a.Status)
            .HasDefaultValue(0);

        modelBuilder.Entity<Activity>()
            .Property(a => a.Fee)
            .HasDefaultValue(0);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasDefaultValue("待支付");

        modelBuilder.Entity<Order>()
            .Property(o => o.Amount)
            .HasDefaultValue(0);
    }
}