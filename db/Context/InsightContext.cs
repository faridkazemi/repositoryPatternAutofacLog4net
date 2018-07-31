using Fiveways.Insight.Model.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;
using Fiveways.Insight.Model.Entities.InsightAuthorization;
using System.Data.Entity.Infrastructure;
using Fiveways.Insight.Model.Entities.Rebate;

namespace Fiveways.Insight.Model.Context
{
    public class MainConfiguration : DbConfiguration
    {
        public MainConfiguration()
        {
            SetExecutionStrategy(providerInvariantName: "System.Data.SqlClient", getExecutionStrategy: () => 
                new SqlAzureExecutionStrategy(maxRetryCount: 3, maxDelay: TimeSpan.FromSeconds(value: 30)));
        }
    }

    [DbConfigurationType(typeof(MainConfiguration))]
    public class InsightContext : DbContext
    {
        public InsightContext()
            : base("name=InsightEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            (this as IObjectContextAdapter).ObjectContext.CommandTimeout = 240;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();

            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 4));
            
            modelBuilder.Entity<CustomerProductPurchaseHistory>().HasKey(x => new {x.TransactionId, x.LineId, x.CustomerId, x.ProductId});
            modelBuilder.Entity<CustomerProductPriceView>().HasKey(x => new { x.CustomerId, x.ProductId});
            modelBuilder.Entity<PantryAreaProduct>().HasKey(x => new { x.PantryAreaId, x.ProductId });
            modelBuilder.Entity<SubscriberSupplier>().HasKey(x => new { x.SubscriberId, x.SupplierId });
            modelBuilder.Entity<SubscriberCustomer>().HasKey(x => new {x.SubscriberId, x.CustomerId});
            modelBuilder.Entity<CustomerPantrySheet>().HasKey(x => new { x.CustomerId, x.PantrySheetId });
            //modelBuilder.Entity<SupplierTransactionLineRebate>()
            //    .HasKey(x => new {x.SupplierTransactionLineId, x.SupplierRebateRuleId});
            modelBuilder.Entity<CustomerProductExclusion>().HasKey(x => new {x.CustomerId, x.ProductId});
            modelBuilder.Entity<InternalUserPermission>().HasKey(x => new { x.InternalUserId, x.PermissionId});
            modelBuilder.Entity<RolePermission>().HasKey(x => new { x.RoleId, x.PermissionId });
            //modelBuilder.Entity<InternalUser>().HasMany(p => p.Roles).WithMany(r => r.InternalUsers).Map(mc => {
            //    mc.MapLeftKey("InternalUserId");
            //    mc.MapRightKey("roleId");
            //    mc.ToTable("InternalUserRole");
            //});

            modelBuilder.Entity<InsightUserPermission>().HasKey(x => new { x.InsightUserId, x.InsightPermissionId });
            modelBuilder.Entity<InsightRolePermission>().HasKey(x => new { x.InsightRoleId, x.InsightPermissionId });
            modelBuilder.Entity<InsightUser>().HasMany(p => p.Roles).WithMany(r => r.InsightUsers).Map(mc => {
                mc.MapLeftKey("InsightUserId");
                mc.MapRightKey("InsightRoleId");
                mc.ToTable("InsightUserRole");
            });

            modelBuilder. Entity<CustomerProductPurchaseHistory>().ToTable("vw_ProductPurchaseHistory");
            modelBuilder.Entity<CustomerProductPriceView>().ToTable("vw_CustomerProductPrice");
            modelBuilder.Entity<CombinedCustomerTransactionHeader>().ToTable("vw_CombinedCustomerTransactionHeaders");
            modelBuilder.Entity<Transaction>().HasMany(x => x.TransactionLines).WithOptional().HasForeignKey(x => x.TransactionId).WillCascadeOnDelete();
            modelBuilder.Entity<SupplierTransaction>().HasMany(x => x.Lines).WithOptional().HasForeignKey(x => x.SupplierTransactionId).WillCascadeOnDelete();
            modelBuilder.Entity<AccountManager>().HasOptional(e => e.ReportsTo).WithMany().HasForeignKey(m => m.ReportsToId);

            modelBuilder.Entity<Customer>()
                .HasMany(s => s.OperationsGroups)
                .WithMany(c => c.Customers)
                .Map(cs => 
                        {
                            cs.MapLeftKey("CustomerRefId");
                            cs.MapRightKey("OperationsGroupRefId");
                            cs.ToTable("CustomerOperationsGroup");
                        });
        }

        public DbSet<CustomerSaleRebate> CustomerSalesRebate { get; set; }
        public DbSet<CustomerProductRebate> CustomerProductRebate { get; set; }
        public DbSet<CustomerProductExclusion> CustomerProductExclusion { get; set; }
        public DbSet<AccountManager> AccountManager { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddresse { get; set; }
        public DbSet<CustomerContact> CustomerContact { get; set; }
        public DbSet<CustomerDocument> CustomerDocument { get; set; }
        public DbSet<CustomerComplaint> CustomerComplaint { get; set; }
        public DbSet<CustomerDriverReturn> CustomerDriverReturn { get; set; }
        public DbSet<PantrySheet> PantrySheet { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<SalesOrder> SalesOrder { get; set; }
        public DbSet<SalesOrderLine> SalesOrderLine { get; set; }
        public DbSet<Subscriber> Subscriber { get; set; }
        public DbSet<SubscriberCustomer> SubscriberCustomer { get; set; }
        public DbSet<Distributor> Distributor { get; set; }
        //public DbSet<SupplierCustomer> SupplierCustomer { get; set; }
        //public DbSet<DistributorProduct> SupplierProduct { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionLine> TransactionLine { get; set; }
        public DbSet<PantrySheetProduct> PantrySheetProduct { get; set; }
        public DbSet<PantrySheetProductPrice> PantrySheetProductPrice { get; set; }
        public DbSet<WebLog> WebLog { get; set; }

        public DbSet<AccountsReceivable> AccountsReceivable { get; set; }

        public DbSet<Zone> Zone { get; set; }

        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }

        public DbSet<UserSecurity> UserSecurity { get; set; }

        public DbSet<PantryArea> PantryArea { get; set; }

        public DbSet<Promotion> Promotion { get; set; }

        public DbSet<PromotionCustomer> PromotionCustomer { get; set; }

        public DbSet<PromotionProduct> PromotionProduct { get; set; }

        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<CustomerIndustry> CustomerIndustry { get; set; }
        public DbSet<OrderTaker> OrderTaker { get; set; }


        public DbSet<PantryAreaProduct> PantryAreaProduct { get; set; }

        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<SupplierAddress> SupplierAddress { get; set; }
        public DbSet<SupplierAddressContact> SupplierAddressContact { get; set; }
        public DbSet<SupplierAttachment> SupplierAttachment { get; set; }
        public DbSet<SupplierContact> SupplierContact { get; set; }
        public DbSet<SupplierProduct> SupplierProduct { get; set; }

        public DbSet<SubscriberSupplier> SubscriberSupplier { get; set; }

        public DbSet<SupplierTransaction> SupplierTransaction { get; set; }

        public DbSet<SupplierTransactionLine> SupplierTransactionLine { get; set; }
        public DbSet<SupplierPriceChange> SupplierPriceChange { get; set; }
        public DbSet<SupplierProductPriceChange> SupplierProductPriceChange { get; set; }

        public DbSet<CustomerPantrySheet> CustomerPantrySheet { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserTokenCache> UserTokenCache { get; set; }

        public DbSet<PublicHoliday> PublicHoliday { get; set; }

        public DbSet<PlanningSession> PlanningSession { get; set; }

        public DbSet<PublicHolidayDateMapping> PublicHolidayDateMapping { get; set; }

        public DbSet<CustomerProductPurchaseHistory> vw_ProductPurchaseHistory { get; set; }
        public DbSet<CustomerProductPriceView> vw_CustomerProductPrice { get; set; }
        public DbSet<CombinedCustomerTransactionHeader> vw_CombinedCustomerTransactionHeader { get; set; }

        public DbSet<InternalUser> InternalUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }

        public DbSet<TransactionArchive> TransactionArchive { get; set; }

        public DbSet<TransactionLineArchive> TransactionLineArchive { get; set; }

        public DbSet<Lead> Leads { get; set; }

        public DbSet<CustomerPrice> CustomerPrices { get; set; }
        public DbSet<CustomerPriceChange> CustomerPriceChanges { get; set; }
        public DbSet<NoteHeader> NoteHeader { get; set; }
        public DbSet<AttachmentHeader> AttachmentHeader { get; set; }

        public DbSet<ProductPricingLevel> ProductPricingLevel { get; set; }
        public DbSet<CustomerProductPricingLevel> CustomerProductPricingLevel { get; set; }

        public DbSet<ProductBrand> ProductBrand { get; set; }

        public DbSet<ProductBuyer> ProductBuyer { get; set; }

        public DbSet<ProductPackage> ProductPackage { get; set; }

        public DbSet<PackageType> ProductType { get; set; }

        public DbSet<CustomerEvent> CustomerEvent { get; set; }
        
        public DbSet<AttachmentType> AttachmentTypes { get; set; }

        public DbSet<PurchaseOrderTracker> PurchaseOrderTracker { get; set; }

        public DbSet<InsightUser> InsightUser { get; set; }

        public DbSet<InsightUserPermission> InsightUserPermission { get; set; }

        public DbSet<InsightRole> InsightRole { get; set; }

        public DbSet<InsightRolePermission> InsightRolePermission { get; set; }


        public DbSet<HpvBasePrice> HpvBasePrice { get; set; }


        public DbSet<HpvCustomerMapping> HpvCustomerMapping { get; set; }


        public DbSet<HpvOrderingPartNumber> HpvOrderingPartNumber { get; set; }


        public DbSet<HpvProductMapping> HpvProductMapping { get; set; }

        public DbSet<Warehouse> Warehouse { get; set; }

        public DbSet<WarehouseProduct> WarehouseProduct { get; set; }

        public DbSet<ProductClass> ProductClass { get; set; }

        public DbSet<ProductGroup> ProductGroup { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<RolePermission> RolePermission { get; set; }

        public DbSet<InternalUserRole> InternalUserRole { get; set; }

        public DbSet<CustomerRebateRule> CustomerRebateRoleConfig { get; set; }

        public DbSet<CustomerRebate> CustomerRebate { get; set; }

        //public DbSet<CustomerTypeRebate> CustomerTypeRebate { get; set; }

        public DbSet<CustomerRebateProduct> CustomerRebateProduct { get; set; }

        public DbSet<CustomerRebateProductCategory> CustomerRebateProductCategory { get; set; }

        public DbSet<SupplierTransactionArchive> SupplierTransactionArchive { get; set; }

        public DbSet<SupplierTransactionLineArchive> SupplierTransactionLineArchive { get; set; }

        public DbSet<CustomerRebateCustomerType> CustomerRebateCustomerType { get; set; }

        public DbSet<CustomerRebateTransaction> CustomerRebateTransaction { get; set; }

        public DbSet<CustomerRebateProductValues> CustomerRebateProductValues { get; set; }

        public DbSet<WarehouseReceiver> WarehouseReceiver { get; set; }

        public DbSet<SupplierRebateRule> SupplierRebateRule { get; set; }

        public DbSet<SupplierRebateSupplier> SupplierRebateSupplier { get; set; }

        public DbSet<SupplierRebateProduct> SupplierRebateProduct { get; set; }
        public DbSet<SupplierRebateCustomer> SupplierRebateCustomer { get; set; }



        public DbSet<CustomerRebateAllocation> CustomerRebateAllocation { get; set; }

        public DbSet<PoConfirmation> PoConfirmation { get; set; }

        public DbSet<CustomerBudget> CustomerBudget { get; set; }

        public DbSet<CustomerActivity> CustomerActivity { get; set; }

        public DbSet<CustomerActivityReason> CustomerActivityReason { get; set; }

        public DbSet<CustomerActivityReasonDescription> CustomerActivityReasonDescription { get; set; }

        public DbSet<CustomerActivityStatus> CustomerActivityStatus { get; set; }

        public DbSet<CustomerActivityHistory> CustomerActivityHistory { get; set; }

        public DbSet<ReportGroup> ReportGroup { get; set; }

        public DbSet<CustomerReportGroup> CustomerReportGroup { get; set; }

        public DbSet<OperationsGroup>  OperationsGroup { get; set; }
        public DbSet<SystemSetting> SystemSetting { get; set; }
        public DbSet<CustomerIndustryBudget> CustomerIndustryBudget { get; set; }
        public DbSet<BudgetGroup> BudgetGroup { get; set; }






    }
}
