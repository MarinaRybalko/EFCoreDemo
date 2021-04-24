﻿using System;

namespace EFCoreDemo.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual UserProfile Profile { get; set; }
    }
}
