using System.Globalization;
using System.Linq.Expressions;

namespace GrblControlAPI.Models;

public static class StaticCnc
{
    private static SerialComPort SerialPort { get; set; }
    public static Status? CurrentStatus { get; set; }
    public static int Speed { get; private set; } = 458;
    public static bool IsConnected { get; private set; } = false;
    private static ManualResetEvent _statusReadEvent { get; set; }
    public static void Connect(SerialPortSettings settings)
    {
        if(SerialPort is null && IsConnected == false) 
        {
            if(CurrentStatus is not null) { CurrentStatus = null; }
            SerialPort = new SerialComPort();
            SerialPort.Open(settings.SerialPortName, settings.BaudRate, settings.DataBits, settings.Parity, settings.StopBits);
            SerialPort.RegisterReceiveCallback(_reciveDataEventHandler);
            _statusReadEvent = new ManualResetEvent(false);
            IsConnected = true;
            GetStatus();
        }
        else if(SerialPort is null && IsConnected == true)
        {
            IsConnected = false;
            throw new SerialPortFailException();
        }
        else
        {
            throw new SerialPortAlredyOpenException();
        }
    }
    public static void Disconnect()
    {
        if (SerialPort is not null)
        {
            SerialPort.Close();
        }
        IsConnected = false;
    }
    private static void _reciveDataEventHandler(string data)
    {
        if (data.Contains("ok\r\n") || data == "\r\n")
        {
            return;
        }
        else if (data.Contains("MPos:"))
        {
            Status status;
            if(Status.TryParseStatus(out status, data))
                CurrentStatus = status;
            else
                throw new StatusParseException(data);
            _statusReadEvent.Set();
        }
        else if(data.Contains("[MSG:'$H'|'$X' to unlock]\r\n"))
        {
            Status status;
            if (CurrentStatus is null)
                status = new(CncStsatuses.Alarm, new());
            else
                status = new(CncStsatuses.Alarm, CurrentStatus.Position);
            CurrentStatus = status;
        }
    }
    public static void GetStatus()
    {
        SerialPort.SendLine("?");
        _statusReadEvent.WaitOne();
        _statusReadEvent.Reset();
    }
    public static void MoveToAbsPoint(Point3f point)
    {
        SerialPort.SendLine($"$J=G90X{point.X.ToString("F3", CultureInfo.InvariantCulture)}" +
            $"Y{point.Y.ToString("F3", CultureInfo.InvariantCulture)}" +
            $"Z{point.Z.ToString("F3", CultureInfo.InvariantCulture)}" +
            $"F{Speed}");
    }
    public static void MoveToAddPoint(Point3f point)
    {
        SerialPort.SendLine($"$J=G91X{point.X.ToString("F3", CultureInfo.InvariantCulture)}" +
            $"Y{point.Y.ToString("F3", CultureInfo.InvariantCulture)}" +
            $"Z{point.Z.ToString("F3", CultureInfo.InvariantCulture)}" +
            $"F{Speed}");
    }
    public static void SetCncPosition(Point3f point)
    {
        SerialPort.SendLine($"G92 X{point.X.ToString("F3", CultureInfo.InvariantCulture)}" +
            $" Y{point.Y.ToString("F3", CultureInfo.InvariantCulture)}" +
            $" Z{point.Z.ToString("F3", CultureInfo.InvariantCulture)}");
        { //ответ на запрос статуса МОЖЕТ содержать WCO поле, пока так... 
            CurrentStatus.WorkCoordinatesOffset = CurrentStatus.Position; 
            CurrentStatus.Position = point;
        }
    }
    public static void SetCncSpeed(int speed)
    {
        if (speed >= 0)
            Speed = speed;
    }
    public static void SetCncSetting(GrblSettingName setting, string value)
    {
        SerialPort.SendLine($"${setting}={value}");
    }
}
