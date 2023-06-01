using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projeto_gamer_backEnd.Models;

namespace projeto_gamer_backEnd.Infra
{
    public class Context : DbContext
    {
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string de conexão com o banco de dados
                //Data Source: o nome do servidor do gerenciador do banco
                //initial catalog: nome do banco de dados

                //Autenticação pelo Windows
                //Integrated Security: autenticação pelo windows
                //TrustServerCertificate: autenticação pelo windows

                //Autenticação pelo SQL Server
                //User Id = "nome do seu usuário de login"
                //password = "senha do seu usuário"

                optionsBuilder.UseSqlServer("Data Source = DESKTOP-VLQ1I1C; initial catalog = gamerManha; User Id = sa; pwd = Senai@134; TrustServerCertificate = true");
            }
        }

        //referência de classes e tabelas
        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<Equipe> Equipe { get; set; }
    }
}