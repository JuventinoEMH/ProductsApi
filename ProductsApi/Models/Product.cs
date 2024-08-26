namespace ProductsApi.Models
{
    public class Product
    {

        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string category { get; set; }
        public int discount { get; set; }
    }
}
