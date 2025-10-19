using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.MenuItem
{
    public class UpdateMenuItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        public bool IsAvailable { get; set; }
    }
}
