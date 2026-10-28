namespace StoriesOfInclusion.Models
{
    public class StoreProduct
    {
        public int Id { get; set; }
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // "books", "kits", "merch", "sponsor"
        public string CategoryLabelEn { get; set; } = string.Empty;
        public string CategoryLabelBn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionBn { get; set; } = string.Empty;
        public decimal PriceBdt { get; set; }
        public decimal PriceUsd { get; set; }
        public string BadgeTextEn { get; set; } = string.Empty;
        public string BadgeTextBn { get; set; } = string.Empty;
        public string ImpactNoteEn { get; set; } = string.Empty;
        public string ImpactNoteBn { get; set; } = string.Empty;
        public string IconEmoji { get; set; } = "📦";
        public string ImageGradient { get; set; } = "linear-gradient(135deg, #0D9488 0%, #0B1938 100%)";
        public List<string> FeaturesEn { get; set; } = new();
        public List<string> FeaturesBn { get; set; } = new();
        public bool InStock { get; set; } = true;
    }

    public class CartItem
    {
        public StoreProduct Product { get; set; } = default!;
        public int Quantity { get; set; } = 1;
        public decimal SubtotalBdt => Product.PriceBdt * Quantity;
    }

    public class StoreOrder
    {
        public string OrderId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = "Dhaka";
        public string FullAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "bKash";
        public string Notes { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new();
        public decimal DeliveryFee { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}
