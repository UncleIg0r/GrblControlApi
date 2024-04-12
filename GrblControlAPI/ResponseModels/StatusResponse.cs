using GrblControlAPI.Models;
namespace GrblControlAPI.Responses;

public class StatusResponse
{
    public string ComPortName { get; set; }
    public Status? CncStatus { get; set; }
    public int CncSpeed { get; set; }
    public bool CncConnected { get; set; }
    public string Message { get; set; } = string.Empty;
    public StatusResponse() { }
    public StatusResponse(Status status, int speed, bool isConnected)
    {
        CncStatus = status;
        CncSpeed = speed;
        CncConnected = isConnected;
    }
    public StatusResponse(Status status, int speed, bool isConnected, string message)
    {
        CncStatus = status;
        CncSpeed = speed;
        CncConnected = isConnected;
        Message = message;
    }
    
}
