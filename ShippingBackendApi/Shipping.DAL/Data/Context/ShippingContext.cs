using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shipping.DAL.Data.Models;
using System.Security.Claims;

namespace Shipping.DAL.Data
{
    public class ShippingContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Governorate> Governorates => Set<Governorate>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<ShippingType> ShippingTypes => Set<ShippingType>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<SpecialPrice> SpecialPrices => Set<SpecialPrice>();
        public DbSet<GroupPermission> GroupPermissions => Set<GroupPermission>();
        public DbSet<DeliverToVillage> DeliverToVillages => Set<DeliverToVillage>();
        public DbSet<Weight> Weights => Set<Weight>();
        public DbSet<RepresentativeGovernate> RepresentativeGovernates => Set<RepresentativeGovernate>();
        public DbSet<ReasonsRefusalType> ReasonsRefusalTypes => Set<ReasonsRefusalType>();
        public ShippingContext(DbContextOptions options) : base(options)
        {


        }

        List<Permission> permissions = new List<Permission>()
        {
            new Permission()
            {
                Id = 1,
                Name="Branch"

            },
            new Permission()
            {
                Id = 2,
                Name = "City"

            },
             new Permission()
            {
                Id =3 ,
                Name = "Governorate"

            }
            , new Permission()
            {
                Id = 4,
                Name = "Employee"

            }, new Permission()
            {
                Id = 5,
                Name = "Representative"

            }, new Permission()
            {
                Id = 6,
                Name = "Merchant"

            },
             new Permission()
            {
                Id = 7,
                Name = "Order"

            },
              new Permission()
            {
                Id = 8,
                Name = "OrderReports"

            },
              new Permission()
            {
                Id = 9,
                Name = "Group"
            },
            new Permission()
            {
                Id = 10,
                Name = "ReasonsRefusalType"

            },
           new Permission()
            {
                Id = 11,
                Name = "ShippingType"
            },
           new Permission()
            {
                Id = 12,
                Name = "DeliverToVillage"
            },
            new Permission()
            {
                Id = 13,
                Name = "Weight"
            },



        };

        List<GroupPermission> groupPermissions = new List<GroupPermission>()

        {
            new GroupPermission { id =1, GroupId =1, PermissionId=1,Action="Add"},
            new GroupPermission {id=2, GroupId =1, PermissionId=1,Action="Edit"},
            new GroupPermission {id =3, GroupId = 1, PermissionId = 1, Action = "Delete" },
            new GroupPermission {id =4, GroupId = 1, PermissionId = 1, Action = "Show" },

            new GroupPermission {id =5, GroupId = 1, PermissionId = 2, Action = "Add" },
            new GroupPermission {id =6, GroupId = 1, PermissionId = 2, Action = "Edit" },
            new GroupPermission {id =7, GroupId = 1, PermissionId = 2, Action = "Delete" },
            new GroupPermission {id =8, GroupId = 1, PermissionId = 2, Action = "Show" },

            new GroupPermission {id =9, GroupId = 1, PermissionId = 3, Action = "Add" },
            new GroupPermission {id =10, GroupId = 1, PermissionId = 3, Action = "Edit" },
            new GroupPermission {id =11, GroupId = 1, PermissionId = 3, Action = "Delete" },
            new GroupPermission {id =12, GroupId = 1, PermissionId = 3, Action = "Show" },

            new GroupPermission {id=13 , GroupId = 1, PermissionId = 4, Action = "Add" },
            new GroupPermission {id=14 , GroupId = 1, PermissionId = 4, Action = "Edit" },
            new GroupPermission {id=15 , GroupId = 1, PermissionId = 4, Action = "Delete" },
            new GroupPermission {id=16 , GroupId = 1, PermissionId = 4, Action = "Show" },

            new GroupPermission {id=17 , GroupId = 1, PermissionId = 5, Action = "Add" },
            new GroupPermission {id=18 , GroupId = 1, PermissionId = 5, Action = "Edit" },
            new GroupPermission {id=19 , GroupId = 1, PermissionId = 5, Action = "Delete" },
            new GroupPermission {id=20 , GroupId = 1, PermissionId = 5, Action = "Show" },

            new GroupPermission {id=21, GroupId = 1, PermissionId = 6, Action = "Add" },
            new GroupPermission {id=22, GroupId = 1, PermissionId = 6, Action = "Edit" },
            new GroupPermission {id=23, GroupId = 1, PermissionId = 6, Action = "Delete" },
            new GroupPermission {id=24, GroupId = 1, PermissionId = 6, Action = "Show" },

            new GroupPermission {id=25, GroupId = 1, PermissionId = 7, Action = "Add" },
            new GroupPermission {id=26, GroupId = 1, PermissionId = 7, Action = "Edit" },
            new GroupPermission {id=27, GroupId = 1, PermissionId = 7, Action = "Delete" },
            new GroupPermission {id=28, GroupId = 1, PermissionId = 7, Action = "Show" },

            new GroupPermission {id=29, GroupId = 1, PermissionId = 8, Action = "Add" },
            new GroupPermission {id=30, GroupId = 1, PermissionId = 8, Action = "Edit" },
            new GroupPermission {id=31, GroupId = 1, PermissionId = 8, Action = "Delete" },
            new GroupPermission {id=32, GroupId = 1, PermissionId = 8, Action = "Show" },

            new GroupPermission {id=33, GroupId = 1, PermissionId = 9, Action = "Add" },
            new GroupPermission {id=34, GroupId = 1, PermissionId = 9, Action = "Edit" },
            new GroupPermission {id=35, GroupId = 1, PermissionId = 9, Action = "Delete" },
            new GroupPermission {id=36, GroupId = 1, PermissionId = 9, Action = "Show" },


            new GroupPermission {id=37, GroupId = 1, PermissionId = 10, Action = "Add" },
            new GroupPermission {id=38, GroupId = 1, PermissionId = 10, Action = "Edit" },
            new GroupPermission {id=39, GroupId = 1, PermissionId = 10, Action = "Delete" },
            new GroupPermission {id=40, GroupId = 1, PermissionId = 10, Action = "Show" },

            new GroupPermission {id=41, GroupId = 1, PermissionId = 11, Action = "Add" },
            new GroupPermission {id=42, GroupId = 1, PermissionId = 11, Action = "Edit" },
            new GroupPermission {id=43, GroupId = 1, PermissionId = 11, Action = "Delete" },
            new GroupPermission {id=44, GroupId = 1, PermissionId = 11, Action = "Show" },

            new GroupPermission {id=45, GroupId = 1, PermissionId = 12, Action = "Add" },
            new GroupPermission {id=46, GroupId = 1, PermissionId = 12, Action = "Edit" },
            new GroupPermission {id=47, GroupId = 1, PermissionId = 12, Action = "Delete" },
            new GroupPermission {id=48, GroupId = 1, PermissionId = 12, Action = "Show" },

            new GroupPermission {id=49, GroupId = 1, PermissionId = 13, Action = "Add" },
            new GroupPermission {id=50, GroupId = 1, PermissionId = 13, Action = "Edit" },
            new GroupPermission {id=51, GroupId = 1, PermissionId = 13, Action = "Delete" },
            new GroupPermission {id=52, GroupId = 1, PermissionId = 13, Action = "Show" },

        };



        DeliverToVillage deliverToVillage = new DeliverToVillage()
        {
            Id = 1,
            AdditionalCost = 10
        };
        Weight weight = new Weight()
        {
            Id = 1,
            DefaultWeight = 10,
            AdditionalPrice = 5
        };
        Branch branch = new Branch()
        {
            Id = 1,
            Name = "الفرع الرئيسي",

        };
        Group group = new Group()
        {
            Id = 1,
            Name = "ادمن"
        };

      



    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Permission>().HasData(permissions);
            modelBuilder.Entity<Branch>().HasData(branch);
            modelBuilder.Entity<Group>().HasData(group);
            modelBuilder.Entity<GroupPermission>().HasData(groupPermissions.ToArray());
            modelBuilder.Entity<Weight>().HasData(weight);
            modelBuilder.Entity<DeliverToVillage>().HasData(deliverToVillage);
           // modelBuilder.Entity<Employee>().HasData(Admin);
            
            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Merchant>("Merchant")
                .HasValue<Employee>("Employee")
                .HasValue<Representative>("Representative");

            modelBuilder.Entity<SpecialPrice>()
               .HasOne(s => s.Governorate)
               .WithMany(g => g.SpecialPrices)
               .HasForeignKey(s => s.GovernorateId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasOne(o => o.Governorate)
               .WithMany(g => g.Orders)
               .HasForeignKey(o => o.GovernorateId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
               .HasMany(b => b.Orders).WithOne(c => c.Branch)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Group>()
               .HasMany(g => g.GroupPermissions)
               .WithOne(gp => gp.Group)
               .HasForeignKey(gp => gp.GroupId);

            modelBuilder.Entity<Permission>()
                .HasMany(p => p.GroupPermissions)
                .WithOne(gp => gp.Permission)
                .HasForeignKey(gp => gp.PermissionId);


            modelBuilder.Entity<Group>()
              .HasMany(g => g.Employees)
              .WithOne(e => e.Group)
              .HasForeignKey(e => e.GroupId);

            base.OnModelCreating(modelBuilder);

        }

        public void SeedData()
        {
            using (var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(this),
                null,
                new PasswordHasher<ApplicationUser>(),
                null,
                null,
                null,
                null,
                null,
                null
            ))
            {
                var adminUsername = "SuperAdmin";

               
                var existingAdmin = userManager.FindByNameAsync(adminUsername).GetAwaiter().GetResult();
                if (existingAdmin == null)
                {
                    var admin = new Employee
                    {
                        UserName = adminUsername,
                        NormalizedUserName = adminUsername.ToUpper(),
                        Email = "admin@shipping.com",
                        NormalizedEmail = "ADMIN@SHIPPING.COM",
                        Address = "Banha",
                        PhoneNumber = "01125186485",
                        Name = "ادمن",
                        BranchId = 1,
                        GroupId = 1
                    };

                    var password = "admin3828#";
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    var hashedPassword = passwordHasher.HashPassword(admin, password);
                    admin.PasswordHash = hashedPassword;

                    userManager.CreateAsync(admin).GetAwaiter().GetResult();

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.Role, "Employee")
            };

                    userManager.AddClaimsAsync(admin, claims).GetAwaiter().GetResult();
                }
            }
        }


    }
}






