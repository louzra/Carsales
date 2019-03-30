using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Carsales.Models
{
    public class BoatProperty
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [Required]
        [Range(10.00, 100.00)]
        public decimal Length { get; set; }
        [Required]
        [DisplayName("Propulsion Type")]
        public string PropulsionType { get; set; }
        [Required]
        [DisplayName("Fishing Rod Holders")]
        public string FishingRodHolders { get; set; }
    }
}