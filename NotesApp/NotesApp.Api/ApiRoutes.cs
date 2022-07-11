namespace NotesApp.Api;

public static class ApiRoutes
{
    public static class Authentication
    {
        public const string Login = "login";
        public const string Registration = "registration";
    }
    public static class Notes
    {
        public const string NoteDetailId = "{noteDetailId}";
        public const string Trash = "trash";
        public const string Recover = "{noteDetailId}/recover";
        public const string SoftDeleteByNoteDetailId = "{noteDetailId}/soft-delete";

    }
}