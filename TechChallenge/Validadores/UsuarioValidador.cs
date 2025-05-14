using FluentValidation;
using TechChallenge.Application.DTO;

namespace TechChallenge.Validadores
{
    public class UsuarioValidador : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidador()
        {

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .Length(3, 50)
                .WithMessage("O nome deve ter entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("E-mail inválido.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("A senha é obrigatória.")
                .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$")
                .WithMessage("A senha deve ter no mínimo 8 caracteres, com letras, números e um caractere especial.");

            RuleFor(x => x.SenhaConfirmacao)
                .NotEmpty()
                .WithMessage("A confirmação de senha é obrigatória.")
                .Equal(x => x.Senha)
                .WithMessage("As senhas não coincidem.");

            RuleFor(x => x.Perfil)
                .NotEmpty()
                .WithMessage("O perfil é obrigatório.")
                .IsInEnum()
                .WithMessage("O perfil deve ser um valor válido do enum.");
        }
    }
}
