using AutoMapper;
using Engie.Domain.Enums;
using Engie.Domain.Interfaces;
using Engie.Domain.Models;

namespace Engie.Business.Implementations
{
    public class ProductionPlanService : IProductionPlanService
    {
        public ProductionPlanService()
        { }

        public List<Response> CreateResponse(Payload payload)
        {
            var list = new List<Response>();
            var gasCost = 0.0;
            var kerosineCost = 0.0;
            var windTurbinePayload = 0;
            CalculateCost(payload, ref gasCost, ref kerosineCost, ref windTurbinePayload);

            double payloadProduced = 0;
            payloadProduced = AddWindTurbines(payload, list, payloadProduced);
            AddFuelPowerPlants(payload, list, gasCost, kerosineCost, payloadProduced);

            return list;
        }

        private static void AddFuelPowerPlants(Payload payload, List<Response> list, double gasCost, double kerosineCost, double payloadProduced)
        {
            foreach (var powerplant in payload.Powerplants)
            {
                if (powerplant.Type != PowerplantType.Windturbine && gasCost > kerosineCost && powerplant.Type == PowerplantType.Turbojet)
                {
                    payloadProduced = CalculateProducedPayload(payload, list, payloadProduced, powerplant);
                }else if (powerplant.Type == PowerplantType.Gasfired)
                {
                    payloadProduced = CalculateProducedPayload(payload, list, payloadProduced, powerplant);
                }
            }
        }

        private static double AddWindTurbines(Payload payload, List<Response> list, double payloadProduced)
        {
            foreach (var windturbine in payload.Powerplants.Where(a => a.Type == PowerplantType.Windturbine))
            {
                if (payloadProduced < payload.Load)
                {
                    payloadProduced = AddWindPayloadToList(payload, list, payloadProduced, windturbine);
                }
            }

            return payloadProduced;
        }

        private static void CalculateCost(Payload payload, ref double gasCost, ref double kerosineCost, ref int windTurbinePayload)
        {
            foreach (var powerplant in payload.Powerplants)
            {
                if (powerplant != null && payload.Fuels != null)
                {
                    switch (powerplant.Type)
                    {
                        case PowerplantType.Windturbine:
                            windTurbinePayload += Convert.ToInt32((powerplant.PMax * powerplant.Efficiency)* (payload.Fuels.Wind/100));
                            break;
                        case PowerplantType.Gasfired:
                            gasCost += (powerplant.Efficiency * powerplant.PMax * payload.Fuels.Gas) + ((powerplant.Efficiency * powerplant.PMax * 0.3) * payload.Fuels.Co2);
                            break;
                        case PowerplantType.Turbojet:
                            kerosineCost += powerplant.Efficiency * powerplant.PMax * payload.Fuels.Kerosine;
                            break;
                    }
                }
            }
        }

        private static double CalculateProducedPayload(Payload payload, List<Response> list, double payloadProduced, Powerplant powerplant)
        {
            if (payloadProduced < payload.Load)
            {
                payloadProduced = CalculateFuelPayload(list, payloadProduced, powerplant, payload);
            }
            else
            {
                payloadProduced = AddFuelPayload(list, payloadProduced, powerplant, 0);
            }

            return payloadProduced;
        }

        private static double CalculateFuelPayload(List<Response> list, double payloadProduced, Powerplant powerplant, Payload payload)
        {
            var maxPayload = Convert.ToDouble((powerplant.PMax * powerplant.Efficiency));
            if (payload.Load >= (maxPayload + payloadProduced))
            {
                payloadProduced = AddFuelPayload(list, payloadProduced, powerplant, maxPayload);
            }
            else
            {
                maxPayload = payload.Load - payloadProduced;
                payloadProduced = AddFuelPayload(list, payloadProduced, powerplant, maxPayload);
            }

            return payloadProduced;
        }

        private static double AddFuelPayload(List<Response> list, double payloadProduced, Powerplant powerplant, double maxPayload)
        {
            list.Add(new Response
            {
                Name = powerplant.Name,
                Payload = maxPayload
            });
            payloadProduced += maxPayload;
            return payloadProduced;
        }

        private static double AddWindPayloadToList(Payload payload, List<Response> list, double payloadProduced, Powerplant powerplant)
        {
            list.Add(new Response
            {
                Name = powerplant.Name,
                Payload = Math.Round(Convert.ToDouble((powerplant.PMax * powerplant.Efficiency)* (payload.Fuels.Wind/100)),2)
            });
            payloadProduced += Math.Round(Convert.ToDouble((powerplant.PMax * powerplant.Efficiency)* (payload.Fuels.Wind/100)));
            return payloadProduced;
        }
    }
}
