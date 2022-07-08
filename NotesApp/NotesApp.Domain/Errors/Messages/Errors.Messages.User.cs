namespace NotesApp.Domain.Errors.Messages;

public static partial class Errors
{
    public static partial class Messages
    {
        public static class User
        {
            public const string DuplicatedEmail = "Email is already exist"; 
            public const string InvalidEmailFormat = "Invalid email format"; 
            public const string EmptyName = "'Name' must not be empty."; 
        }
    }
}