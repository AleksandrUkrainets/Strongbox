using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Domain.Entities;

namespace Strongbox.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccessRequestDto, AccessRequest>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            CreateMap<AccessRequest, AccessRequestResultDto>()
                .ForMember(dest => dest.AccessRequestId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : "no subject"))
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.Document != null ? src.Document.Name : "no subject"))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<DecisionDto, Decision>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.AccessRequestId, opt => opt.MapFrom(src => src.AccessRequestId))
                .ForMember(dest => dest.ApproverId, opt => opt.MapFrom(src => src.ApproverId))
                .ForMember(dest => dest.AccessRequest, opt => opt.Ignore())
                .ForMember(dest => dest.Approver, opt => opt.Ignore());

            CreateMap<Decision, DecisionResultDto>()
                .ForMember(dest => dest.DecisionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccessRequestId, opt => opt.MapFrom(src => src.AccessRequestId))
                .ForMember(dest => dest.ApproverId, opt => opt.MapFrom(src => src.ApproverId))
                .ForMember(dest => dest.ApproverName, opt => opt.MapFrom(src => src.Approver != null ? src.Approver.Name : string.Empty))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));


            CreateMap<Document, DocumentAttributesResultDto>()
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Access, opt => opt.Ignore());

            CreateMap<Document, DocumentResultDto>()
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DocumentContent, opt => opt.MapFrom(src => src.Content));

            CreateMap<RegisterDto, User>()
                .ForMember(u => u.PasswordHash, o => o.Ignore())
                .ForMember(u => u.PasswordSalt, o => o.Ignore());
        }
    }
}