using System.Collections;
using TMPro;
using UnityEngine;

public class HUDBottomPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _colorText;
    [SerializeField]
    private TextMeshProUGUI _wrongColorText;

    private GameMatchModel _gameMatchModel;
    private Coroutine _wrongColorTextCoroutine;

    private void Start()
    { 
        _gameMatchModel = FindObjectOfType<GameSceneController>().MatchModel;
        _gameMatchModel.TargetColorChanged += OnTargetColorChanged;
        _gameMatchModel.WrongColorChanged += OnWrongColor;
    }

    private void OnDestroy()
    {
        _gameMatchModel.TargetColorChanged -= OnTargetColorChanged;
        _gameMatchModel.WrongColorChanged -= OnWrongColor;
        _gameMatchModel = null;

        if (_wrongColorTextCoroutine != null)
        {
            StopCoroutine(_wrongColorTextCoroutine);
            _wrongColorTextCoroutine = null;
        }
    }

    private void OnTargetColorChanged(Color newTargetColor)
    {
        _colorText.color = newTargetColor;
    }

    private void OnWrongColor(Color wrongColor)
    {
        if (_wrongColorTextCoroutine != null)
        {
            StopCoroutine(_wrongColorTextCoroutine);
        }

        _wrongColorText.color = wrongColor;
        _wrongColorTextCoroutine = StartCoroutine(WrongColorCoroutine());
    }

    private IEnumerator WrongColorCoroutine()
    {
        float maxDuration = 1.0f;
        float time = 0;
        
        _wrongColorText.enabled = true;
        while (time < maxDuration)
        {
            float lerpedColor = Mathf.Lerp(1, 0, time);
            
            Color textColor = _wrongColorText.color;
            textColor.a = lerpedColor;
            _wrongColorText.color = textColor;
            
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _wrongColorText.enabled = false;
        _wrongColorTextCoroutine = null;
    }
}