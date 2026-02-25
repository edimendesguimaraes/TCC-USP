using FluentValidation;
using Zeladoria.Application.DTOs;

namespace Zeladoria.Application.Validators;

public class NovaOcorrenciaDtoValidator : AbstractValidator<NovaOcorrenciaDto>
{
    public NovaOcorrenciaDtoValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título da ocorrência é obrigatório.")
            .Length(5, 100).WithMessage("O título deve ter entre 5 e 100 caracteres.");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(500).WithMessage("A descrição não pode passar de 500 caracteres.");        
        
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-23.18, -22.98)
            .WithMessage("A latitude informada está fora dos limites do município de Indaiatuba.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-47.32, -47.08)
            .WithMessage("A longitude informada está fora dos limites do município de Indaiatuba.");
    }
}