using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_gamer_backEnd.Models
{
    public class Jogador
    {
        //propriedades
        [Key]
        public int IdJogador { get; set; }

        [Required]
        public string NomeJogador { get; set; }

        [Required]
        public string EmailJogador { get; set; }

        [Required]
        public string SenhaJogador { get; set; }

        [ForeignKey("Equipe")]
        public int IdEquipe { get; set; }

        public Equipe Equipe { get; set; }
    }
}