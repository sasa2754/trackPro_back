using MediatR;

namespace TrackPro.Application.Features.Stations.Commands.DeleteStation
{
    public class DeleteStationCommand : IRequest
    {
        public int Id { get; set; }
    }
}