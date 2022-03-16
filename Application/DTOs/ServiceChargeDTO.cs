using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class ServiceChargeDTO : IMapFrom<ServiceCharge>
    {
        public int Id { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool IsPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ServiceChargeTypeDTO ServiceChargeTypeDTO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ServiceCharge, ServiceChargeDTO>()
                .ForMember(d => d.ServiceChargeTypeDTO, opt => opt.MapFrom(s => s.ServiceChargeType)); ;
        }
    }
}
