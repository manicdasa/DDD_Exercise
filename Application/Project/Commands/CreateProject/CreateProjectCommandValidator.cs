using FluentValidation;

namespace GhostWriter.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.LanguageId).NotEmpty();
            RuleFor(x => x.ProjectTopic).NotEmpty();
            RuleFor(x => x.KindOfWorkId).NotEmpty();
            RuleFor(x => x.ExpertiseAreaIds).NotEmpty();
            RuleFor(x => x.PricePerPage).NotEmpty();
            RuleFor(x => x.PagesNo).NotEmpty();
        }
    }
}
