using Godot;

namespace CyberPolice.scripts;

public partial class SillyBrain : Node, IBrain
{
    private Human _human;
     
    public override void _Ready()
    {
        _human = GetParent<Human>();
    }
    
    public override void _Process(double delta)
    {
        _human.Jump();
    }
}