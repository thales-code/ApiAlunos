using ApiAlunos.Application.Repositories.AlunosRepository;
using ApiAlunos.Application.Services.DbSession;
using ApiAlunos.Domain.Entities;
using ApiAlunos.Infrastructure.Models;
using Bogus;
using Dapper;
using Moq;
using Moq.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace ApiAlunos.Tests
{
    public class AlunosRepositoryTest
    {
        private readonly Mock<IDbConnection> _connectionMock;
        private IRepository<Alunos> _alunosRepository;
        private readonly IEnumerable<Alunos> _alunos;

        public AlunosRepositoryTest()
        {
            _connectionMock = new Mock<IDbConnection>();
            _alunos = new Faker<Alunos>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => new Random().Next())
                .RuleFor(x => x.Nome, x => x.Name.FullName())
                .RuleFor(x => x.Cidade, x => x.Address.City())
                .RuleFor(x => x.Estado, x => x.Address.State())
                .RuleFor(x => x.DataNascimento, x => x.Date.Between(new DateTime(1990, 1, 1), new DateTime(2010, 12, 31)))
                .RuleFor(x => x.DataMatricula, x => DateTime.UtcNow)
                .Generate(20)
                .AsEnumerable();
        }

        [Fact]
        public async void GetAllTest()
        {
            //arange
            _connectionMock.SetupDapperAsync(c => c.QueryAsync<Alunos>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(_alunos);

            _alunosRepository = new AlunosRepository(new DbSession(_connectionMock.Object));
            //act
            var results = await _alunosRepository.GetAll();

            //assert
            Assert.Equal(_alunos.Count(), results.Count());

            foreach (var alunoResult in results)
            {
                var aluno = _alunos.First(a => a.Id == alunoResult.Id);
                Assert.Equal(aluno.Id, alunoResult.Id);
                Assert.Equal(aluno.Estado, alunoResult.Estado);
                Assert.Equal(aluno.Cidade, alunoResult.Cidade);
                Assert.Equal(aluno.DataMatricula, alunoResult.DataMatricula);
                Assert.Equal(aluno.DataNascimento, alunoResult.DataNascimento);
                Assert.Equal(aluno.Nome, alunoResult.Nome);
            }
        }

        [Fact]
        public async void GetById()
        {
            //arange
            var aluno = new Faker().PickRandom(_alunos);
            _connectionMock.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<Alunos>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(aluno);

            _alunosRepository = new AlunosRepository(new DbSession(_connectionMock.Object));
            //act
            var alunoResult = await _alunosRepository.GetById(aluno.Id);

            //assert
            Assert.Equal(aluno.Id, alunoResult.Id);
            Assert.Equal(aluno.Estado, alunoResult.Estado);
            Assert.Equal(aluno.Cidade, alunoResult.Cidade);
            Assert.Equal(aluno.DataMatricula, alunoResult.DataMatricula);
            Assert.Equal(aluno.DataNascimento, alunoResult.DataNascimento);
            Assert.Equal(aluno.Nome, alunoResult.Nome);
        }
    }
}
