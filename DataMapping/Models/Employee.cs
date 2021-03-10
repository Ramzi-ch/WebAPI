using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataMapping.Models
{
    public class Employee
    {
        //by default EmployeeId will be considered as primary key,
        //because according to standard the column with entityName+Id will be a primary key
        //so we can remove the annotations
        [Column("EmployeeId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Required]
        public int EmployeeId { get; set; }

        [Column("EmployeeName")]
        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        public virtual Country Country { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
