using FluentValidation;
using Valantis.Catalogo.Domain.Commands.ProdutoCommands;

namespace Valantis.Catalogo.Domain.Validations.ProdutoValidations
{
    public abstract class ProdutoValidation<T> : AbstractValidator<T> where T : ProdutoCommand
    {
        protected void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Por favor, tenha certeza que informou o Nome.");
        }
    }
}
