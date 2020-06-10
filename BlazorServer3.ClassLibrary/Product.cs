namespace BlazorServer3.ClassLibrary
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageFilePath { get; set; }
    }
}
