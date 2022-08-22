namespace NotesApp.Domain.Errors.Messages;

public static partial class ErrorMessages
{
    public static class User
    {
        public const string DuplicatedEmail = "Email already exists"; 
        public const string InvalidEmailFormat = "Invalid email format"; 
        public const string EmptyName = "'Name' must not be empty."; 
        public const string NotNullName = "'Name' can not be null.";
        public const string EmptyLastName = "'Last name' must not be empty."; 
        public const string NotNullLastName = "'Last name' can not be null."; 
        public const string EmptyPassword = "'Password' must not be empty."; 
        public const string NotNullPassword = "'Password' must not be empty.";
        public const string EmptyEmail= "'Email' must not be empty."; 
        public const string NotNullEmail = "'Email' must not be empty."; 
    }
}