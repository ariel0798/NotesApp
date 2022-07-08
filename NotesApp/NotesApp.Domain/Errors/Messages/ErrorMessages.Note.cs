namespace NotesApp.Domain.Errors.Messages;

public static partial class ErrorMessages
{
    public static class Note
    {
        public const string NoteNotFound = "The note was not found"; 
        public const string EmptyTitle = "'Title' must not be empty."; 
        public const string NotNullTitle = "'Title' can not be null.";
        public const string EmptyDescription = "'Description' must not be empty."; 
        public const string NotNullDescription = "'Description' can not be null.";
        
    }
}