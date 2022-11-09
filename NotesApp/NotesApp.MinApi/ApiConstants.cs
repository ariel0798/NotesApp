namespace NotesApp.MinApi;

public static class ApiConstants
{
    public const string ContentType = "application/json";
    public static class Authentication
    {
        public const string Tag = "Auth";
        public const string BaseRoute = "auth";
        
        public static class EndpointNames
        {
            public const string Login = $"{BaseRoute}/login";
            public const string Register = $"{BaseRoute}/register";
            public const string RefreshToken = $"{BaseRoute}/refresh-token";
        }
    }
    public static class Notes
    {
        public const string Tag = "Notes";
        public const string BaseRoute = "notes";

        public static class EndpointNames
        {
            public const string NoteDetailId = $"{BaseRoute}/{{noteDetailId}}";
            public const string Trash = $"{BaseRoute}/trash";
            public const string Recover = $"{BaseRoute}/{{noteDetailId}}/recover";
            public const string SoftDeleteByNoteDetailId = $"{BaseRoute}/{{noteDetailId}}/soft-delete";
            public const string RecurringJobInitializer = "/recurringJob-initializer";
        }
    }
}