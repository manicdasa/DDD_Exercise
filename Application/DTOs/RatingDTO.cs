using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class RatingDTO : IMapFrom<Rate>
    {
        public int Id { get; set; }

        public int StarRating { get; set; }

        public string Comment { get; set; }

        public RateWriter RateWriter { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rate, RatingDTO>();
        }
    }
}
