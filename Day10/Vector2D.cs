using System;

public struct Vector2D : IEquatable<Vector2D>, IComparable<Vector2D>
{
    public int X { get; set; }
    public int Y { get; set; }

    public int realX {get; set;}
    public int realY {get; set;}

    public double Angle
    {
        get
        {
            // dit word wel iffy, HEEL iffy
            return Math.Atan2(X, Y) + Math.PI/2;
        }
    }

    public double Length {
        get {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y,2));
        }
    }

    public Vector2D(int x, int y)
    {
        // take into account a rotation clockwise for the angle after atan2:
        // Misschien moet dat roteren ergens anders gebeuren
        if ((x >= 0 && y <= 0) || (x <= 0 && y >= 0)) {
            X = x;
            Y = -y;
        } else if ((x >= 0 && y >= 0) || (x <= 0 && y <= 0)) {
            X = -x;
            Y = y;
        } else {
            X = 0;
            Y = 0;
        }

        this.realX = x;
        this.realY = y;
    }

    public bool Equals(Vector2D other)
    {
        return other.Angle == Angle;
    }

    public override int GetHashCode()
    {
        return Angle.GetHashCode();
    }

    public int CompareTo(Vector2D other)
    {
        return Angle.CompareTo(other.Angle);
    }
}