using Godot;

namespace CyberPolice.scripts;

public enum Direction
{
	right,
	left
}

public partial class Human : CharacterBody2D
{
	[Export] private CharacterStats _stats = new();
	private Direction _direction = Direction.right;
	public float walkingAxis;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private AnimatedSprite2D _animatedSprite;
	private Vector2 _newVelocity;
	private bool _justJumped;
	public bool running;
	public bool crouching;

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var brain = GetNodeOrNull<IBrain>(".");
		if(brain == null)
			GD.Print("WARNING: no brain found, a human won't do anything");
	}

	public void Jump()
	{
		_justJumped = true;
	}

	private void HandleAnimation()
	{
		var animation = "idle";
		
		if (crouching)
			animation = "crouch_idle";
			
		if (walkingAxis != 0f)
		{
			animation = running ? "run" : "walk";

			if (animation == "walk" && crouching)
				animation = "crouch_walk";
							
			if (walkingAxis > 0 && _direction == Direction.left)
			{
				_direction = Direction.right;
				_animatedSprite.FlipH = !_animatedSprite.FlipH;
			}
			if (walkingAxis < 0 && _direction == Direction.right)
			{
				_direction = Direction.left;
				_animatedSprite.FlipH = !_animatedSprite.FlipH;
			}
		}
		if (!IsOnFloor())
		{
			animation = "jump";
		}
		
		_animatedSprite.Play(animation);
	}

	public override void _Process(double delta)
	{
		HandleAnimation();
	}

	public override void _PhysicsProcess(double delta)
	{
		_newVelocity = Velocity;
		_newVelocity.Y += _gravity * (float)delta;

		if (_justJumped && IsOnFloor())
		{
			_newVelocity.Y = _stats.jumpVelocity;
			_justJumped = false;
		}

		if (walkingAxis != 0f)
		{
			var speed = running ? _stats.runSpeed : _stats.walkSpeed;
			_newVelocity.X = walkingAxis * speed;
		}
		else
		{
			_newVelocity.X = Mathf.MoveToward(Velocity.X, 0, _stats.runSpeed);
		}

		Velocity = _newVelocity;
		MoveAndSlide();
	}
}