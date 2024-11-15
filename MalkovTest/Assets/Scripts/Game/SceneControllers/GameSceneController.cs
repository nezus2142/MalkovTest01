using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
	[SerializeField] private GameSceneConfiguration _gameSceneConfiguration;

	private GameMatchController _gameMatchController;
	private GameMatchModel _gameMatchModel;

	public GameMatchModel MatchModel => _gameMatchModel;

	private void Awake()
	{
		_gameMatchModel = new GameMatchModel();
		_gameMatchController = new GameMatchController(_gameMatchModel, _gameSceneConfiguration);
	}

	private void OnDestroy()
	{
		_gameMatchController.DisposeMatch();
	}

	private async void Start()
	{
		await Task.Delay(1500);
		_gameMatchController.StartMatch();
	}
}