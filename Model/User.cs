namespace CisternasGAMC.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public short PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate {  get; set; }
        public string Status { get; set; }
    }
}
