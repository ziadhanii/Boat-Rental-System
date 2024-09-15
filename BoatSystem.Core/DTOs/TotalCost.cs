﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.DTOs
{
    public class TotalCostRequestDto
    {
        public int TripId { get; set; }
        public int NumberOfPeople { get; set; }
        public List<int> AdditionalServiceIds { get; set; }
    }

    public class TotalCostResponseDto
    {
        public decimal TotalPrice { get; set; }
    }

}
