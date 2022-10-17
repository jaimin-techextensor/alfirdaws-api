namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class CaseCategoryCreateRequest
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public class CaseCategoryUpdateRequest
    {
        public int CaseCategoryId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
