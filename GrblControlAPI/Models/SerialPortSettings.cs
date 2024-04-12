namespace GrblControlAPI.Models;

public class SerialPortSettings
{
    public string SerialPortName { get; set; }
    public string BaudRate { get; set; }
    public string DataBits { get; set; }
    public string Parity { get; set; }
    public string StopBits { get; set; }
}
