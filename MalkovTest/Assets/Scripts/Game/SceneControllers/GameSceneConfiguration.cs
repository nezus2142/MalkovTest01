using System.Collections.Generic;
using UnityEngine;

public class GameSceneConfiguration : MonoBehaviour
{
	[SerializeField] private ColorObject _colorObjectPrefab;
	[SerializeField] private LayerMask _colorObjectLayerMask;
	[SerializeField] private List<Transform> _staticObjectPositionPoints;
	[SerializeField] private GameplayInputController _gameplayInputController;
	[SerializeField] private Camera _gameSceneCamera;

	public IEnumerable<Transform> StaticObjectPositionPoints => _staticObjectPositionPoints;

	public ColorObject ColorObjectPrefab => _colorObjectPrefab;

	public LayerMask ColorObjectLayerMask => _colorObjectLayerMask;

	public GameplayInputController InputController => _gameplayInputController;

	public Camera GameSceneCamera => _gameSceneCamera;
}