namespace TrackPro.Application.Features.Movements.Queries.GetMovementHistoryByPartCode
{
    public class MovementHistoryDto
    {
        public DateTime Date { get; set; }
        public string? OriginStationName { get; set; }
        public required string DestinationStationName { get; set; }
        public required string Responsible { get; set; }
    }
}