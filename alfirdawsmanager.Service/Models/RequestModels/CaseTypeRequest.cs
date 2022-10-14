namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class CaseTypeCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }

    public class CaseTypeUpdateRequest
    {
        public int CaseTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}

