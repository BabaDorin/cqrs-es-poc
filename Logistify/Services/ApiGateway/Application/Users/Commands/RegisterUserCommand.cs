namespace Application.Users.Commands
{
    public class RegisterUserCommand
    {
        public RegisterUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}
