namespace DealershipProject.Web.Services
{
    // per-circuit scoped state for Blazor Server (not persisted across reconnects)
    public class UserState
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
    }
}