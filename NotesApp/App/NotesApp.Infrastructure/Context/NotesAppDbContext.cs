using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Models;

namespace NotesApp.Infrastructure.Context
{
    public partial class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext()
        {
        }

        public NotesAppDbContext(DbContextOptions<NotesAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<NoteDetail> NoteDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Note__UserId__3B75D760");
            });

            modelBuilder.Entity<NoteDetail>(entity =>
            {
                entity.ToTable("NoteDetail");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.LastEdited).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.NoteDetails)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("FK__NoteDetai__NoteI__3E52440B");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).HasMaxLength(1024);

                entity.Property(e => e.PasswordSalt).HasMaxLength(1024);

                entity.Property(e => e.RefreshToken).IsUnicode(false);

                entity.Property(e => e.TokenCreated).HasColumnType("datetime");

                entity.Property(e => e.TokenExpires).HasColumnType("datetime");

                entity.Property(e => e.UserCreated).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
