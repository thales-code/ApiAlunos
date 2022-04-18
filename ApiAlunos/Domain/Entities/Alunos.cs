using System;

namespace ApiAlunos.Domain.Entities
{
    public class Alunos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public DateTime DataMatricula { get; set; }
    }
}
