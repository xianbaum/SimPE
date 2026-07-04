using System;
using SNMatrix = System.Numerics.Matrix4x4;
using SNVector3 = System.Numerics.Vector3;

namespace Microsoft.DirectX
{
	public struct Vector2
	{
		public float X;
		public float Y;

		public Vector2(float valueX, float valueY)
		{
			X = valueX;
			Y = valueY;
		}
	}

	public struct Vector3
	{
		public float X;
		public float Y;
		public float Z;

		public Vector3(float valueX, float valueY, float valueZ)
		{
			X = valueX;
			Y = valueY;
			Z = valueZ;
		}

		public float Length()
		{
			return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		public void Normalize()
		{
			float len = Length();
			if (len != 0f)
			{
				X /= len;
				Y /= len;
				Z /= len;
			}
		}

		// Instance, in-place: transform this point by the matrix and homogeneous-divide.
		public void TransformCoordinate(Matrix sourceMatrix)
		{
			float x = X * sourceMatrix.M11 + Y * sourceMatrix.M21 + Z * sourceMatrix.M31 + sourceMatrix.M41;
			float y = X * sourceMatrix.M12 + Y * sourceMatrix.M22 + Z * sourceMatrix.M32 + sourceMatrix.M42;
			float z = X * sourceMatrix.M13 + Y * sourceMatrix.M23 + Z * sourceMatrix.M33 + sourceMatrix.M43;
			float w = X * sourceMatrix.M14 + Y * sourceMatrix.M24 + Z * sourceMatrix.M34 + sourceMatrix.M44;
			if (w != 0f)
			{
				x /= w;
				y /= w;
				z /= w;
			}
			X = x;
			Y = y;
			Z = z;
		}

		public static float Dot(Vector3 left, Vector3 right)
		{
			return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
		}

		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				lhs.Y * rhs.Z - lhs.Z * rhs.Y,
				lhs.Z * rhs.X - lhs.X * rhs.Z,
				lhs.X * rhs.Y - lhs.Y * rhs.X);
		}

		public static Vector3 Normalize(Vector3 source)
		{
			Vector3 v = source;
			v.Normalize();
			return v;
		}

		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		public static Vector3 operator -(Vector3 vec)
		{
			return new Vector3(0f - vec.X, 0f - vec.Y, 0f - vec.Z);
		}

		public static Vector3 operator *(float right, Vector3 left)
		{
			return new Vector3(left.X * right, left.Y * right, left.Z * right);
		}

		public static Vector3 operator *(Vector3 left, float right)
		{
			return new Vector3(left.X * right, left.Y * right, left.Z * right);
		}

		public SNVector3 ToNumerics()
		{
			return new SNVector3(X, Y, Z);
		}
	}

	public struct Vector4
	{
		public float X;
		public float Y;
		public float Z;
		public float W;

		public Vector4(float valueX, float valueY, float valueZ, float valueW)
		{
			X = valueX;
			Y = valueY;
			Z = valueZ;
			W = valueW;
		}
	}

	public struct Quaternion
	{
		public float X;
		public float Y;
		public float Z;
		public float W;

		public Quaternion(float valueX, float valueY, float valueZ, float valueW)
		{
			X = valueX;
			Y = valueY;
			Z = valueZ;
			W = valueW;
		}
	}

	// Row-major 4x4 matrix, row-vector convention (identical layout to System.Numerics.Matrix4x4).
	public struct Matrix
	{
		public float M11;
		public float M12;
		public float M13;
		public float M14;
		public float M21;
		public float M22;
		public float M23;
		public float M24;
		public float M31;
		public float M32;
		public float M33;
		public float M34;
		public float M41;
		public float M42;
		public float M43;
		public float M44;

		public static Matrix Identity
		{
			get
			{
				Matrix m = default(Matrix);
				m.M11 = 1f;
				m.M22 = 1f;
				m.M33 = 1f;
				m.M44 = 1f;
				return m;
			}
		}

		public void Invert()
		{
			if (SNMatrix.Invert(ToNumerics(), out var inv))
			{
				FromNumerics(inv);
			}
		}

		public static Matrix Multiply(Matrix left, Matrix right)
		{
			Matrix r = default(Matrix);
			r.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41;
			r.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42;
			r.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43;
			r.M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44;
			r.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41;
			r.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42;
			r.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43;
			r.M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44;
			r.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41;
			r.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42;
			r.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43;
			r.M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44;
			r.M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41;
			r.M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42;
			r.M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43;
			r.M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44;
			return r;
		}

		public static Matrix operator *(Matrix left, Matrix right)
		{
			return Multiply(left, right);
		}

		// Instance, in-place: this = this * source.
		public void Multiply(Matrix source)
		{
			this = Multiply(this, source);
		}

		public static Matrix Translation(Vector3 v)
		{
			return Translation(v.X, v.Y, v.Z);
		}

		public static Matrix Translation(float x, float y, float z)
		{
			Matrix m = Identity;
			m.M41 = x;
			m.M42 = y;
			m.M43 = z;
			return m;
		}

		public static Matrix Scaling(Vector3 v)
		{
			return Scaling(v.X, v.Y, v.Z);
		}

		public static Matrix Scaling(float x, float y, float z)
		{
			Matrix m = default(Matrix);
			m.M11 = x;
			m.M22 = y;
			m.M33 = z;
			m.M44 = 1f;
			return m;
		}

		public static Matrix RotationX(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			Matrix m = Identity;
			m.M22 = c;
			m.M23 = s;
			m.M32 = 0f - s;
			m.M33 = c;
			return m;
		}

		public static Matrix RotationY(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			Matrix m = Identity;
			m.M11 = c;
			m.M13 = 0f - s;
			m.M31 = s;
			m.M33 = c;
			return m;
		}

		public static Matrix RotationZ(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			Matrix m = Identity;
			m.M11 = c;
			m.M12 = s;
			m.M21 = 0f - s;
			m.M22 = c;
			return m;
		}

		public static Matrix RotationQuaternion(Quaternion quat)
		{
			float len = (float)Math.Sqrt(quat.X * quat.X + quat.Y * quat.Y + quat.Z * quat.Z + quat.W * quat.W);
			float x = quat.X, y = quat.Y, z = quat.Z, w = quat.W;
			if (len != 0f)
			{
				x /= len;
				y /= len;
				z /= len;
				w /= len;
			}
			Matrix m = Identity;
			m.M11 = 1f - 2f * (y * y + z * z);
			m.M12 = 2f * (x * y + z * w);
			m.M13 = 2f * (x * z - y * w);
			m.M21 = 2f * (x * y - z * w);
			m.M22 = 1f - 2f * (x * x + z * z);
			m.M23 = 2f * (y * z + x * w);
			m.M31 = 2f * (x * z + y * w);
			m.M32 = 2f * (y * z - x * w);
			m.M33 = 1f - 2f * (x * x + y * y);
			return m;
		}

		public static Matrix PerspectiveFovLH(float fieldOfViewY, float aspectRatio, float znearPlane, float zfarPlane)
		{
			float yScale = 1f / (float)Math.Tan(fieldOfViewY / 2f);
			float xScale = yScale / aspectRatio;
			Matrix m = default(Matrix);
			m.M11 = xScale;
			m.M22 = yScale;
			m.M33 = zfarPlane / (zfarPlane - znearPlane);
			m.M34 = 1f;
			m.M43 = 0f - znearPlane * zfarPlane / (zfarPlane - znearPlane);
			return m;
		}

		public static Matrix PerspectiveFovRH(float fieldOfViewY, float aspectRatio, float znearPlane, float zfarPlane)
		{
			float yScale = 1f / (float)Math.Tan(fieldOfViewY / 2f);
			float xScale = yScale / aspectRatio;
			Matrix m = default(Matrix);
			m.M11 = xScale;
			m.M22 = yScale;
			m.M33 = zfarPlane / (znearPlane - zfarPlane);
			m.M34 = -1f;
			m.M43 = znearPlane * zfarPlane / (znearPlane - zfarPlane);
			return m;
		}

		public SNMatrix ToNumerics()
		{
			return new SNMatrix(
				M11, M12, M13, M14,
				M21, M22, M23, M24,
				M31, M32, M33, M34,
				M41, M42, M43, M44);
		}

		public void FromNumerics(SNMatrix m)
		{
			M11 = m.M11; M12 = m.M12; M13 = m.M13; M14 = m.M14;
			M21 = m.M21; M22 = m.M22; M23 = m.M23; M24 = m.M24;
			M31 = m.M31; M32 = m.M32; M33 = m.M33; M34 = m.M34;
			M41 = m.M41; M42 = m.M42; M43 = m.M43; M44 = m.M44;
		}
	}

	// Minimal matrix stack (Push/Pop/MultiplyMatrixLocal/LoadMatrix/Top).
	public sealed class MatrixStack : IDisposable
	{
		private readonly System.Collections.Generic.Stack<Matrix> stack = new System.Collections.Generic.Stack<Matrix>();

		public MatrixStack()
		{
			stack.Push(Matrix.Identity);
		}

		public Matrix Top => stack.Peek();

		public void LoadMatrix(Matrix m)
		{
			stack.Pop();
			stack.Push(m);
		}

		public void Push()
		{
			stack.Push(Top);
		}

		public void Pop()
		{
			stack.Pop();
		}

		public void MultiplyMatrixLocal(Matrix m)
		{
			Matrix top = stack.Pop();
			stack.Push(Matrix.Multiply(m, top));
		}

		public void Dispose()
		{
			stack.Clear();
		}
	}
}
