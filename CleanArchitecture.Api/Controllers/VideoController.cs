using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.Api.Controllers
{


    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //private readonly ILogger<VideoController> _logger;

        //public VideoController(ILogger<VideoController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet("{username}", Name = "GetVideo")]
        [ProducesResponseType(typeof(IEnumerable<VideoVm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVideosByUserName(string username)
        {
            //instanciamos el command
            var query = new GetVideosListQuery(username);

            //enviamos el command al mediator y el sabe que va dirigido
            // al respectivo handleer
            var result = await _mediator.Send(query);

            //retornamos la respuesta
            return Ok(result);
        }
    }
}