using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class HomeVM
    {
        public Slayd Slayd { get; set; }
        public List<Support> Supports { get; set; }
        public Festival Festival { get; set; }
        public Offer Offer { get; set; }
        public Gift Gift { get; set; }
        public List<Sponsor> Sponsors { get; set; }
    }
}
