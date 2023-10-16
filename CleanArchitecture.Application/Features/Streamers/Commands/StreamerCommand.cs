using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class StreamerCommand : IRequest<int>
    {

        public string Nombre { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

    }

    public class StreamerCommandHandler : IRequestHandler<StreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<StreamerCommandHandler> _logger;

        public StreamerCommandHandler(
            IStreamerRepository streamerRepository, 
            IMapper mapper, 
            IEmailService emailService, 
            ILogger<StreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<int> Handle(StreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);

            var entityCreated = await  _streamerRepository.AddAsync(streamerEntity);

            _logger.LogInformation($"Streamer created with id {entityCreated.Id}");

            await SendEmail(entityCreated);

            return entityCreated.Id;
        }

        private async Task SendEmail(Streamer streamer) 
        {

            var email = new Email()
            {
                To = "dykeylu@hotmail.com",
                Body = "La compania de streamer se creo correctamente",
                DisplayName = streamer.Nombre,
                Subject = "Mensaje de alerta",

            };
            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception)
            {
                _logger.LogError("Some problems ocurred trying to send the email");
                throw;
            }
         
       
        }
    }

    
}
