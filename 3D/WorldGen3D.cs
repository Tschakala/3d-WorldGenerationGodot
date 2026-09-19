using Godot;

namespace d_worldgen._3D
{
	public partial class WorldGen3D : Node3D
	{
		[Export] private int _middle;
		[Export] private int _width = 250;
		[Export] private float _heightMulti = 30f;
		[Export] private int _blockMulti = 1;
		[Export] private float _frequency = 0.01f;
		[Export] private Color _color = Colors.YellowGreen;
		public override void _Ready()
		{
			var meshInstance = new MeshInstance3D();
			SurfaceTool st = new();
			st.Begin(Mesh.PrimitiveType.Triangles);
			var noise = new FastNoiseLite();
			noise.Frequency = _frequency;

			for (int x = -(_width / 2) + _middle; x < _width; x = x + _blockMulti)
			{
				for (int z = -(_width / 2) + _middle; z < _width; z = z + _blockMulti)
				{
					float yA = noise.GetNoise2D(x, z) * _heightMulti;
					float yB = noise.GetNoise2D(x + _blockMulti, z) * _heightMulti;
					float yC = noise.GetNoise2D(x, z + _blockMulti) * _heightMulti;
					float yD = noise.GetNoise2D(x + _blockMulti, z + _blockMulti) * _heightMulti;

					Vector3 a = new(x, yA, z);
					Vector3 b = new(x + _blockMulti, yB, z);
					Vector3 c = new(x, yC, z + _blockMulti);
					Vector3 d = new(x + _blockMulti, yD, z + _blockMulti);

					st.SetUV(new Vector2(0, 0));
					st.AddVertex(a);

					st.SetUV(new Vector2(1, 0));
					st.AddVertex(b);

					st.SetUV(new Vector2(0, 1));
					st.AddVertex(c);

					st.SetUV(new Vector2(1, 0));
					st.AddVertex(b);

					st.SetUV(new Vector2(1, 1));
					st.AddVertex(d);

					st.SetUV(new Vector2(0, 1));
					st.AddVertex(c);
				}
			}
			
			st.GenerateNormals();
			ArrayMesh mesh = st.Commit();
			var material = new StandardMaterial3D();
			//material.AlbedoTexture = GD.Load<Texture2D>("res://texture_06.png"); # if you want to use a texture put the file path into the "".
			material.AlbedoColor = new Color(_color);
			mesh.SurfaceSetMaterial(0, material);
			meshInstance.Mesh = mesh;
			AddChild(meshInstance);
			meshInstance.CreateTrimeshCollision();
		}

		public override void _Process(double delta)
		{
			
		}
	}
}

