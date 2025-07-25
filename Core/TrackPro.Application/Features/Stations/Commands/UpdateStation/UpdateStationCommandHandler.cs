using MediatR;
using System.Net;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Application.Exceptions;
using TrackPro.Domain.Entities;

namespace TrackPro.Application.Features.Stations.Commands.UpdateStation
{
    public class UpdateStationCommandHandler : IRequestHandler<UpdateStationCommand>
    {
        private readonly IStationRepository _stationRepository;

        public UpdateStationCommandHandler(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task Handle(UpdateStationCommand request, CancellationToken cancellationToken)
        {
            var stationToUpdate = await _stationRepository.GetByIdAsync(request.Id);
            if (stationToUpdate == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Station with Id {request.Id} not found.");
            }

            var existingStationWithOrder = await _stationRepository.GetByOrderAsync(request.Order);
            if (existingStationWithOrder != null && existingStationWithOrder.Id != request.Id)
            {
                throw new ApiException(HttpStatusCode.BadRequest, $"Another station with Order {request.Order} already exists.");
            }

            stationToUpdate.Name = request.Name;
            stationToUpdate.Order = request.Order;

            await _stationRepository.UpdateAsync(stationToUpdate);
        }
    }
}