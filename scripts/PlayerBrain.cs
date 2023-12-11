using Godot;

namespace CyberPolice.scripts;

interface IBrain
{
}

public partial class PlayerBrain : Node, IBrain
{
    private Human _human;
    private PlayerCamera _camera;
     
     public override void _Ready()
     {
         _human = GetParent<Human>();
         _camera = GetNode<PlayerCamera>("../Camera2D");
     }

     public override void _Process(double delta)
     {
         HandleInput();
     }
     
     private void HandleInput()
     {
         _human.running = Input.IsActionPressed("run");

         _human.crouching = Input.IsActionPressed("crouch");
     
         if (Input.IsActionJustPressed("jump"))
             _human.Jump();

         _human.walkingAxis = Input.GetAxis("go_left", "go_right");

         // mouse wheel doesn't work for some reason, use + and - on numpad
         if (Input.IsActionPressed("camera_zoom_in"))
             _camera.ZoomIn();
         if (Input.IsActionPressed("camera_zoom_out"))
             _camera.ZoomOut();
     }
}
