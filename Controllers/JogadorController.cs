using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projeto_gamer_backEnd.Infra;
using projeto_gamer_backEnd.Models;

namespace projeto_gamer_backEnd.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();

            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //instancia do objeto jogador
            Jogador novoJogador = new Jogador();

            //atribuição de valores recebidos do formulário
            novoJogador.NomeJogador = form["Nome"].ToString();
            novoJogador.EmailJogador = form["Email"].ToString();
            novoJogador.SenhaJogador = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["Id da Equipe"].ToString());

            //adiciona objeto na tabela do banco de dados
            c.Jogador.Add(novoJogador);

            //salva as alterações feitas no banco de dados
            c.SaveChanges();

            ViewBag.Jogador = c.Jogador.ToList();

            //retorna para o local chamando a rota de listar(método Index)
            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Jogador jogadorBuscado = c.Jogador.FirstOrDefault(j => j.IdJogador == id);

            c.Remove(jogadorBuscado);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            
            Jogador jogador = c.Jogador.First(j => j.IdJogador == id);

            ViewBag.Jogador = jogador;

            ViewBag.Equipe = c.Equipe.ToList();

            return View("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador jogadorEditado = new Jogador();

            jogadorEditado.IdJogador = int.Parse(form["IdJogador"].ToString());
            jogadorEditado.NomeJogador = form["NomeJogador"].ToString();
            jogadorEditado.EmailJogador = form["EmailJogador"].ToString();
            jogadorEditado.SenhaJogador = form["SenhaJogador"].ToString();
            jogadorEditado.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            Jogador jogadorBuscado = c.Jogador.First(j => j.IdJogador == jogadorEditado.IdJogador);

            jogadorBuscado.NomeJogador = jogadorEditado.NomeJogador;
            jogadorBuscado.EmailJogador = jogadorEditado.EmailJogador;
            jogadorBuscado.SenhaJogador = jogadorEditado.SenhaJogador;
            jogadorBuscado.IdEquipe = jogadorEditado.IdEquipe;

            c.Jogador.Update(jogadorBuscado);
            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}