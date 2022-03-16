using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class BuzzwordDTO : IMapFrom<Buzzword>
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Buzzword, BuzzwordDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.Id))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value));
        }
    }
}
