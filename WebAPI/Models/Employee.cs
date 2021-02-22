using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int EmployeeId { get; set; }
        
        [Column("EmployeeName")]
        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
