using MediatR;
using Valantis.Catalogo.Domain.Validations.ProdutoValidations;

namespace Valantis.Catalogo.Domain.Commands.ProdutoCommands
{
    public class AdicionarProdutoCommand : ProdutoCommand
    {
        public AdicionarProdutoCommand(string nome)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarProdutoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
