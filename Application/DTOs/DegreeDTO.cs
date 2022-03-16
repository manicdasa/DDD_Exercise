using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class DegreeDTO : IMapFrom<Degree>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Degree, DegreeDTO>();
        }
    }
}
