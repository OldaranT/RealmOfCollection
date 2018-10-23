using RealmOfCollection.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection
{

    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0, 0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D Zero()
        {
            this.X = 0;
            this.Y = 0;

            return this;
        }

        public bool isZero()
        {
            if (X == 0 && Y == 0)
                return true;
            else
                return false;
        }

        public double Length()
        {
            double length = Math.Sqrt(X * X + Y * Y);
            return length;
        }

        public double LengthSquared()
        {
            return Length() * Length();
        }

        public Vector2D Add(Vector2D v)
        {
            this.X += v.X;
            this.Y += v.Y;
            return this;
        }

        public Vector2D Sub(Vector2D v)
        {
            this.X -= v.X;
            this.Y -= v.Y;
            return this;
        }

        public Vector2D Multiply(double value)
        {
            this.X *= value;
            this.Y *= value;
            return this;
        }

        public Vector2D Multiply(float value)
        {
            this.X *= value;
            this.Y *= value;
            return this;
        }

        public Vector2D divide(double value)
        {
            if(value == 0)
            {
                throw new Exception("do not devide by 0!");
            }
            this.X /= value;
            this.Y /= value;
            return this;
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            Vector2D result = new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
            return result;
        }

        public static Vector2D operator *(Vector2D v, float value)
        {
            Vector2D result = new Vector2D(v.X * value, v.Y * value);
            return result;
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            Vector2D result = new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
            return result;
        }

        public static Vector2D operator +(Vector2D v1, float v2)
        {
            Vector2D result = new Vector2D(v1.X + v2, v1.Y + v2);
            return result;
        }

        public static Vector2D operator +(float v2, Vector2D v1)
        {
            Vector2D result = new Vector2D(v1.X + v2, v1.Y + v2);
            return result;
        }

        public static Vector2D operator /(Vector2D v, float value)
        {
            if (value == 0)
            {
                throw new Exception("do not devide by 0!");
            }
            Vector2D result = new Vector2D(v.X / value, v.Y / value);
            return result;
        }

        public static Vector2D operator*(Vector2D lhs, double rhs)
        {
            Vector2D result = lhs;
            result.Multiply(rhs);
            return result;
        }

        public static  Vector2D operator *(double lhs, Vector2D rhs)
        {
            Vector2D result = rhs;
            result.Multiply(lhs);
            return result;
        }

        public Vector2D Normalize()
        {
            double length = Length();
            if(length == 0)
            {
                return this;
            }
            this.X = X / length;
            this.Y = Y / length;
            return this;
        }

        public static Vector2D Vec2DNormalize(Vector2D v)
        {
            Vector2D vec = v;

            double vector_length = vec.Length();
            if(vector_length == 0)
            {
                return vec;
            }
            vec.X /= vector_length;
            vec.Y /= vector_length;

            return vec;
        }

        public double Distance(Vector2D toCheck)
        {
            double xSeparation = toCheck.X - X;
            double ySeparation = toCheck.Y - Y;

            return Math.Sqrt(ySeparation * ySeparation + xSeparation * xSeparation);
        }

        public float DistanceSqrt(Vector2D toCheck)
        {
            double xSeparation = toCheck.X - X;
            double ySeparation = toCheck.Y - Y;

            return (float)(ySeparation * ySeparation + xSeparation * xSeparation);
        }

        public static double Vec2DDistanceSq(Vector2D v1, Vector2D v2)
        {
            double ySeparation = v2.Y - v1.Y;
            double xSeparation = v2.X - v1.X;

            return ySeparation * ySeparation + xSeparation * xSeparation;
        }

        public Vector2D Truncate(double max)
        {
            
            //double i;
            //i = max / this.Length();
            //i = i < 1.0 ? i : 1.0;
            //this.ScaleBy((float)i);
            if (Length() > max)
            {
                Normalize();
                Multiply(max);
            }
            return this;
        }

        public Vector2D Perp()
        {
            return new Vector2D(-this.Y, this.X);
        }

        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }


        public Vector2D ScaleBy(float scale)
        {
            this.X *= scale;
            this.Y *= scale;
            return this;
        }

        
        
        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }

        public static Vector2D PointToWorldSpace(Vector2D point, Vector2D AgentHeading, Vector2D AgentSide, Vector2D AgentPosition)
        {
            //make a copy of the point
            Vector2D TransPoint = point;

            //create a transformation matrix
            C2DMatrix matTransform = new C2DMatrix();

            //rotate
            matTransform.Rotate(AgentHeading, AgentSide);

            //and translate
            matTransform.Translate(AgentPosition.X, AgentPosition.Y);

            //now transform the vertices
            matTransform.TransformVector2Ds(TransPoint);

            return TransPoint;
        }

        public static void WrapAround(Vector2D pos, int MaxX, int MaxY)
        {
            if (pos.X > MaxX)
            {
                pos.X = 0.0;
            }

            if (pos.X < 0)
            {
                pos.X = (double)MaxX;
            }

            if (pos.Y < 0)
            {
                pos.Y = (double)MaxY;
            }

            if (pos.Y > MaxY)
            {
                pos.Y = 0.0;
            }

        }

        public static Vector2D rotate(Vector2D v, float degrees)
        {
            return new Vector2D(
            (float)(v.X * Math.Cos(degrees) - v.Y * Math.Sin(degrees)),
            (float)(v.X * Math.Sin(degrees) + v.Y * Math.Cos(degrees))
        );
        }
        
        public static Vector2D truncate(Vector2D v, float Max)
        {
            Vector2D truncated = v;
            //float i;
            //i = Max / (float)truncated.Length();
            //i = i < 1.0 ? i : 1.0f;
            //truncated = truncated.ScaleBy(i);
            if (v.Length() > Max)
            {

                truncated.Normalize();

                truncated *= Max;
            }
            return truncated;
        }

        public static bool InsideRegion(Vector2D p, Vector2D top_left, Vector2D bot_rgt)
        {
            return !((p.X < top_left.X) || (p.X > bot_rgt.X) ||
                   (p.Y < top_left.Y) || (p.Y > bot_rgt.Y));
        }

        public static bool InsideRegion(Vector2D p, int left, int top, int right, int bottom)
        {
            return !((p.X < left) || (p.X > right) || (p.Y < top) || (p.Y > bottom));
        }

        public bool Equals(Vector2D other)
        {
            if(other == null)
            {
                return false;
            }
            return X == other.X && Y == other.Y;
        }

    }


}
