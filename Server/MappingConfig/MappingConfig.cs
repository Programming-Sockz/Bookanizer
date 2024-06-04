using Bookanizer.Server.Model;
using Bookanizer.Shared.DTO;
using Mapster;

namespace Bookanizer.Server.MappingConfig
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Book, BookDTO>.NewConfig()
                .Map(dest => dest.Genres, src => src.BookGenres != null && src.BookGenres.Any() ? src.BookGenres.Select(bg => bg.Genre) : null)
                .Map(dest => dest.Tags, src => src.BookTags != null && src.BookTags.Any() ? src.BookTags.Select(bg => bg.Tag) : null)
                .Map(dest => dest.Author, src => src.Author != null ? src.Author.Adapt<AuthorDTO>() : null);

            TypeAdapterConfig<Genre, GenreDTO>.NewConfig();
            TypeAdapterConfig<Tag, TagDTO>.NewConfig();
            TypeAdapterConfig<Author, AuthorDTO>.NewConfig();
        }
    }
}
