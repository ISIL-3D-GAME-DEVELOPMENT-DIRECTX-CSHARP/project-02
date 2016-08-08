#region License
/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace math
{
	public struct NVector3 : IEquatable<NVector3>
	{
		#region Private Fields

        private static NVector3 maxValue = new NVector3(float.MaxValue);
		private static NVector3 minValue = new NVector3(float.MinValue);
		private static NVector3 zero = new NVector3(0f, 0f, 0f);
		private static NVector3 one = new NVector3(1f, 1f, 1f);
		private static NVector3 unitX = new NVector3(1f, 0f, 0f);
		private static NVector3 unitY = new NVector3(0f, 1f, 0f);
		private static NVector3 unitZ = new NVector3(0f, 0f, 1f);
		private static NVector3 up = new NVector3(0f, 1f, 0f);
		private static NVector3 down = new NVector3(0f, -1f, 0f);
		private static NVector3 right = new NVector3(1f, 0f, 0f);
		private static NVector3 left = new NVector3(-1f, 0f, 0f);
		private static NVector3 forward = new NVector3(0f, 0f, -1f);
		private static NVector3 backward = new NVector3(0f, 0f, 1f);
		
		#endregion Private Fields
		
		
		#region Public Fields
		
		public float X;
		public float Y;
		public float Z;
		
		#endregion Public Fields
		
		
		#region Properties

        public static NVector3 MaxValue {
            get { return maxValue; }
        }

        public static NVector3 MinValue {
            get { return minValue; }
        }

		public static NVector3 Zero
		{
			get { return zero; }
		}
		
		public static NVector3 One
		{
			get { return one; }
		}
		
		public static NVector3 UnitX
		{
			get { return unitX; }
		}
		
		public static NVector3 UnitY
		{
			get { return unitY; }
		}
		
		public static NVector3 UnitZ
		{
			get { return unitZ; }
		}
		
		public static NVector3 Up
		{
			get { return up; }
		}
		
		public static NVector3 Down
		{
			get { return down; }
		}
		
		public static NVector3 Right
		{
			get { return right; }
		}
		
		public static NVector3 Left
		{
			get { return left; }
		}
		
		public static NVector3 Forward
		{
			get { return forward; }
		}
		
		public static NVector3 Backward
		{
			get { return backward; }
		}
		
		#endregion Properties
		
		
		#region Constructors
		
		public NVector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
		
		
		public NVector3(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
		}
		
		
		
		#endregion Constructors
		
		
		#region Public Methods
		
		public static NVector3 Add(NVector3 value1, NVector3 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}
		
		public static void Add(ref NVector3 value1, ref NVector3 value2, out NVector3 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}
		
		
		public static NVector3 Cross(NVector3 vector1, NVector3 NVector2)
		{
			Cross(ref vector1, ref NVector2, out vector1);
			return vector1;
		}
		
		public static void Cross(ref NVector3 vector1, ref NVector3 NVector2, out NVector3 result)
		{
			result = new NVector3(vector1.Y * NVector2.Z - NVector2.Y * vector1.Z,
			                     -(vector1.X * NVector2.Z - NVector2.X * vector1.Z),
			                     vector1.X * NVector2.Y - NVector2.X * vector1.Y);
		}
		
		public static float Distance(NVector3 vector1, NVector3 NVector2)
		{
			float result;
			DistanceSquared(ref vector1, ref NVector2, out result);
			return (float)Math.Sqrt(result);
		}
		
		public static void Distance(ref NVector3 value1, ref NVector3 value2, out float result)
		{
			DistanceSquared(ref value1, ref value2, out result);
			result = (float)Math.Sqrt(result);
		}
		
		public static float DistanceSquared(NVector3 value1, NVector3 value2)
		{
			float result;
			DistanceSquared(ref value1, ref value2, out result);
			return result;
		}
		
		public static void DistanceSquared(ref NVector3 value1, ref NVector3 value2, out float result)
		{
			result = (value1.X - value2.X) * (value1.X - value2.X) +
				(value1.Y - value2.Y) * (value1.Y - value2.Y) +
					(value1.Z - value2.Z) * (value1.Z - value2.Z);
		}
		
		public static NVector3 Divide(NVector3 value1, NVector3 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}
		
		public static NVector3 Divide(NVector3 value1, float value2)
		{
			float factor = 1 / value2;
			value1.X *= factor;
			value1.Y *= factor;
			value1.Z *= factor;
			return value1;
		}
		
		public static void Divide(ref NVector3 value1, float divisor, out NVector3 result)
		{
			float factor = 1 / divisor;
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
			result.Z = value1.Z * factor;
		}
		
		public static void Divide(ref NVector3 value1, ref NVector3 value2, out NVector3 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}
		
		public static float Dot(NVector3 vector1, NVector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}
		
		public static void Dot(ref NVector3 vector1, ref NVector3 vector2, out float result)
		{
			result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}
		
		public override bool Equals(object obj)
		{
			return (obj is NVector3) ? this == (NVector3)obj : false;
		}
		
		public bool Equals(NVector3 other)
		{
			return this == other;
		}
		
		public override int GetHashCode()
		{
			return (int)(this.X + this.Y + this.Z);
		}
		
		
		public float Length()
		{
			float result;
			DistanceSquared(ref this, ref zero, out result);
			return (float)Math.Sqrt(result);
		}
		
		public float LengthSquared()
		{
			float result;
			DistanceSquared(ref this, ref zero, out result);
			return result;
		}
		
		
		public static NVector3 Multiply(NVector3 value1, NVector3 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}
		
		public static NVector3 Multiply(NVector3 value1, float scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

        public static NVector3 ToVector3(ref NVector4 value) {
            NVector3 newVector = NVector3.Zero;
            newVector.X = value.X;
            newVector.Y = value.Y;
            newVector.Z = value.Z;
            return newVector;
        }
		
		public static void Multiply(ref NVector3 value1, float scaleFactor, out NVector3 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}
		
		public static void Multiply(ref NVector3 value1, ref NVector3 value2, out NVector3 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}
		
		public static NVector3 Negate(NVector3 value)
		{
			value = new NVector3(-value.X, -value.Y, -value.Z);
			return value;
		}
		
		public static void Negate(ref NVector3 value, out NVector3 result)
		{
			result = new NVector3(-value.X, -value.Y, -value.Z);
		}
		
		public void Normalize()
		{
			Normalize(ref this, out this);
		}
		
		public static NVector3 Normalize(NVector3 vector)
		{
			Normalize(ref vector, out vector);
			return vector;
		}
		
		public static void Normalize(ref NVector3 value, out NVector3 result)
		{
			float factor;
			Distance(ref value, ref zero, out factor);
			factor = 1f / factor;
			result.X = value.X * factor;
			result.Y = value.Y * factor;
			result.Z = value.Z * factor;
		}
		
		public static NVector3 Reflect(NVector3 vector, NVector3 normal)
		{
			// I is the original array
			// N is the normal of the incident plane
			// R = I - (2 * N * ( DotProduct[ I,N] ))
			NVector3 reflectedVector;
			// inline the dotProduct here instead of calling method
			float dotProduct = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
			reflectedVector.X = vector.X - (2.0f * normal.X) * dotProduct;
			reflectedVector.Y = vector.Y - (2.0f * normal.Y) * dotProduct;
			reflectedVector.Z = vector.Z - (2.0f * normal.Z) * dotProduct;
			
			return reflectedVector;
		}
		
		public static void Reflect(ref NVector3 vector, ref NVector3 normal, out NVector3 result)
		{
			// I is the original array
			// N is the normal of the incident plane
			// R = I - (2 * N * ( DotProduct[ I,N] ))
			
			// inline the dotProduct here instead of calling method
			float dotProduct = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
			result.X = vector.X - (2.0f * normal.X) * dotProduct;
			result.Y = vector.Y - (2.0f * normal.Y) * dotProduct;
			result.Z = vector.Z - (2.0f * normal.Z) * dotProduct;
			
		}
		
		
		public static NVector3 Subtract(NVector3 value1, NVector3 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}
		
		public static void Subtract(ref NVector3 value1, ref NVector3 value2, out NVector3 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}
		
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(32);
			sb.Append("{X:");
			sb.Append(this.X);
			sb.Append(" Y:");
			sb.Append(this.Y);
			sb.Append(" Z:");
			sb.Append(this.Z);
			sb.Append("}");
			return sb.ToString();
		}
		


		
		
		#endregion Public methods
		
		
		#region Operators
		
		public static bool operator ==(NVector3 value1, NVector3 value2)
		{
			return value1.X == value2.X
				&& value1.Y == value2.Y
					&& value1.Z == value2.Z;
		}
		
		public static bool operator !=(NVector3 value1, NVector3 value2)
		{
			return !(value1 == value2);
		}
		
		public static NVector3 operator +(NVector3 value1, NVector3 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}
		
		public static NVector3 operator -(NVector3 value)
		{
			value = new NVector3(-value.X, -value.Y, -value.Z);
			return value;
		}
		
		public static NVector3 operator -(NVector3 value1, NVector3 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}
		
		public static NVector3 operator *(NVector3 value1, NVector3 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}
		
		public static NVector3 operator *(NVector3 value, float scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			value.Z *= scaleFactor;
			return value;
		}
		
		public static NVector3 operator *(float scaleFactor, NVector3 value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			value.Z *= scaleFactor;
			return value;
		}
		
		public static NVector3 operator /(NVector3 value1, NVector3 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}
		
		public static NVector3 operator /(NVector3 value, float divider)
		{
			float factor = 1 / divider;
			value.X *= factor;
			value.Y *= factor;
			value.Z *= factor;
			return value;
		}
		
#endregion
	}
}
