using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_gamer_backEnd.Models
{
    public class Equipe
    {
        //propriedades
        [Key]
        public int IdEquipe { get; set; }

        [Required]
        public string NomeEquipe { get; set; }

        public string ImagemEquipe { get; set; }

        //referencia que a classe equipe vai ter acesso a collection "Jogador"
        public ICollection<Jogador> Jogador { get; set; }
    }
}