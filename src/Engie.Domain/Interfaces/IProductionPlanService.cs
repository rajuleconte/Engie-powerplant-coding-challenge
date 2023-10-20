using Engie.Domain.Models;

namespace Engie.Domain.Interfaces
{
    public interface IProductionPlanService
    {  
        List<Response> CreateResponse(Payload payload);
    }
}
