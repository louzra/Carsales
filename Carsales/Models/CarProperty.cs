using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Carsales.Models
{
    public class CarProperty
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [Range(1, 5)]
        public int Doors { get; set; }
        [Range(2, 10)]
        public int Wheels { get; set; }
    }
}