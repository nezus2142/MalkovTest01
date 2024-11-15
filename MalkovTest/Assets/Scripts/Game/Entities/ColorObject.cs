using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    public Color CurrentColor { get; private set; }
    
    public void SetRendererColor(Color newColor)
    {
        CurrentColor = newColor;
        _renderer.material.color = CurrentColor;
    }
}