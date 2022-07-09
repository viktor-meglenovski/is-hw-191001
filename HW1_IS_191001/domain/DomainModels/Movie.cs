using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace domain.DomainModels
{
    public class Movie:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public double Rating { get; set; }

        public virtual List<Projection> Projections { get; set; }
    }
}
