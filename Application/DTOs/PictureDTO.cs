using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;
using System;

namespace GhostWriter.Application.DTOs
{
    public class PictureDTO : IMapFrom<Picture>
    {
        public int Id { get; set; }
        public string MimeType { get; set; }
        public string SeoFilename { get; set; }
        public string LocalPath { get; set; }
        public string PictureFileName { get; set; }
        public DateTime DateCreated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Picture, PictureDTO>();
        }
    }
}
