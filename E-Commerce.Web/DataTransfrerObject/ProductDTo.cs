namespace E_Commerce.Web.DataTransfrerObject
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } =     default!;
        public string Description { get; set; } = default!;
        public
             string PictureUrl
        { get; set; } = default!;

        public decimal price {  get; set; } 
        public decimal BrandName {  get; set; }=default!;
        public decimal TypeName {  get; set; } =default!;
    }
}
