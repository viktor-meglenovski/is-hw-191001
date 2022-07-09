using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Projection SelectedProjection { get; set; }
        public Guid SelectedProjectionId { get; set; }
        public int Quantity { get; set; }
    }
}
