using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAppAPI.Data;
//The ApiUser class is used to add fields to the users table 
public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07BB4FAD9B");
            entity.Property(e => e.Bio).HasColumnName("BIO");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07DB0EED1A");

            entity.HasIndex(e => e.Isbn, "UQ__tmp_ms_x__447D36EA39298648").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });


        //Seeded the database
        //Added these two Roles to the database
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
               Name="User",
               NormalizedName="USER",
               Id= "cee0008b - bcf2 - 4b71 - 8fd4 - f90e0e1548c1"
            },
            new IdentityRole
            {
                Name="Administrator",
                NormalizedName="ADMINISTARTOR",
                Id= "00177bda-de97-4a85-9058-675612e8e1a6"
            }
            );

        var hasher = new PasswordHasher<ApiUser>();//for hiding password
        //Added these two users for testing purposes
        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser
            {   
              
                Id = "2515af1f-70d1-4b2d-a624-844a885fa35f",
                Email= "skhomo@beier.co.za",
                NormalizedEmail="SKHOMO@BEIER.CO.ZA",
                UserName= "skhomo@beier.co.za",
                NormalizedUserName= "SKHOMO@BEIER.CO.ZA",
                FirstName="Eric",
                LastName="Khomo",
                PasswordHash=hasher.HashPassword(null,"Password")
            },
            new ApiUser
            {
                Id = "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8",
                Email = "smboweni@beier.co.za",
                NormalizedEmail = "SMBOWENI@BEIER.CO.ZA",
                UserName = "skhomo@beier.co.za",
                NormalizedUserName = "SMBOWENI@BEIER.CO.ZA",
                FirstName = "Zanele",
                LastName = "Mboweni",
                PasswordHash = hasher.HashPassword(null, "Password")
            }
            );

        //Adding Roles to the user, Many to Many
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            { 
                RoleId= "cee0008b - bcf2 - 4b71 - 8fd4 - f90e0e1548c1",
                UserId= "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8"
            },
            new IdentityUserRole<string>
            {
                RoleId = "00177bda-de97-4a85-9058-675612e8e1a6",
                UserId = "2515af1f-70d1-4b2d-a624-844a885fa35f"
            }
            );
           OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
