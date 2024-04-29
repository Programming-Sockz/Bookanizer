namespace Bookanizer.Server.Model
{
    //Das hier ist das Model was wir abspeichern. für verschiedene Sachen im Frontend wird es aufgeteilt um keine sensitive Daten an User zu leaken.
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; } = true;
    }
}
