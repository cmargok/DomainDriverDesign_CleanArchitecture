using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public partial class UpdateStreamerHandler : IRequestHandler<UpdateStreamerCommand, Unit>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerHandler> _logger;

        public UpdateStreamerHandler(
            IStreamerRepository streamerRepository, 
            IMapper mapper, 
            ILogger<UpdateStreamerHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _streamerRepository.GetByIdAsync(request.Id);

            if (streamerToUpdate == null)
            {
                _logger.LogError($"No se encontro el streamer con el id {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _mapper.Map(request, streamerToUpdate,typeof(UpdateStreamerCommand),typeof(Streamer));

            await _streamerRepository.UpdateAsync(streamerToUpdate);

            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer con el id {request.Id}");

            return Unit.Value;

        }
    }
}
