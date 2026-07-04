using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using V = Vortice.Direct3D9;

namespace Microsoft.DirectX.Direct3D
{
	public abstract class BaseMesh : IDisposable
	{
		protected Device device;
		protected Array vertexData;
		protected short[] indexData;
		protected int numberVertices;
		protected int numberFaces;
		protected VertexFormats fvf;
		protected bool disposed;

		public int NumberVertices => numberVertices;
		public int NumberFaces => numberFaces;
		public int NumberAttributes => 1;
		public bool Disposed => disposed;

		public VertexBuffer VertexBuffer => new VertexBuffer(fvf);

		public void SetVertexBufferData(object vertices, LockFlags flags)
		{
			vertexData = (Array)vertices;
			if (vertexData != null)
			{
				numberVertices = vertexData.Length;
			}
		}

		public void SetIndexBufferData(object data, LockFlags flags)
		{
			indexData = (short[])data;
			if (indexData != null)
			{
				numberFaces = indexData.Length / 3;
			}
		}

		// MDX returns the vertex array to read/fill; we hand back the stored blittable struct array.
		public Array LockVertexBuffer(Type typeVertexType, LockFlags flags, params int[] ranks)
		{
			if (vertexData != null && vertexData.GetType().GetElementType() == typeVertexType)
			{
				return vertexData;
			}
			int len = (ranks != null && ranks.Length > 0) ? ranks[0] : numberVertices;
			return Array.CreateInstance(typeVertexType, len);
		}

		public void UnlockVertexBuffer()
		{
		}

		// Optional in the fixed-function preview path — adjacency/normal/optimize passes are no-ops.
		// TODO(runtime): implement if a mesh ever arrives without normals and needs them computed.
		public void GenerateAdjacency(float epsilon, int[] adjacency)
		{
		}

		public void ComputeNormals(int[] adjacency)
		{
		}

		public void DrawSubset(int subset)
		{
			if (device == null || vertexData == null || indexData == null || numberFaces == 0)
			{
				return;
			}
			int stride = Marshal.SizeOf(vertexData.GetType().GetElementType());
			device.Native.VertexFormat = (V.VertexFormat)(int)fvf;
			GCHandle vh = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
			GCHandle ih = GCHandle.Alloc(indexData, GCHandleType.Pinned);
			try
			{
				device.Native.DrawIndexedPrimitiveUP(
					V.PrimitiveType.TriangleList,
					0u,
					(uint)numberVertices,
					(uint)numberFaces,
					ih.AddrOfPinnedObject(),
					V.Format.Index16,
					vh.AddrOfPinnedObject(),
					(uint)stride);
			}
			finally
			{
				vh.Free();
				ih.Free();
			}
		}

		public virtual void Dispose()
		{
			disposed = true;
			vertexData = null;
			indexData = null;
		}
	}

	public sealed class Mesh : BaseMesh
	{
		public Mesh(int numberFaces, int numberVertices, MeshFlags options, VertexFormats vertexFormat, Device device)
		{
			this.numberFaces = numberFaces;
			this.numberVertices = numberVertices;
			fvf = vertexFormat;
			this.device = device;
		}

		private Mesh(Device device, VertexFormats vertexFormat, CustomVertex.PositionNormal[] verts, short[] indices)
		{
			this.device = device;
			fvf = vertexFormat;
			vertexData = verts;
			indexData = indices;
			numberVertices = verts.Length;
			numberFaces = indices.Length / 3;
		}

		// Optimization is optional for a preview renderer.
		public void OptimizeInPlace(MeshFlags flags, int[] adjacency)
		{
		}

		private static Mesh FromGeometry(Device device, List<CustomVertex.PositionNormal> verts, List<short> indices)
		{
			return new Mesh(device, VertexFormats.PositionNormal, verts.ToArray(), indices.ToArray());
		}

		private static void AddQuad(List<CustomVertex.PositionNormal> v, List<short> idx, Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 n)
		{
			short baseIndex = (short)v.Count;
			v.Add(new CustomVertex.PositionNormal(a, n));
			v.Add(new CustomVertex.PositionNormal(b, n));
			v.Add(new CustomVertex.PositionNormal(c, n));
			v.Add(new CustomVertex.PositionNormal(d, n));
			idx.Add(baseIndex);
			idx.Add((short)(baseIndex + 1));
			idx.Add((short)(baseIndex + 2));
			idx.Add(baseIndex);
			idx.Add((short)(baseIndex + 2));
			idx.Add((short)(baseIndex + 3));
		}

		public static Mesh Box(Device device, float width, float height, float depth)
		{
			float x = width / 2f;
			float y = height / 2f;
			float z = depth / 2f;
			List<CustomVertex.PositionNormal> v = new List<CustomVertex.PositionNormal>();
			List<short> idx = new List<short>();
			// +X, -X
			AddQuad(v, idx, new Vector3(x, -y, -z), new Vector3(x, y, -z), new Vector3(x, y, z), new Vector3(x, -y, z), new Vector3(1f, 0f, 0f));
			AddQuad(v, idx, new Vector3(-x, -y, z), new Vector3(-x, y, z), new Vector3(-x, y, -z), new Vector3(-x, -y, -z), new Vector3(-1f, 0f, 0f));
			// +Y, -Y
			AddQuad(v, idx, new Vector3(-x, y, -z), new Vector3(-x, y, z), new Vector3(x, y, z), new Vector3(x, y, -z), new Vector3(0f, 1f, 0f));
			AddQuad(v, idx, new Vector3(-x, -y, z), new Vector3(-x, -y, -z), new Vector3(x, -y, -z), new Vector3(x, -y, z), new Vector3(0f, -1f, 0f));
			// +Z, -Z
			AddQuad(v, idx, new Vector3(x, -y, z), new Vector3(x, y, z), new Vector3(-x, y, z), new Vector3(-x, -y, z), new Vector3(0f, 0f, 1f));
			AddQuad(v, idx, new Vector3(-x, -y, -z), new Vector3(-x, y, -z), new Vector3(x, y, -z), new Vector3(x, -y, -z), new Vector3(0f, 0f, -1f));
			return FromGeometry(device, v, idx);
		}

		public static Mesh Sphere(Device device, float radius, int slices, int stacks)
		{
			if (slices < 3) slices = 3;
			if (stacks < 2) stacks = 2;
			List<CustomVertex.PositionNormal> v = new List<CustomVertex.PositionNormal>();
			List<short> idx = new List<short>();
			for (int stack = 0; stack <= stacks; stack++)
			{
				double phi = Math.PI * stack / stacks;
				float py = (float)Math.Cos(phi);
				float pr = (float)Math.Sin(phi);
				for (int slice = 0; slice <= slices; slice++)
				{
					double theta = 2.0 * Math.PI * slice / slices;
					float nx = pr * (float)Math.Cos(theta);
					float nz = pr * (float)Math.Sin(theta);
					Vector3 normal = new Vector3(nx, py, nz);
					v.Add(new CustomVertex.PositionNormal(new Vector3(nx * radius, py * radius, nz * radius), normal));
				}
			}
			int ringVerts = slices + 1;
			for (int stack = 0; stack < stacks; stack++)
			{
				for (int slice = 0; slice < slices; slice++)
				{
					short a = (short)(stack * ringVerts + slice);
					short b = (short)(a + ringVerts);
					idx.Add(a); idx.Add((short)(a + 1)); idx.Add(b);
					idx.Add(b); idx.Add((short)(a + 1)); idx.Add((short)(b + 1));
				}
			}
			return FromGeometry(device, v, idx);
		}

		public static Mesh Cylinder(Device device, float radius1, float radius2, float length, int slices, int stacks)
		{
			if (slices < 3) slices = 3;
			if (stacks < 1) stacks = 1;
			List<CustomVertex.PositionNormal> v = new List<CustomVertex.PositionNormal>();
			List<short> idx = new List<short>();
			// Cylinder runs along Z, centred: z in [-length/2, +length/2]; radius1 at -Z end, radius2 at +Z end.
			for (int stack = 0; stack <= stacks; stack++)
			{
				float t = (float)stack / stacks;
				float z = -length / 2f + length * t;
				float r = radius1 + (radius2 - radius1) * t;
				for (int slice = 0; slice <= slices; slice++)
				{
					double theta = 2.0 * Math.PI * slice / slices;
					float cx = (float)Math.Cos(theta);
					float cy = (float)Math.Sin(theta);
					Vector3 normal = new Vector3(cx, cy, 0f);
					normal.Normalize();
					v.Add(new CustomVertex.PositionNormal(new Vector3(cx * r, cy * r, z), normal));
				}
			}
			int ringVerts = slices + 1;
			for (int stack = 0; stack < stacks; stack++)
			{
				for (int slice = 0; slice < slices; slice++)
				{
					short a = (short)(stack * ringVerts + slice);
					short b = (short)(a + ringVerts);
					idx.Add(a); idx.Add(b); idx.Add((short)(a + 1));
					idx.Add((short)(a + 1)); idx.Add(b); idx.Add((short)(b + 1));
				}
			}
			return FromGeometry(device, v, idx);
		}

		public static Mesh TextFromFont(Device device, Font font, string text, float deviation, float extrusion)
		{
			List<CustomVertex.PositionNormal> v = new List<CustomVertex.PositionNormal>();
			List<short> idx = new List<short>();
			Vector3 n = new Vector3(0f, 0f, 1f);
			using (GraphicsPath path = new GraphicsPath())
			{
				if (!string.IsNullOrEmpty(text))
				{
					path.AddString(text, font.FontFamily, (int)font.Style, font.SizeInPoints, new PointF(0f, 0f), StringFormat.GenericDefault);
					path.Flatten(null, Math.Max(deviation, 0.1f));
					PointF[] pts = path.PathPoints;
					byte[] types = path.PathTypes;
					int start = -1;
					for (int i = 0; i < pts.Length; i++)
					{
						if ((types[i] & 0x7) == 0)
						{
							start = i;
						}
						bool closeSub = (types[i] & 0x80) != 0 || i == pts.Length - 1;
						if (closeSub && start >= 0 && i - start >= 2)
						{
							// Fan-triangulate this subpath. (Holes are not subtracted — best effort.)
							short baseIndex = (short)v.Count;
							for (int p = start; p <= i; p++)
							{
								v.Add(new CustomVertex.PositionNormal(new Vector3(pts[p].X, 0f - pts[p].Y, 0f), n));
							}
							int count = i - start + 1;
							for (int t = 1; t < count - 1; t++)
							{
								idx.Add(baseIndex);
								idx.Add((short)(baseIndex + t));
								idx.Add((short)(baseIndex + t + 1));
							}
							start = -1;
						}
					}
				}
			}
			if (v.Count == 0)
			{
				v.Add(new CustomVertex.PositionNormal(new Vector3(0f, 0f, 0f), n));
				v.Add(new CustomVertex.PositionNormal(new Vector3(0f, 0f, 0f), n));
				v.Add(new CustomVertex.PositionNormal(new Vector3(0f, 0f, 0f), n));
				idx.Add(0); idx.Add(1); idx.Add(2);
			}
			return FromGeometry(device, v, idx);
		}

		public static Mesh TessellateNPatches(Mesh mesh, int[] adjacencyIn, float numberSegments, bool quadraticInterpNormals)
		{
			return mesh;
		}
	}

	[Flags]
	public enum FX
	{
		None = 0,
		DoNotSaveState = 1,
		DoNotSaveShaderState = 2,
		DoNotSaveSamplerState = 4
	}

	public sealed class Effect : IDisposable
	{
		public int Begin(FX flags) => 1;
		public void BeginPass(int pass) { }
		public void EndPass() { }
		public void End() { }
		public void Dispose() { }
	}
}
