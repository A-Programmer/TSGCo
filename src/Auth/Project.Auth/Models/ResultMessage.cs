
namespace Project.Auth.Models
{
    public class ResultMessage
    {
        public ResultMessage(bool status, string cssClass, string message)
        {
            Status = status;
            CssClass = cssClass;
            Message = message;
        }

        public void Update(bool status, string cssClass, string message)
        {
            Status = status;
            CssClass = cssClass;
            Message = message;
        }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string CssClass { get; set; }
    }
}