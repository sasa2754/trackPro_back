using MediatR;

namespace TrackPro.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationCommand : IRequest<int>
    {
        public required string Name { get; set; }
        public int Order { get; set; }
    }
}