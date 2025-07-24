namespace TrackPro.Application.Features.Stations.Queries.GetStationList
{
    public class StationListDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Order { get; set; }
    }
}