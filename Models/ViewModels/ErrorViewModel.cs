namespace Mvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string?  Message { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // vai  retornar se n�o � nulo ou vazio
    }
}
