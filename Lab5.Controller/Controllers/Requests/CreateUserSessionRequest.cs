namespace Lab5.Controller.Controllers.Requests;

public class CreateUserSessionRequest
{
    public string AccountNumber { get; set; } = string.Empty;

    public string PinCode { get; set; } = string.Empty;
}