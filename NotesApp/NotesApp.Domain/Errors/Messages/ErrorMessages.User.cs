namespace NotesApp.Domain.Errors.Messages;

public static partial class ErrorMessages
{
    public static class User
    {
        public const string DuplicatedEmail = "Email is already exist"; 
        public const string InvalidEmailFormat = "Invalid email format"; 
        public const string EmptyName = "'Name' must not be empty."; 
    }
}