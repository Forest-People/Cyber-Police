using Godot;

namespace CyberPolice.scripts;

public partial class CharacterStats : Resource
{
    [Export] public float walkSpeed;
    [Export] public float runSpeed;
    [Export] public float jumpVelocity;

    public CharacterStats() : this(0f, 0f, 0f) {}
    public CharacterStats(float walkSpeed, float runSpeed, float jumpVelocity)
    {
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        this.jumpVelocity = jumpVelocity;
    }
}