using GrblControlAPI.Responses;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GrblControlAPI.Models;

public class Status
{
    internal CncStsatuses Condition { get; set; }
    internal Point3f Position { get; set; }
    internal Point3f WorkCoordinatesOffset { get; set; } = new Point3f(0, 0, 0);
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="position"></param>
    public Status(CncStsatuses condition, Point3f position)
    {
        Condition = condition;
        Position = position;
    }
    public Status(CncStsatuses condition, Point3f position, Point3f wco)
    {
        Condition = condition;
        Position = position;
        WorkCoordinatesOffset = wco;
    }
    //строка текущего статуса выглядит примерно так:
    //<Idle|MPos:0.000,0.000,0.000|FS:0,0>
    //<Idle|MPos:0.000,0.000,0.000|FS:0,0|WCO:0.000,0.000,0.000>

    /// <summary>
    /// Метод извлечения статуса из строки типа:
    /// Idle|MPos:0.000,0.000,0.000|FS:0,00>
    /// </summary>
    /// <param name="message"></param>
    /// <returns>true если преобразование успешно. При провале result.Condition будет None</returns>
    public static bool TryParseStatus(out Status result, string message)
    {
        //разделение сообщения на строки
        if(message.EndsWith("\r\n"))
            message.Remove(message.IndexOf("\r\n"));

        result = new(CncStsatuses.None, new());

        if(message.Length < 28) return false; //грубая проверка целостноси строки

        message = message.Trim(new char[] { '<', '>' });

        string[] payload = message.Split('|');
        if (payload.Length < 2) return false; //грубая проверка целостноси строки 
        if (!payload[1].Contains("MPos:")) return false; //грубая проверка целостноси строки 

        //получение Condition
        result.Condition = (CncStsatuses)Enum.Parse(typeof(CncStsatuses), payload[0]);

        //пропуск "MPos:"
        payload[1] = payload[1].Substring(5);
        string[] xyz = payload[1].Split(",");
        //получение Position
        result.Position = new Point3f(float.Parse(xyz[0], CultureInfo.InvariantCulture),
            float.Parse(xyz[1], CultureInfo.InvariantCulture), float.Parse(xyz[2], CultureInfo.InvariantCulture));
        
        if(payload.Length > 3 && payload[3].Contains("WCO:")) 
        {
            payload[3] = payload[3].Substring(4);
            xyz = payload[3].Split(",");
            result.WorkCoordinatesOffset = new Point3f(float.Parse(xyz[0], CultureInfo.InvariantCulture),
                float.Parse(xyz[1], CultureInfo.InvariantCulture), float.Parse(xyz[2], CultureInfo.InvariantCulture));
        }

        return true;
    }
    public static bool operator ==(Status left, Status right) 
    { 
        return left.Equals(right); 
    }
    public static bool operator !=(Status left, Status right)
    {
        return !left.Equals(right);
    }

    public bool Equals(Status other)
    {
        return Condition.Equals(other.Condition)&&Position.Equals(other.Position);
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Condition.GetHashCode(), Position.GetHashCode());
    }
}
public enum CncStsatuses
{
    None,
    Idle,
    Run,
    Hold,
    Door,
    Home,
    Alarm,
    Check
}
