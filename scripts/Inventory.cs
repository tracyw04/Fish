using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node2D
{
	public string startText;

	private List<FishData> storage;
	public override void _Ready()
	{
		startText = "start";
		storage = new List<FishData>();
	}

	public void addToStorage(FishData f)
	{
		storage.Add(f);
	}

	public int sell()
	{
		int total = 0;
		foreach (FishData f in storage)
		{
			total += f.Value;
		}
		storage.Clear();
		return total;
	}

	public int getStorageSize()
	{
		return storage.Count;
	}
}
