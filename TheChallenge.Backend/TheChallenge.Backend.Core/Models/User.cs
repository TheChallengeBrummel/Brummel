﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheChallenge.Backend.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}