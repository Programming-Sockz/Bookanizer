﻿using System.ComponentModel.DataAnnotations;

namespace Bookanizer.Server.Model
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookTags> BookTags { get; set; }
    }
}
