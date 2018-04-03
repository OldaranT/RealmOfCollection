﻿using RealmOfCollection.util;
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

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            if (double.IsNaN(x))
            {
                Console.WriteLine();
            }
            X = x;
            Y = y;
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

        public Vector2D Normalize()
        {
            double length = Length();
            if(length == 0)
            {
                length = 1;
            }
            this.X = X / length;
            this.Y = Y / length;
            return this;
        }

        public Vector2D truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
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

        public Vector2D scaleBy(float scale)
        {
            this.X *= scale;
            this.Y *= scale;
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

        public static Vector2D operator /(Vector2D v, float value)
        {
            if(value == 0)
            {
                throw new Exception("do not devide by 0!");
            }
            Vector2D result = new Vector2D(v.X / value, v.Y / value);
            return result;
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

        public static Vector2D truncate(Vector2D v, float Max)
        {
            Vector2D truncated = v;
            if (v.Length() > Max)
            {

                truncated.Normalize();

                truncated *= Max;
            }
            return truncated;
        }

    }


}
