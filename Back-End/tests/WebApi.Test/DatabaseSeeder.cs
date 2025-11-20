using controleDespesa.Domain.Entities;
using controleDespesa.Domain.Enums;
using controleDespesa.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test
{
    

        public static class DatabaseSeeder
        {
            public static void Seed(ApiContext db)
            {
            db.TipoDespesaReceitas.Add(new TipoDespesaReceita
            {
                Id = 1,
                Nome = "Despesa Fixa",
                Ativo = true,
                DataCadastro = DateTime.Now,
                Tipo = TipoDespesaReceitaEnum.DESPESA

            });
            db.TipoDespesaReceitas.Add(new TipoDespesaReceita
            {
                Id = 2,
                Nome = "Moradia",
                Ativo = true,
                DataCadastro = DateTime.Now,
                Tipo = TipoDespesaReceitaEnum.DESPESA

            });
            db.TipoDespesaReceitas.Add(new TipoDespesaReceita
            {
                Id = 3,
                Nome = "Alimentação",
                Ativo = true,
                DataCadastro = DateTime.Now,
                Tipo = TipoDespesaReceitaEnum.DESPESA

            });
            db.TipoDespesaReceitas.Add(new TipoDespesaReceita
            {
                Id = 4,
                Nome = "Transporte",
                Ativo = true,
                DataCadastro = DateTime.Now,
                Tipo = TipoDespesaReceitaEnum.DESPESA

            });

                db.SaveChanges();
            }
        }
    }

