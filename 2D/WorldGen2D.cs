using Godot;

namespace d_worldgen._2D;

public partial class WorldGen2D : Node2D
{
	[Export] private int _middle;
	[Export] private int _width = 500;
	[Export] private float _frequency = 0.03f;
	[Export] private TileMapLayer _world;
	[Export] private float _amountOfTiles = 16f;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();
	
	public override void _Ready()
	{
		FastNoiseLite noise = new FastNoiseLite();
		noise.Frequency = _frequency;

		BuildMap(noise);
	}
	
	public override void _Process(double delta)
	{

	}
	
	private void BuildMap(FastNoiseLite noise)
	{
		for (int i = 0; i < _width; i++)
		{
			for (int j = 0; j < _width; j++)
			{
				int x = j - (_width / 2);
				int y = i - (_width / 2);
				int c = -1;
				Vector2I idTileMap = new Vector2I();
								
				float tile = noise.GetNoise2D(x, y);
				tile = (tile + 1) / 2;

				float tempTile = tile;

				while (tile > 0)
				{
					c++;
					tile = tile - (1 / _amountOfTiles);
				}
				
				//GD.Print(tempTile + "		Set: " + c);
				
				//SetTile(_world, new Vector2I(y, x), 0, new Vector2I(c, 0));

				if (tempTile > 0.45f)
				{
					SetTile(_world, new Vector2I(y, x), 0, new Vector2I(0, 0));
				}
				else
				{
					SetTile(_world, new Vector2I(y, x), 0, new Vector2I(15, 0));
				}
			}
		}
		
		SetTile(_world, new Vector2I(0, 0), 1, new Vector2I(0, 0));
	}

	private void SetTile(TileMapLayer layer, Vector2I pos, int idMap, Vector2I idTile)
	{
		layer.SetCell(pos, idMap, idTile);
	}
}