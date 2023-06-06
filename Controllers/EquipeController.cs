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
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        //instância do contexto para acessar o banco de dados
        Context c = new Context();

        [Route("Listar")] //http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            //variável q armazena as equipes listadas do banco de dados
            ViewBag.Equipe = c.Equipe.ToList();

            //retorna a view de equipe (tela)
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //instancia do objeto equipe
            Equipe novaEquipe = new Equipe();

            //atribuição de valores recebidos do formulário
            novaEquipe.NomeEquipe = form["Nome"].ToString();
            //novaEquipe.ImagemEquipe = form["Imagem"].ToString();
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo até o caminho do arquivo (imagem - nome com extensão)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.ImagemEquipe = file.FileName;
            }
            else
            {
                novaEquipe.ImagemEquipe = "padrao.png";
            }

            //adiciona objeto na tabela do banco de dados
            c.Equipe.Add(novaEquipe);

            //salva as alterações feitas no banco de dados
            c.SaveChanges();

            ViewBag.Equipe = c.Equipe.ToList();

            //retorna para o local chamando a rota de listar(método Index)
            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Equipe equipeBuscada = c.Equipe.FirstOrDefault(e => e.IdEquipe == id);

            c.Remove(equipeBuscada);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            Equipe equipe = c.Equipe.First(x => x.IdEquipe == id);

            ViewBag.Equipe = equipe;

            return View("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe equipeEditada = new Equipe();

            equipeEditada.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            equipeEditada.NomeEquipe = form["NomeEquipe"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                equipeEditada.ImagemEquipe = file.FileName;
            }
            else
            {
                equipeEditada.ImagemEquipe = "padrao.png";
            }

            Equipe equipeBuscada = c.Equipe.First(X => X.IdEquipe == equipeEditada.IdEquipe);

            equipeBuscada.NomeEquipe = equipeEditada.NomeEquipe;
            equipeBuscada.ImagemEquipe = equipeEditada.ImagemEquipe;

            c.Equipe.Update(equipeBuscada);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public override bool Equals(object? obj)
        {
            return obj is EquipeController controller &&
                   EqualityComparer<Context>.Default.Equals(c, controller.c);
        }
    }
}