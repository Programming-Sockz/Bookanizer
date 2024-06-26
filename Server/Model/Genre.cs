﻿using System.ComponentModel.DataAnnotations;

namespace Bookanizer.Server.Model
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
