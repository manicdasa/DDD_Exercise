using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class BuzzwordListDTO : IMapFrom<List<Buzzword>>
    {
        public BuzzwordListDTO()
        {
            BuzzwordDTOs = new List<BuzzwordDTO>();
        }
        public IList<BuzzwordDTO> BuzzwordDTOs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<List<Buzzword>, BuzzwordListDTO>();
        }
    }
}
