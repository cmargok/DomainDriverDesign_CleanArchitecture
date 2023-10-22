using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Contracts.UnitOfWork;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Models.EmailModels;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreatedStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        //  private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreatedStreamerCommandHandler> _logger;

        public CreatedStreamerCommandHandler(
            //  IStreamerRepository streamerRepository,
            IMapper mapper,
            IEmailService emailService,
            ILogger<CreatedStreamerCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            // _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);

            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);

           var result =  await _unitOfWork.Complete();

            if (result <= 0) 
            {
                throw new Exception("no se pudo insertar el record de streamer");
            }

            _logger.LogInformation($"Streamer created with id {streamerEntity.Id}");

            await SendEmail(streamerEntity);

            return streamerEntity.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {

            var email = new Email()
            {
                To = "dykeylu@hotmail.com",
                Body = "La compania de streamer se creo correctamente",
                DisplayName = streamer.Nombre!,
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
