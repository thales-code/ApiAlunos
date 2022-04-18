using ApiAlunos.Application.Commands.AlunosCommands;
using ApiAlunos.Domain.Entities;
using AutoMapper;
using System;

namespace ApiAlunos.Infrastructure.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AlteraAlunosCommand, Alunos>();
            CreateMap<CadastraAlunosCommand, Alunos>();
            CreateMap<AtualizaAlunosCommand, Alunos>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null && IsDateEmpty(srcMember)));
        }

        private static bool IsDateEmpty(object srcMember)
        {
            if (srcMember is DateTime)
            {
                return (DateTime)srcMember != DateTime.MinValue;
            }


            return true;
        }
    }
}
