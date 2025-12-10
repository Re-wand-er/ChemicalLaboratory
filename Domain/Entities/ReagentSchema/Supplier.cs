namespace Domain.Entities
{
    public class Supplier
    {
        public int idSupplier { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }


        public int? idManufacturer { get; set; }
        public Manufacturer? Manufacturer { get; set; }

    }
}
