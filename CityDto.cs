using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstcoreappbykelvin.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfIntrest { get
            { return Pointsofinterest.Count; }
        }
        public ICollection<PointsOfInterestDto> Pointsofinterest { get; set; }
        = new List<PointsOfInterestDto>();
    }
}
