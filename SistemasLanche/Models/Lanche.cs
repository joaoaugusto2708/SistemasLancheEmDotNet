using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemasLanche.Models
{
    public class Lanche
    {
        //Id do lanche
        [Key]
        public int LancheId { get; set; }

        //Nome do lanche
        [Required(ErrorMessage ="O nome do lanche deve ser informado")]
        [Display(Name ="Nome do lanche")]
        [StringLength(80, MinimumLength =10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2}")]
        public string Nome { get; set; }

        //Descrição curta
        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição pode exceder {1} caracteres")]
        public string DescricaoCurta { get; set; }

        //Descrição detalhada
        [Required(ErrorMessage = "A descrição detalhada deve ser informada")]
        [Display(Name = "Descrição detalhada do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada pode exceder {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        //Preço
        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name ="Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99, ErrorMessage ="O preço deve estar entre 1 e 999,999")]
        public decimal Preco { get; set; }

        //Imagem
        [Display(Name = "Caminho Imagem normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no maximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        //Imagem ThumbnailURL
        [Display(Name = "Caminho Imagem miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no maximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        //Lanche Preferido
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        //Em estoque
        [Display(Name = "Estoque")]
        public bool emEstoque { get; set; }

        //Chave estrangeira
        public int CategoriaId { get; set; }

        //Defini relacionamento com a tabela lanches
        public virtual Categoria Categoria { get; set; }

    }
}
