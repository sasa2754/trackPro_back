using MediatR;
using System.Net;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Application.Exceptions;
using TrackPro.Domain.Entities;

namespace TrackPro.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationCommandHandler : IRequestHandler<CreateStationCommand, int>
    {
        private readonly IStationRepository _stationRepository;

        public CreateStationCommandHandler(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<int> Handle(CreateStationCommand request, CancellationToken cancellationToken)
        {
            var existingStationWithOrder = await _stationRepository.GetByOrderAsync(request.Order);
            if (existingStationWithOrder != null)
            {
                throw new ApiException(HttpStatusCode.BadRequest, $"A station with Order {request.Order} already exists.");
            }

            var station = new Station() { Name = request.Name, Order = request.Order };

            station = await _stationRepository.AddAsync(station);

            return station.Id;
        }
    }
}