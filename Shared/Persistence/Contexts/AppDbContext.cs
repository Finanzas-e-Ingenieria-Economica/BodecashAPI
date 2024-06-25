using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<InstallmentPlan> InstallmentPlans { get; set; }
    public DbSet<IPPayment> IPPayments { get; set; }
    public DbSet<IPPaymentProduct> IPPaymentProducts { get; set; }
    public DbSet<NormalPurchase> NormalPurchases { get; set; }
    public DbSet<NPPurchase> NPPurchases { get; set; }
    public DbSet<NPPurchaseProduct> NPPurchaseProducts { get; set; }
    public DbSet<PersonalData> PersonalDatas { get; set; }
    public DbSet<Shopkeeper> Shopkeepers { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<Client>().HasKey(p => p.Id);
        builder.Entity<Client>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Credit>().ToTable("Credits");
        builder.Entity<Credit>().HasKey(p => p.Id);
        builder.Entity<Credit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Credit>().Property(p => p.InterestRate).IsRequired();
        builder.Entity<Credit>().Property(p => p.Capitalization);
        builder.Entity<Credit>().Property(p => p.PenaltyInterestRate).IsRequired();
        builder.Entity<Credit>().Property(p => p.DisbursementDate).IsRequired();
        builder.Entity<Credit>().Property(p => p.Type).IsRequired();
        builder.Entity<Credit>().Property(p => p.CreditAmount).IsRequired();
        builder.Entity<Credit>().Property(p => p.UsedCredit).IsRequired();
        builder.Entity<Credit>().Property(p => p.RemainingCredit).IsRequired();

        builder.Entity<InstallmentPlan>().ToTable("InstallmentPlans");
        builder.Entity<InstallmentPlan>().HasKey(p => p.Id);
        builder.Entity<InstallmentPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<InstallmentPlan>().Property(p => p.Fee).IsRequired();
        builder.Entity<InstallmentPlan>().Property(p => p.IsGracePeriod).IsRequired();
        builder.Entity<InstallmentPlan>().Property(p => p.GracePeriodType);
        builder.Entity<InstallmentPlan>().Property(p => p.GracePeriodPeriods);
        builder.Entity<InstallmentPlan>().Property(p => p.TotalTerm).IsRequired();
        builder.Entity<InstallmentPlan>().Property(p => p.PaymentTimeType).IsRequired();

        builder.Entity<IPPayment>().ToTable("IPPayments");
        builder.Entity<IPPayment>().HasKey(p => p.Id);
        builder.Entity<IPPayment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<IPPayment>().Property(p => p.Capital).IsRequired();
        builder.Entity<IPPayment>().Property(p => p.Interest).IsRequired();
        builder.Entity<IPPayment>().Property(p => p.Fee).IsRequired();
        builder.Entity<IPPayment>().Property(p => p.Amortization).IsRequired();
        builder.Entity<IPPayment>().Property(p => p.DueDate).IsRequired();
        builder.Entity<IPPayment>().Property(p => p.IsPaid).IsRequired();
        builder.Entity<IPPayment>().Property(p => p.DaysPastDue);
        builder.Entity<IPPayment>().Property(p => p.PaymentDate).IsRequired();
        
        builder.Entity<IPPaymentProduct>().ToTable("IPPaymentProducts");
        builder.Entity<IPPaymentProduct>().HasKey(p => p.Id);
        builder.Entity<IPPaymentProduct>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<IPPaymentProduct>().Property(p => p.Name).IsRequired();
        builder.Entity<IPPaymentProduct>().Property(p => p.Quantity).IsRequired();
        builder.Entity<IPPaymentProduct>().Property(p => p.TotalValue).IsRequired();
        builder.Entity<IPPaymentProduct>().Property(p => p.PurchaseDate).IsRequired();
        
        builder.Entity<NormalPurchase>().ToTable("NormalPurchases");
        builder.Entity<NormalPurchase>().HasKey(p => p.Id);
        builder.Entity<NormalPurchase>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<NormalPurchase>().Property(p => p.AmountDue).IsRequired();
        builder.Entity<NormalPurchase>().Property(p => p.DueDate).IsRequired();
        builder.Entity<NormalPurchase>().Property(p => p.PaymentDate).IsRequired();
        builder.Entity<NormalPurchase>().Property(p => p.IsPaid).IsRequired();
        builder.Entity<NormalPurchase>().Property(p => p.DaysPastDue);
        
        builder.Entity<NPPurchase>().ToTable("NPPurchases");
        builder.Entity<NPPurchase>().HasKey(p => p.Id);
        builder.Entity<NPPurchase>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<NPPurchase>().Property(p => p.PurchaseDate).IsRequired();
        builder.Entity<NPPurchase>().Property(p => p.TotalValue).IsRequired();
        
        builder.Entity<NPPurchaseProduct>().ToTable("NPPurchaseProducts");
        builder.Entity<NPPurchaseProduct>().HasKey(p => p.Id);
        builder.Entity<NPPurchaseProduct>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<NPPurchaseProduct>().Property(p => p.Name).IsRequired();
        builder.Entity<NPPurchaseProduct>().Property(p => p.Quantity).IsRequired();
        builder.Entity<NPPurchaseProduct>().Property(p => p.Value).IsRequired();
        
        builder.Entity<PersonalData>().ToTable("PersonalDatas");
        builder.Entity<PersonalData>().HasKey(p => p.Id);
        builder.Entity<PersonalData>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PersonalData>().Property(p => p.Name).IsRequired();
        builder.Entity<PersonalData>().Property(p => p.Email).IsRequired();
        builder.Entity<PersonalData>().Property(p => p.Password).IsRequired();
        builder.Entity<PersonalData>().Property(p => p.DNI).IsRequired();
        builder.Entity<PersonalData>().Property(p => p.UserType).IsRequired();
        
        builder.Entity<Shopkeeper>().ToTable("Shopkeepers");
        builder.Entity<Shopkeeper>().HasKey(p => p.Id);
        builder.Entity<Shopkeeper>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Shopkeeper>().Property(p => p.Store).IsRequired();
        
        // Relationships
        builder.Entity<PersonalData>()
            .HasOne(p => p.Client)
            .WithOne(p => p.PersonalData)
            .HasForeignKey<Client>(p => p.PersonalDataId);
        
        builder.Entity<PersonalData>()
            .HasOne(p => p.Shopkeeper)
            .WithOne(p => p.PersonalData)
            .HasForeignKey<Shopkeeper>(p => p.PersonalDataId);
        
        builder.Entity<Client>()
            .HasMany(p => p.Credits)
            .WithOne(p=>p.Client)
            .HasForeignKey(p => p.ClientId);
        
        builder.Entity<Shopkeeper>()
            .HasMany(p => p.Credits)
            .WithOne(p=>p.Shopkeeper)
            .HasForeignKey(p => p.ShopkeeperId);
        
        builder.Entity<Credit>()
            .HasMany(p => p.NormalPurchases)
            .WithOne(p=>p.Credit)
            .HasForeignKey(p => p.CreditId);
        
        builder.Entity<Credit>()
            .HasMany(p => p.InstallmentPlans)
            .WithOne(p=>p.Credit)
            .HasForeignKey(p => p.CreditId);

        builder.Entity<NormalPurchase>()
            .HasMany(p => p.NPPurchases)
            .WithOne(p => p.NormalPurchase)
            .HasForeignKey(p => p.NormalPurchaseId);

        builder.Entity<NPPurchase>()
            .HasMany(p => p.NPPurchaseProducts)
            .WithOne(p => p.NPPurchase)
            .HasForeignKey(p => p.NPPurchaseId);

        builder.Entity<InstallmentPlan>()
            .HasMany(p => p.IPPayments)
            .WithOne(p => p.InstallmentPlan)
            .HasForeignKey(p => p.InstallmentPlanId);

        builder.Entity<IPPayment>()
            .HasMany(p => p.IPPaymentProducts)
            .WithOne(p => p.IPPayment)
            .HasForeignKey(p => p.IPPaymentId);
        
        // Apply Snake Case Naming Convention
        builder.UseSnakeCaseNamingConvention();
    }
}