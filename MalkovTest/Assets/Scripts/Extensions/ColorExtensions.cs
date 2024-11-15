using UnityEngine;

public static class ColorExtensions
{
	public static Color GetRandomColor()
	{
		return new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
	}
}