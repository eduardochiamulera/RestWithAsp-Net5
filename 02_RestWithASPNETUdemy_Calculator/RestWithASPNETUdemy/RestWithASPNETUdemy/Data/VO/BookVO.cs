﻿using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Data.VO
{
    public class BookVO : ISupporstHyperMedia
    {
        public long Id { get; set; }

        public string Author { get; set; }

        public DateTime LauchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }
        
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
