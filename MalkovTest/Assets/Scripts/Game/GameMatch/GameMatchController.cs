using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class GameMatchController
{
	private List<ColorObject> _colorObjectsInstances;
	private GameSceneConfiguration _gameSceneConfiguration;
	private ObjectPool<ColorObject> _colorObjectsPool;

	private GameMatchModel Model { get; }

	public GameMatchController(GameMatchModel matchModel, GameSceneConfiguration sceneConfiguration)
	{
		Model = matchModel;
		_gameSceneConfiguration = sceneConfiguration;

		_colorObjectsInstances = new List<ColorObject>();
		_colorObjectsPool = new ObjectPool<ColorObject>(() => Object.Instantiate(_gameSceneConfiguration.ColorObjectPrefab));
	}

	public void StartMatch()
	{
		_gameSceneConfiguration.InputController.SelectObjectClicked += OnSelectObjectClicked;

		GenerateObjects();
		int colorObjectsCount = _colorObjectsInstances.Count;
		int randomColorObjectIndex = Random.Range(0, colorObjectsCount);
		Color targetRandomColor = _colorObjectsInstances[randomColorObjectIndex].CurrentColor;
		Model.TargetColor = targetRandomColor;
	}

	public void FinishMatch()
	{
		_gameSceneConfiguration.InputController.SelectObjectClicked -= OnSelectObjectClicked;
		ReleaseObjects();
	}

	public void DisposeMatch()
	{
		_gameSceneConfiguration.InputController.SelectObjectClicked -= OnSelectObjectClicked;
		_gameSceneConfiguration = null;

		ReleaseObjects();
		_colorObjectsPool.Clear();
		_colorObjectsPool = null;
		_colorObjectsInstances = null;
	}

	private void GenerateObjects()
	{
		foreach (Transform staticPoint in _gameSceneConfiguration.StaticObjectPositionPoints)
		{
			Vector3 staticPositionPosition = staticPoint.position;
			var newObject = _colorObjectsPool.Get();
			newObject.transform.position = staticPositionPosition;
			Color randomColor = ColorExtensions.GetRandomColor();
			newObject.SetRendererColor(randomColor);
			_colorObjectsInstances.Add(newObject);
		}
	}

	private void ReleaseObjects()
	{
		foreach (ColorObject objInstance in _colorObjectsInstances)
		{
			_colorObjectsPool.Release(objInstance);
		}
		
		_colorObjectsInstances.Clear();
	}

	private void OnSelectObjectClicked()
	{
		Vector2 pointerPosition = Pointer.current.position.value;
		Ray worldPosition = _gameSceneConfiguration.GameSceneCamera.ScreenPointToRay(pointerPosition);
		RaycastHit hit;
		if (Physics.Raycast(worldPosition, out hit, float.PositiveInfinity, _gameSceneConfiguration.ColorObjectLayerMask))
		{
			Debug.Log($"RaycastHit:{hit.transform.gameObject.name}");
			ColorObject colorObject = hit.transform.gameObject.GetComponent<ColorObject>();
			if (colorObject == null)
			{
				Debug.LogError("Not found ColorObject component on RaycastHit result gameobject.");
				return;
			}

			Color selectedColor = colorObject.CurrentColor;

			if (selectedColor == Model.TargetColor)
			{
				Model.CorrectColor = selectedColor;
				FinishMatch();
				StartMatch();
			}
			else
			{
				Model.WrongColor = selectedColor;
			}
		}
	}
}