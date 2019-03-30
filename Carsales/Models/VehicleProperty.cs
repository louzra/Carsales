using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Carsales.Models
{
    public class VehicleProperty
    {
        public CommonProperty CommonProperty { get; set; }
        public List<VehicleType> VehicleType { get; set; }
        public CarProperty CarProperty { get; set; }
        public BoatProperty BoatProperty { get; set; }
        public PagedList<CommonProperty> ListCommonProperty { get; set; }
    }
}