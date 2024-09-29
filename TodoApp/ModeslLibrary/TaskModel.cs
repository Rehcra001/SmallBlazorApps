using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeslLibrary
{
    public class TaskModel
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A person must be selected!")]
        public int PersonId { get; set; }
        [Required]
        public String Task { get; set; } = string.Empty;
        [Required(ErrorMessage = "Date to be completed is required!")]
        public int PercentageComplete { get; set; }
        [Required]
        public DateTime? DateToBeCompleted { get; set; } = null;
        public DateTime? DateCompleted { get; set; }
    }
}
