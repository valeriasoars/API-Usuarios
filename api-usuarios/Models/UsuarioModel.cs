﻿namespace api_usuarios.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Usuario {  get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }

    }
}
