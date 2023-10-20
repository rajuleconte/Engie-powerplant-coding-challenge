using AutoMapper;
using Engie.Contracts.Dto;
using Engie.Domain.Interfaces;
using Engie.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Engie.WebApi.Controllers
{
    [ApiController]
    [Route("ProductionPlan")]
    public class PayloadController : ControllerBase
    {
        private readonly ILogger<PayloadController> _logger;
        private readonly IProductionPlanService _productionPlanService;
        private readonly IMapper _mapper;
        public PayloadController(ILogger<PayloadController> logger, IProductionPlanService payloadService, IMapper mapper)
        {
            _logger = logger;
            _productionPlanService = payloadService;
            _mapper = mapper;
        }

        [HttpPost(Name = "ProductionPlan")]        
        [Consumes("application/json")]
        public IActionResult ProductionPlan([FromBody] [Required] PayloadDto payloadDto)
        {
            try
                {
                if(payloadDto == null)
                {  
                    _logger.LogError("something wrong with reading the json");
                    return BadRequest();
                }
                var payload = _mapper.Map<Payload>(payloadDto);
                var response = _productionPlanService.CreateResponse(payload);

                var responseDto = _mapper.Map<List<ResponseDto>>(response);
                return Ok(responseDto);
            }catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
            }
            return BadRequest();
        }
    }
}
