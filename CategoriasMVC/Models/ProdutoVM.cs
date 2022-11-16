using System.ComponentModel.DataAnnotations;

namespace CategoriasMVC.Models
{
    public class ProdutoVM
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto")]
        public Decimal Preco { get; set; }

        [Display(Name = "Caminho da imagem")]
        [Required(ErrorMessage = "Informe o caminho da imagem do produto")]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }
    }
}
