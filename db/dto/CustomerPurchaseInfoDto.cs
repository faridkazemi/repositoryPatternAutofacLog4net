using System;
using Newtonsoft.Json;

namespace Fiveways.Insight.Model.DTO
{
   public class CustomerPurchaseInfoDto
    {
        [JsonProperty("id")]
        public string CustomerIdInSupplierSystem { get; set; }
        public string ProductType { get; set; }
        public int ProductId { get; set; }
        public string ProductIdInDistributorSystem { get; set; }
        public DateTime LastSaleDate { get; set; }
        public DateTime LastGeneralSaleDate { get; set; }
        public decimal QoH { get; set; }
        public decimal MonthAvgUsage { get; set; }
        public decimal TotalMarketSales { get; set; }
        public decimal MarketSharePercentage { get; set; }
        public decimal DaysStockOnHand { get; set; }
        public decimal WithoutThisCustDaysStockOnHand { get; set; }
        public DateTime LastPurchaseDate { get; set; }
        public ProductDTO Product { get; set; }
        public decimal ThreeMonthsAvgUsage { get; set; }
        public int NumberOfInvoices { get; set; }
        public string StorageType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
