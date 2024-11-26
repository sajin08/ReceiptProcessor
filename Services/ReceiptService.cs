using ReceiptProcessor.Models;

namespace ReceiptProcessor.Services
{
    public class ReceiptService
    {
        private readonly Dictionary<string, int> _receiptPoints = new Dictionary<string, int>();
        public string ProcessReceipt(Receipt receipt)
        {
            var id = Guid.NewGuid().ToString();
            var points = CalculatePoints(receipt);
            _receiptPoints[id] = points;
            return id;
        }
        public int GetPoints(string id) => _receiptPoints.TryGetValue(id, out var points) ? points : 0;

        private int CalculatePoints(Receipt receipt)
        {
            int points = 0;
            // One point for every alphanumeric character in the retailer name.
            points += receipt.Retailer.Count(char.IsLetterOrDigit);
            // 50 points if the total is a round dollar amount with no cents.
            if (decimal.TryParse(receipt.Total, out var total))
            {
                if (total % 1 == 0) { points += 50; }
                // 25 points if the total is a multiple of 0.25.
                if (total % 0.25m == 0) { points += 25; }
            }
            // 5 points for every two items on the receipt.
            points += (receipt.Items.Count / 2) * 5;
            // Points for item descriptions and price multiples.
            foreach (var item in receipt.Items)
            {
                if (item.ShortDescription.Trim().Length % 3 == 0 && decimal.TryParse(item.Price, out var itemPrice))
                { points += (int)Math.Ceiling(itemPrice * 0.2m); }
            }
            // 6 points if the day in the purchase date is odd.
            if (DateTime.TryParse(receipt.PurchaseDate, out var purchaseDate) && purchaseDate.Day % 2 != 0)
            { points += 6; }
            // 10 points if the time of purchase is after 2:00pm and before 4:00pm.
            if (TimeSpan.TryParse(receipt.PurchaseTime, out var purchaseTime) && purchaseTime.Hours >= 14 && purchaseTime.Hours < 16)
            { points += 10; }
            return points;
        }
    }
}
