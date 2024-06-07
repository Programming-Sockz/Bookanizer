namespace Bookanizer.Shared.Libraries
{
    //hier stehen alle API Routen sortiert drinnen, die wir benutzen. Das wird nicht gebracuht aber sammelt alle Routen einfach für den Überblick
    public static class ApiRoutes
    {
        //diese Unterteilt man einfach nur für übersicht in Klassen
        public static class Books
        {
            //Wir wollen auf unseren BooksController. Diese Route wird vom Programm automatisch erstellt aus dem Namen der .cs file. (Controller wird vom namen hier ignoriert)
            const string _base = "/api/Books";
            public static string GET()
                => $"{_base}";
            public static string GET_ByName(string name)
                => $"{_base}/name/{name}";
            public static string GET_ById(Guid id)
                => $"{_base}/{id}";
            public static string POST()
                => $"{_base}";
            public static string PUT(Guid id)
                => $"{_base}/{id}";
            public static string GET_AllWithFullInfo()
                => $"{_base}/withFullInfo";
            public static string GET_WithFullInfo(Guid id)
                => $"{_base}/withFullInfo/{id}";
            public static string GET_ByAuthorId(Guid id)
                => $"{_base}/author/{id}";
        }

        public static class User
        {
            const string _base = "/api/User";

            public static string GET_Users()
                => $"{_base}/users";
            public static string GET_UserById(Guid id)
                => $"{_base}/user/{id}";
            public static string POST_Register()
                => $"{_base}/register";
            public static string POST_Login()
                => $"{_base}/login";
            public static string PUT_DeactivateUser(Guid id)
                => $"{_base}/deactivate/{id}";
            public static string PUT_ActivateUser(Guid id)
                => $"{_base}/activate/{id}";
        }
        
        public static class Author
        {
            const string _base = "/api/Author";
            
            public static string POST()
                => $"{_base}";
            
            public static string GET_All()
                => $"{_base}";
            
            public static string PUT()
                => $"{_base}";
            
            public static string GET_ById(Guid id)
                => $"{_base}/{id}";
            public static string GET_ByName(string name)
                => $"{_base}/name/{name}";
        }
        
        public static class Tags
        {
            const string _base = "/api/Tags";
            
            public static string POST()
                => $"{_base}";
            
            public static string GET_All()
                => $"{_base}";
            
            public static string POST_UpdateTags()
                => $"{_base}";
            
            public static string DELETE_ById(Guid id)
                => $"{_base}/{id}";
            public static string GET_ById(Guid id)
                => $"{_base}/{id}";
            public static string GET_BooksById(Guid id)
                => $"{_base}/books/{id}";
            public static string GET_ByName(string name)
                => $"{_base}/name/{name}";
        }
        
        public static class Genres
        {
            const string _base = "/api/Genres";
            
            public static string POST()
                => $"{_base}";
            
            public static string GET_All()
                => $"{_base}";
            
            public static string POST_UpdateGenres()
                => $"{_base}";
            
            public static string DELETE_ById(Guid id)
                => $"{_base}/{id}";
            public static string GET_ById(Guid id)
                => $"{_base}/{id}";
            public static string GET_BooksById(Guid id)
                => $"{_base}/books/{id}";
            public static string GET_ByName(string name)
                => $"{_base}/name/{name}";
        }
        
        public static class BookList
        {
            private const string _base = "/api/booklist";
            
            public static string POST()
                => $"{_base}";

            public static string PUT()
                => $"{_base}";
            
            public static string GET_ByUserId(Guid id)
                => $"{_base}/user/{id}";

            public static string GET_ById(Guid id)
                => $"{_base}/booklist/{id}";

            public static string POST_AddBook()
                => $"{_base}/addbook";
        }
    }
}
