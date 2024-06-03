﻿using Bookanizer.Server.Model;
using Bookanizer.Shared.DTO;
using Mapster;

namespace Bookanizer.Server.MappingConfig
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Book, BookDTO>.NewConfig()
                .Map(dest => dest.Genres, src => src.BookGenres.Select(bg => bg.Genre))
                .Map(dest => dest.Author, src => src.Author.Adapt<AuthorDTO>());

            TypeAdapterConfig<Genre, GenreDTO>.NewConfig();
            TypeAdapterConfig<Author, AuthorDTO>.NewConfig();
        }
    }
}
