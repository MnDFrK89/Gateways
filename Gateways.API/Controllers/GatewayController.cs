using AutoMapper;
using Gateways.API.BasicResponses;
using Gateways.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Gateways.Data.DTO.Response;
using Gateways.Data.DTO.Request;

namespace Gateways.API.Controllers
{
    [Route("api/gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGatewayService _gateway;
        public GatewayController(IMapper mapper, IGatewayService gateway)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _gateway = gateway ?? throw new ArgumentNullException(nameof(gateway));
        }

        /// <summary>
        /// Get all Gateways.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            var data = _gateway.GetAll();
            var result = _mapper.Map<ICollection<GatewayResponseDTO>>(data);
            return Ok(new ApiOkResponse(result));
        }

        /// <summary>
        /// Add a new gateway, the IPV4 attribute is a required value. Peripheral status can be {0} OFFLINE or {1} ONLINE.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add-gateway")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddGateway([FromBody] GatewayRequestDTO request)
        {
            await _gateway.AddGatewayAsync(request);
            return Ok(new ApiOkResponse(200));
        }

        /// <summary>
        /// Get a gateway by Uid
        /// </summary>
        /// <param name="gatewayUid"></param>
        /// <returns></returns>
        [HttpGet("get-gateway")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetGateway(Guid gatewayUid)
        {
            var data = _gateway.GetGateway(gatewayUid);
            var result = _mapper.Map<GatewayResponseDTO>(data);
            return Ok(new ApiOkResponse(result));
        }
    }
}