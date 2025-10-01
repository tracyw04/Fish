using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private int speed = 105;
	private Vector2 currentVelocity;
	private AnimationPlayer _animationPlayer;
	private Inventory bag;
	private int coins;
	private Label mainLabel;
	private bool hasFishingRod = false;
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}
	public Direction FacingDirection { get; private set; } = Direction.Down;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		handleInput();

		Velocity = currentVelocity;
		MoveAndSlide();
	}

	private void handleInput()
	{
		currentVelocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		currentVelocity *= speed;
	}

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		bag = GetNode<Inventory>("../Inventory");
		coins = 0;
		mainLabel = GetNode<Label>("../CanvasLayer/Label");
		UpdateLabel();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_left"))
		{
			_animationPlayer.Play("WalkingL");
			FacingDirection = Direction.Left;
		}
		else if (Input.IsActionPressed("ui_right"))
		{
			_animationPlayer.Play("WalkingR");
			FacingDirection = Direction.Right;
		}
		else if (Input.IsActionPressed("ui_up"))
		{
			_animationPlayer.Play("WalkingU");
			FacingDirection = Direction.Up;
		}
		else if (Input.IsActionPressed("ui_down"))
		{
			_animationPlayer.Play("WalkingF");
			FacingDirection = Direction.Down;
		}
		else
		{
			_animationPlayer.Play("Idle");
			FacingDirection = Direction.Down;
		}
	}

	public void addToBag(Fish f)
	{
		bag.addToStorage(Fish.FishTypes[f.Name]);
	}

	public void makeProfit()
	{
		coins += bag.sell();
		UpdateLabel();
	}

	private void UpdateLabel()
	{
		mainLabel.Text = $"Money: ${this.coins}\n";
	}

	public void pickedUpFishingRod()
	{
		hasFishingRod = true;
	}

	public int getBagSize()
	{
		return this.bag.getStorageSize();
	}

	public int getCoins()
	{
		return this.coins;
	}

	public bool getHasFishingRod()
	{
		return this.hasFishingRod;
	}
}
