namespace ReceiptProcessor.Models
{
    public class Receipt
    {
        public string Retailer { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseTime { get; set; }
        public List<ReceiptItem> Items { get; set; }
        public string Total { get; set; }
    }
}
