using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataMapping.Models
{
    public class Department
    {
        [Column("DepartmentId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DepartmentId { get; set; }

        [Column("DepartmentName")]
        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; }
    }
}
