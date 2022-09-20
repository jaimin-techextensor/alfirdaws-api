namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class PeriodTypeCreateRequest
    {
        public string Name { get; set; }
        public int NrOfDays { get; set; }
    }

    public class PeriodTypeUpdateRequest
    {
        public int? PeriodTypeId { get; set; }
        public string Name { get; set; }
        public int NrOfDays { get; set; }
    }
}

