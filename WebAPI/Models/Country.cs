using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Country
    {
        [Column("CountryId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CountryId { get; set; }

        [Column("CountryName")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("Population")]
        [Required]
        public Int64 Population { get; set; }
    }
}
