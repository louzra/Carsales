using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Carsales.Models
{
    public class CommonProperty
    {
        public int Id { get; set; }
        [DisplayName("Vehicle Type")]
        public string VehicleType { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        [DisplayName("Body Type")]
        public string BodyType { get; set; }
    }
}