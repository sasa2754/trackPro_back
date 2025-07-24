using MediatR;

namespace TrackPro.Application.Features.Stations.Commands.UpdateStation
{
    public class UpdateStationCommand : IRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Order { get; set; }
    }
}