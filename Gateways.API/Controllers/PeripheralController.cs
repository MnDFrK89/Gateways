using AutoMapper;
using Gateways.API.BasicResponses;
using Gateways.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Gateways.Data.DTO.Request;
using Gateways.Data.DTO.Response;


namespace Gateways.API.Controllers
{
    [Route("api/peripheral")]
    public class PeripheralController : ControllerBase
    {
        private readonly IPeripheralService _peripheral;
        private readonly IMapper _mapper;
        public PeripheralController(IPeripheralService peripheral, IMapper mapper)
        {
            _peripheral = peripheral ?? throw new ArgumentNullException(nameof(peripheral));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// Add a peripheral to a gateway by the Uid. Status can be {0} OFFLINE or {1} ONLINE
        /// </summary>
        /// <param name="request"></param>
        /// <param name="gatewayUid"></param>
        /// <returns></returns>
        [HttpPost("add-peripheral")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddPeripheral([FromBody] PeripheralRequestDTO request, Guid gatewayUid)
        {
            var data = await _peripheral.AddPeripheralAsync(request, gatewayUid);
            var result = _mapper.Map<PeripheralResponseDTO>(data);
            return Ok(new ApiCreatedResponse(result));
        }

        /// <summary>
        /// Remove a peripheral by it Uid
        /// </summary>
        /// <param name="peripheralUid"></param>
        /// <returns></returns>
        [HttpDelete("remove-peripheral")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemovePeripheral(Guid peripheralUid)
        {
            await _peripheral.RemovePeripheralAsync(peripheralUid);
            return Ok(new ApiOkResponse(200));
        }
    }
}
