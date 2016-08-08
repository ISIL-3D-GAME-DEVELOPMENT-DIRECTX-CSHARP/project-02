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
using System.Text;

namespace math
{
	public struct NVector4 : IEquatable<NVector4>
	{
		#region Private Fields
		
		private static NVector4 zeroVector = new NVector4();
		private static NVector4 unitVector = new NVector4(1f, 1f, 1f, 1f);
		private static NVector4 unitXVector = new NVector4(1f, 0f, 0f, 0f);
		private static NVector4 unitYVector = new NVector4(0f, 1f, 0f, 0f);
		private static NVector4 unitZVector = new NVector4(0f, 0f, 1f, 0f);
		private static NVector4 unitWVector = new NVector4(0f, 0f, 0f, 1f);
		
		#endregion Private Fields
		
		
		#region Public Fields
		
		public float X;
		public float Y;
		public float Z;
		public float W;
		
		#endregion Public Fields
		
		
		#region Properties
		
		public static NVector4 Zero
		{
			get { return zeroVector; }
		}
		
		public static NVector4 One
		{
			get { return unitVector; }
		}
		
		public static NVector4 UnitX
		{
			get { return unitXVector; }
		}
		
		public static NVector4 UnitY
		{
			get { return unitYVector; }
		}
		
		public static NVector4 UnitZ
		{
			get { return unitZVector; }
		}
		
		public static NVector4 UnitW
		{
			get { return unitWVector; }
		}
		
		#endregion Properties
		
		
		#region Constructors
		
		public NVector4(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}
		
		
		public NVector4(NVector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}
		
		public NVector4(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
			this.W = value;
		}
		
#endregion
		
		
		#region Public Methods
		
		public static NVector4 Add(NVector4 value1, NVector4 value2)
		{
			value1.W += value2.W;
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}
		
		public static void Add(ref NVector4 value1, ref NVector4 value2, out NVector4 result)
		{
			result.W = value1.W + value2.W;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}
		
		
		public static float Distance(NVector4 value1, NVector4 value2)
		{
			return (float)Math.Sqrt(DistanceSquared(value1, value2));
		}
		
		public static void Distance(ref NVector4 value1, ref NVector4 value2, out float result)
		{
			result = (float)Math.Sqrt(DistanceSquared(value1, value2));
		}
		
		public static float DistanceSquared(NVector4 value1, NVector4 value2)
		{
			float result;
			DistanceSquared(ref value1, ref value2, out result);
			return result;
		}
		
		public static void DistanceSquared(ref NVector4 value1, ref NVector4 value2, out float result)
		{
			result = (value1.W - value2.W) * (value1.W - value2.W) +
				(value1.X - value2.X) * (value1.X - value2.X) +
					(value1.Y - value2.Y) * (value1.Y - value2.Y) +
					(value1.Z - value2.Z) * (value1.Z - value2.Z);
		}
		
		public static NVector4 Divide(NVector4 value1, NVector4 value2)
		{
			value1.W /= value2.W;
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}
		
		public static NVector4 Divide(NVector4 value1, float divider)
		{
			float factor = 1f / divider;
			value1.W *= factor;
			value1.X *= factor;
			value1.Y *= factor;
			value1.Z *= factor;
			return value1;
		}
		
		public static void Divide(ref NVector4 value1, float divider, out NVector4 result)
		{
			float factor = 1f / divider;
			result.W = value1.W * factor;
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
			result.Z = value1.Z * factor;
		}
		
		public static void Divide(ref NVector4 value1, ref NVector4 value2, out NVector4 result)
		{
			result.W = value1.W / value2.W;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}
		
		public static float Dot(NVector4 vector1, NVector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}
		
		public static void Dot(ref NVector4 vector1, ref NVector4 vector2, out float result)
		{
			result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}
		
		public override bool Equals(object obj)
		{
			return (obj is NVector4) ? this == (NVector4)obj : false;
		}
		
		public bool Equals(NVector4 other)
		{
			return this.W == other.W
				&& this.X == other.X
					&& this.Y == other.Y
					&& this.Z == other.Z;
		}
		
		public override int GetHashCode()
		{
			return (int)(this.W + this.X + this.Y + this.Y);
		}
		

		
		public float Length()
		{
			float result;
			DistanceSquared(ref this, ref zeroVector, out result);
			return (float)Math.Sqrt(result);
		}
		
		public float LengthSquared()
		{
			float result;
			DistanceSquared(ref this, ref zeroVector, out result);
			return result;
		}
		
		
		
		public static NVector4 Multiply(NVector4 value1, NVector4 value2)
		{
			value1.W *= value2.W;
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}
		
		public static NVector4 Multiply(NVector4 value1, float scaleFactor)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}
		
		public static void Multiply(ref NVector4 value1, float scaleFactor, out NVector4 result)
		{
			result.W = value1.W * scaleFactor;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}
		
		public static void Multiply(ref NVector4 value1, ref NVector4 value2, out NVector4 result)
		{
			result.W = value1.W * value2.W;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}
		
		public static NVector4 Negate(NVector4 value)
		{
			value = new NVector4(-value.X, -value.Y, -value.Z, -value.W);
			return value;
		}
		
		public static void Negate(ref NVector4 value, out NVector4 result)
		{
			result = new NVector4(-value.X, -value.Y, -value.Z,-value.W);
		}
		
		public void Normalize()
		{
			Normalize(ref this, out this);
		}
		
		public static NVector4 Normalize(NVector4 vector)
		{
			Normalize(ref vector, out vector);
			return vector;
		}
		
		public static void Normalize(ref NVector4 vector, out NVector4 result)
		{
			float factor;
			DistanceSquared(ref vector, ref zeroVector, out factor);
			factor = 1f / (float)Math.Sqrt(factor);
			
			result.W = vector.W * factor;
			result.X = vector.X * factor;
			result.Y = vector.Y * factor;
			result.Z = vector.Z * factor;
		}
		
		
		
		public static NVector4 Subtract(NVector4 value1, NVector4 value2)
		{
			value1.W -= value2.W;
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}
		
		public static void Subtract(ref NVector4 value1, ref NVector4 value2, out NVector4 result)
		{
			result.W = value1.W - value2.W;
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
			sb.Append(" W:");
			sb.Append(this.W);
			sb.Append("}");
			return sb.ToString();
		}
		
		#endregion Public Methods
		
		
		#region Operators
		
		public static NVector4 operator -(NVector4 value)
		{
			return new NVector4(-value.X, -value.Y, -value.Z, -value.W);
		}
		
		public static bool operator ==(NVector4 value1, NVector4 value2)
		{
			return value1.W == value2.W
				&& value1.X == value2.X
					&& value1.Y == value2.Y
					&& value1.Z == value2.Z;
		}
		
		public static bool operator !=(NVector4 value1, NVector4 value2)
		{
			return !(value1 == value2);
		}
		
		public static NVector4 operator +(NVector4 value1, NVector4 value2)
		{
			value1.W += value2.W;
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}
		
		public static NVector4 operator -(NVector4 value1, NVector4 value2)
		{
			value1.W -= value2.W;
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}
		
		public static NVector4 operator *(NVector4 value1, NVector4 value2)
		{
			value1.W *= value2.W;
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}
		
		public static NVector4 operator *(NVector4 value1, float scaleFactor)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}
		
		public static NVector4 operator *(float scaleFactor, NVector4 value1)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}
		
		public static NVector4 operator /(NVector4 value1, NVector4 value2)
		{
			value1.W /= value2.W;
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}
		
		public static NVector4 operator /(NVector4 value1, float divider)
		{
			float factor = 1f / divider;
			value1.W *= factor;
			value1.X *= factor;
			value1.Y *= factor;
			value1.Z *= factor;
			return value1;
		}
		
		#endregion Operators
	}
}