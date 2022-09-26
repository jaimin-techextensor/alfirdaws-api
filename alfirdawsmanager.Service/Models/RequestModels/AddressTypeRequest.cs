namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class AddressTypeCreateRequest
    {
        public string Name { get; set; }
    }

    public class AddressTypeUpdateRequest
    {
        public int AddressTypeId { get; set; }
        public string Name { get; set; }
    }
}

