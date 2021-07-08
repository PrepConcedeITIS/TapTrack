using System.Collections.Generic;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.KnowledgeBase.ById;
using TapTrackAPI.Core.Features.KnowledgeBase.List;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.KnowledgeBase
{
    public class ArticleProfile : AutoMapper.Profile
    {
        public ArticleProfile()
        {
            CreateMap<Entities.Project, ProjectWithArticlesDto>();
            CreateMap<ICollection<Article>, List<ShortArticleDto>>();
            CreateMap<Article, ShortArticleDto>();
            CreateMap<Article, FullArticleDto>()
                .ForMember(dist => dist.CreatedBy,
                    expression => expression.MapFrom(source => source.CreatedBy.User))
                .ForMember(dist => dist.UpdatedBy,
                    expression => expression.MapFrom(source => source.UpdatedBy.User))
                .ForMember(dist => dist.ProjectTitle,
                    expression => expression.MapFrom(source => source.BelongsTo.Name));
            CreateMap<User, UserDto>();
            CreateMap<Entities.Project, OptionDto>()
                .ForMember(dto => dto.Value, expression => expression.MapFrom(project => project.Id))
                .ForMember(dto => dto.Label, expression => expression.MapFrom(project => project.Name));
        }
    }
}