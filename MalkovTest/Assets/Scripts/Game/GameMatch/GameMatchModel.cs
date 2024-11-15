using System;
using UnityEngine;

/// <summary>
/// Keeps information about match state and sends events about changing properties.
/// </summary>
public class GameMatchModel
{
	private Color _targetColor;
	private Color _correctColor;
	private Color _wrongColor;

	public Color TargetColor
	{
		get => _targetColor;
		set
		{
			_targetColor = value;
			TargetColorChanged?.Invoke(_targetColor);
		}
	}
	
	public Color CorrectColor
	{
		get => _correctColor;
		set
		{
			_correctColor = value;
			SelectedColorChanged?.Invoke(_correctColor);
		}
	}

	public Color WrongColor
	{
		get => _wrongColor;
		set
		{
			_wrongColor = value;
			WrongColorChanged?.Invoke(_wrongColor);
		}
	}

	public event Action<Color> TargetColorChanged;
	public event Action<Color> SelectedColorChanged;
	public event Action<Color> WrongColorChanged;
}