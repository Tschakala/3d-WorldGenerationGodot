using System;
using Godot;
using Vector2 = Godot.Vector2;

namespace d_worldgen._2D;

public partial class CameraController : Camera2D
{
	[Export] private Resource _defaultCursor;
	[Export] private Resource _grabCursor;
	[Export] private float _sensitivity = 0.25f;
	[Export] private float _zoomSensitivity = 0.025f;
 	private bool _isMoving;

	private void MoveCamera(Vector2 delta)
	{
		GlobalPosition += delta;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			if (Input.IsActionPressed("camera_move"))
			{
				MoveCamera(-mouseMotion.Relative / GetZoom().X);
				Input.SetCustomMouseCursor(_grabCursor);
			}
			else
			{
				Input.SetCustomMouseCursor(_defaultCursor);
			}
		}

		if (Input.IsActionJustReleased("camera_zoom"))
		{
			Zoom += Vector2.One * _zoomSensitivity;
			Zoom = new Vector2(Math.Min(2,  Zoom.X), Math.Min(2, Zoom.Y));
			//SetZoom(_currentZoom);
		}
		
		if (Input.IsActionJustReleased("camera_unzoom"))
		{
			Zoom -= Vector2.One * _zoomSensitivity;
			Zoom = new Vector2(Math.Max(0.1f,  Zoom.X), Math.Max(0.1f, Zoom.Y));
			//SetZoom(_currentZoom);
		}
	}
}