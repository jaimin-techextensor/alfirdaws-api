namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class CasePriorityCreateRequest
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }

    public class CasePriorityUpdateRequest
    {
        public int CasePriorityId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}

