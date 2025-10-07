using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; } // Waiter, Chef, Cashier
        [Required]
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;

    }
}
