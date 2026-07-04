using Ambertation.Geometry;
using Microsoft.DirectX;

namespace Ambertation.Scenes;

public class Converter
{
	public static Vector2 ToDx(Vector2 v)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2((float)v.X, (float)v.Y);
	}

	public static Vector3 ToDx(Vector3 v)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3((float)v.X, (float)v.Y, (float)v.Z);
	}

	public static Vector4 ToDx(Vector4 v)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return new Vector4((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
	}

	public static Matrix ToDx(Transformation t)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		return Matrix.Multiply(Matrix.Scaling(ToDx(t.Scaling)), Matrix.Multiply(Matrix.RotationX((float)t.Rotation.X), Matrix.Multiply(Matrix.RotationY((float)t.Rotation.Y), Matrix.Multiply(Matrix.RotationZ((float)t.Rotation.Z), Matrix.Translation(ToDx(t.Translation))))));
	}

	public static Matrix FromDx(Matrix m)
	{
		Matrix matrix = new Matrix(4, 4);
		matrix[0, 0] = m.M11;
		matrix[0, 1] = m.M21;
		matrix[0, 2] = m.M31;
		matrix[0, 3] = m.M41;
		matrix[1, 0] = m.M12;
		matrix[1, 1] = m.M22;
		matrix[1, 2] = m.M32;
		matrix[1, 3] = m.M42;
		matrix[2, 0] = m.M13;
		matrix[2, 1] = m.M23;
		matrix[2, 2] = m.M33;
		matrix[2, 3] = m.M43;
		matrix[3, 0] = m.M14;
		matrix[3, 1] = m.M24;
		matrix[3, 2] = m.M34;
		matrix[3, 3] = m.M44;
		return matrix;
	}

	public static Matrix ToDx(Matrix t)
	{
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		Matrix result = default(Matrix);
		((Matrix)(ref result))._002Ector();
		result.M11 = (float)t[0, 0];
		result.M21 = (float)t[0, 1];
		result.M31 = (float)t[0, 2];
		result.M41 = (float)t[0, 3];
		result.M12 = (float)t[1, 0];
		result.M22 = (float)t[1, 1];
		result.M32 = (float)t[1, 2];
		result.M42 = (float)t[1, 3];
		result.M13 = (float)t[2, 0];
		result.M23 = (float)t[2, 1];
		result.M33 = (float)t[2, 2];
		result.M43 = (float)t[2, 3];
		result.M14 = (float)t[3, 0];
		result.M24 = (float)t[3, 1];
		result.M34 = (float)t[3, 2];
		result.M44 = (float)t[3, 3];
		return result;
	}
}
