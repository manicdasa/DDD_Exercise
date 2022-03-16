using AutoMapper;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;
using System;

namespace GhostWriter.Application.DTOs
{
    public class DocumentDTO : IMapFrom<Document>
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public bool IsFinalVersion { get; set; }
        public DateTime DateCreated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Document, DocumentDTO>();
        }
    }
}
