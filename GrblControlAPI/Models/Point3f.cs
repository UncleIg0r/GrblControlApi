//Класс подсмотрен в OpenCvSharp

namespace GrblControlAPI.Models;

public class Point3f
{
    public float X { get; set; } = default;
    public float Y { get; set; } = default;
    public float Z { get; set; } = default;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public Point3f(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public Point3f() { }

    /// <summary>
    /// Сравнение
    /// </summary>
    /// <param name="other"></param>
    /// <returns>true если точки равны</returns>
    public bool Equals(Point3f other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
    }
    public static bool operator ==(Point3f left, Point3f right)
    {
        return left.Equals(right);
    }
    public static bool operator !=(Point3f left, Point3f right)
    {
        return !left.Equals(right);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Point3f Negate() => new(-X, -Y, -Z);

    /// <summary>
    /// Сложение
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public Point3f Add(Point3f p) => new(X + p.X, Y + p.Y, Z + p.Z);
    public static Point3f operator +(Point3f p1, Point3f p2) => p1.Add(p2);
    /// <summary>
    /// Вычитание
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public Point3f Subtract(Point3f p) => new(X - p.X, Y - p.Y, Z - p.Z);
    public static Point3f operator -(Point3f p1, Point3f p2) => p1.Subtract(p2);

    public override string ToString()
    {
        return $"(x:{X} y:{Y} z:{Z})";
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        throw new NotImplementedException();
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}
