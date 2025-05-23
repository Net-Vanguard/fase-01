﻿using System.ComponentModel.DataAnnotations;
using TechChallenge.Domain.Entities.Usuario;

namespace TechChallenge.Application.DTO
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string SenhaConfirmacao { get; set; }

        public EnumPerfil Perfil { get; set; }
    }
}

