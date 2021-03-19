namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        //public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Status { get; set; }
        
        /*public override string ToString()
        {
            return "ID = " + Id +
                   " / FirstName = " + FirstName +
                   " / LastName = " + LastName +
                   " / EMail = " + EMail;
        }*/
    }
}