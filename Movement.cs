using Godot;

namespace d_worldGen
{
    public partial class Movement : Camera3D
    {
        [Export]
        public float Speed = 500.0f;

        [Export]
        public float MouseSensitivity = 0.003f;

        private float pitch = 0.0f;

        public override void _Ready()
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseMotion motion)
            {
                RotateY(-motion.Relative.X * MouseSensitivity);

                pitch -= motion.Relative.Y * MouseSensitivity;
                pitch = Mathf.Clamp(pitch, -Mathf.Pi / 2, Mathf.Pi / 2);

                Rotation = new Vector3(pitch, Rotation.Y, 0);
            }
        }

        public override void _Process(double delta)
        {
            Vector3 direction = Vector3.Zero;

            if (Input.IsKeyPressed(Key.W))
                direction -= Transform.Basis.Z;

            if (Input.IsKeyPressed(Key.S))
                direction += Transform.Basis.Z;

            if (Input.IsKeyPressed(Key.A))
                direction -= Transform.Basis.X;

            if (Input.IsKeyPressed(Key.D))
                direction += Transform.Basis.X;

            if (Input.IsKeyPressed(Key.Space))
                direction += Vector3.Up;

            if (Input.IsKeyPressed(Key.Shift))
                direction += Vector3.Down;

            if (direction != Vector3.Zero)
            {
                direction = direction.Normalized();
                Position += direction * Speed * (float)delta;
            }
        }
    }
}

