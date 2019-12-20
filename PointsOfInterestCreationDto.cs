using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace firstcoreappbykelvin.Models
{
    public class PointsOfInterestCreationDto
    {
       [Required(ErrorMessage ="Please provide a name value")]
       [MaxLength(50)]
       public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }
}

