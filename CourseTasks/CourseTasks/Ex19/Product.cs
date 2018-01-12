namespace CourseTasks.Ex19
{
   public class Product
    {
        public Product(string name, string code, string size, string countOf)
        {
            Name = name;
            Code = code;
            Size = size;
            CountOf = countOf;
        }
        public Product()
        {
            Size = "Small";
            Name = "Product";
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Size { get; set; }
        public string CountOf { get; set; }
    }
}