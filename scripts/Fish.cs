using Godot;
using System;
using System.Collections.Generic;

public partial class Fish : Node2D
{
	private AnimationPlayer _animationPlayer;
	public string Name;
	public int Value;
	
	public static Dictionary<string, FishData> FishTypes = new Dictionary<string, FishData>
	{
		{ "blue", new FishData("blue", 1, GD.Load<Texture2D>("res://assets/sprites/fish.png")) },
		{ "shark",  new FishData("shark", 10, GD.Load<Texture2D>("res://assets/sprites/shark.png")) }
	};
	
	public override void _Ready()
	{
		makeRandom();
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("Caught");
		var timer = GetNode<Timer>("Timer");
		timer.Start();
	}
	
	//will make this instance a random fishData
	public void makeRandom() {
		var sprite = GetNode<Sprite2D>("Sprite2D");
		var rng = new RandomNumberGenerator();
		rng.Randomize();
		
		int randomInt = rng.RandiRange(0,10);
		
		if (randomInt < 2) {
			sprite.Texture = FishTypes["shark"].Texture;
			sprite.Scale = new Vector2(0.03f, 0.03f);
			this.Name = "shark";
			this.Value = FishTypes["shark"].Value;
		} else {//blueFish
			sprite.Texture = FishTypes["blue"].Texture;
			sprite.Scale = new Vector2(1f, 1f);
			this.Name = "blue";
			this.Value = FishTypes["blue"].Value;
		}
	}
	
	public void OnCooldownTimerTimeout()
	{
		QueueFree();
	}
}
public class FishData
{
	public string Name { get; set;}
	public int Value { get; set;}
	public Texture2D Texture { get; set;}
	
	public FishData(string name, int value, Texture2D texture)
	{
		Name = name;
		Value = value;
		Texture = texture;
	}
}
