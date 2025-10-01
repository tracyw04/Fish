using Godot;
using System;
using System.Collections.Generic;

public partial class Store : Control
{
	private PopupMenu menu;
	
	public static Dictionary<int, RodData> RodTypes = new Dictionary<int, RodData>
	{
		{ 0, new RodData("Standard Fishing Rod", 100, GD.Load<Texture2D>("res://assets/sprites/fishingRod2.png")) },
		{ 1,  new RodData("Magic Fishing Rod", 350, GD.Load<Texture2D>("res://assets/sprites/fishingRod3.png")) }
	};
	
	public override void _Ready()
	{
		menu = GetNode<PopupMenu>("PopupMenu");
		buildMenu();
		menu.IdPressed += OnMenuItemPressed;
	}
	
	public void buildMenu(){
		for (int i = 0; i < RodTypes.Count; i++) {
			Texture2D before = RodTypes[i].Texture;
			Texture2D newRod = resized(before);
			menu.AddIconItem(newRod, RodTypes[i].Name);
		}
	}
	
	public Texture2D resized(Texture2D t) {
		Image img = t.GetImage();
		img.Resize(16, 16);
		return ImageTexture.CreateFromImage(img);
	}
	
	private void OnMenuItemPressed(long id)
	{
		string itemText = menu.GetItemText((int)id);
		GD.Print("Fishing rod selected: " + itemText);
	}
	
	public void ShowMenu() {
		menu.PopupCentered();
	}
}

public class RodData
{
	public string Name { get; set;}
	public int Value { get; set;}
	public Texture2D Texture { get; set;}
	
	public RodData(string name, int value, Texture2D texture)
	{
		Name = name;
		Value = value;
		Texture = texture;
	}
}
