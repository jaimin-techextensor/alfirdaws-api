namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class ReachTypeCreateRequest
    {
        public string? Name { get; set; }
    }

    public class ReachTypeUpdateRequest
    {
        public int? ReachTypeId { get; set; }
        public string? Name { get; set; }
    }
}

