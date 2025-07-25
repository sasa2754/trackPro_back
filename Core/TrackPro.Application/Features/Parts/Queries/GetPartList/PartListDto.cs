namespace TrackPro.Application.Features.Parts.Queries.GetPartList
{
    public class PartListDto
    {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
    }
}