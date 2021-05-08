using System.Collections.Generic;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Mappers
{
    public class ArticleProfile : AutoMapper.Profile
    {
        public ArticleProfile()
        {
            CreateMap<Entities.Project, ProjectWithArticlesDto>();
            CreateMap<ICollection<Article>, List<ShortArticleDto>>();
            CreateMap<Article, ShortArticleDto>();
            CreateMap<Article, FullArticleDto>()
                .ForMember(dto => dto.ProjectTitle, expression => expression.MapFrom(member => member.BelongsTo.Name));
            CreateMap<TeamMember, TeamMemberDto>()
                .ForMember(dto => dto.Email, expression => expression.MapFrom(member => member.User.Email))
                .ForMember(dto => dto.Username, expression => expression.MapFrom(member => member.User.UserName));
            CreateMap<Entities.Project, OptionDto>()
                .ForMember(dto => dto.Value, expression => expression.MapFrom(project => project.Id))
                .ForMember(dto => dto.Label, expression => expression.MapFrom(project => project.Name));
        }
    }
}