using Godot;
using System;

public partial class Store : Control
{
	private PopupMenu menu;
	
	public override void _Ready()
	{
		menu = GetNode<PopupMenu>("PopupMenu");
		buildMenu();
		menu.IdPressed += OnMenuItemPressed;
	}
	
	public void buildMenu(){
		Texture2D t = resized();
		menu.AddIconItem(t, "Standard Fishing Rod");
	}
	
	public Texture2D resized() {
		Texture2D t = GD.Load<Texture2D>("res://assets/sprites/fishingRod2.png");
		Image img = t.GetImage();
		img.Resize(32, 32);
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
