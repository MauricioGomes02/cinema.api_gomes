using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Search
    {
        /// <summary>
        /// Termo a ser pesquisado.
        /// </summary>
        [Required]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Ano de lançamento.
        /// </summary>
        public int? ReleaseYear { get; set; }
    }
}
