namespace TrackPro.Application.Features.Parts.Queries.GetPartByCode
{
    public class PartDetailDto
    {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string CurrentStationName { get; set; }
    }
}