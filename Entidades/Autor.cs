using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(5, ErrorMessage = "El campo {0} no debe de tener mas de {1} carácteres")]
        public string Name { get; set; }
        [Range(18,120)]
        [NotMapped]
        public int Edad { get; set; }
        [CreditCard]
        [NotMapped]
        public string TarjetaDeCredito { get; set; }
        [Url]
        [NotMapped]
        public string URL { get; set; }

        public List<Libro> Libros { get; set; }
    }
}
