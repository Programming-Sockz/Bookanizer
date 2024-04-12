namespace Bookanizer.Shared.Libraries
{
    //hier stehen alle API Routen sortiert drinnen die wir benutzen. Das wird nicht gebracuht aber sammelt alle Routen einfach für den Überblick
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
        }
    }
}
