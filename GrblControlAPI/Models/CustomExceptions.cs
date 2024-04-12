namespace GrblControlAPI.Models;

public class GrblCncNotConnectedException : Exception
{
    public GrblCncNotConnectedException() : base("Grbl Cnc not connected.") { }
}
public class SerialPortAlredyOpenException : Exception
{
    public SerialPortAlredyOpenException() : base("Serial port is already open.") { }
}
public class SerialPortFailException : Exception
{
    public SerialPortFailException() : base("Serial port fail") { }
}
public class StatusParseException : Exception
{
    public StatusParseException(string message) : base("Cant parse status from string: "+message) { }
}