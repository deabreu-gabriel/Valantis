using Valantis.Catalogo.Domain.Commands.ProdutoCommands;

namespace Valantis.Catalogo.Domain.Validations.ProdutoValidations
{
    public class AdicionarProdutoCommandValidation : ProdutoValidation<AdicionarProdutoCommand>
    {
        public AdicionarProdutoCommandValidation()
        {
            ValidarNome();
        }
    }
}
