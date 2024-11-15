using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputController : MonoBehaviour
{
	public event Action SelectObjectClicked;
	
	public void OnMouseClicked(InputAction.CallbackContext actionContext)
	{
		if (actionContext.performed)
		{
			Debug.Log($"Mouse Clicked. Performed:{actionContext.performed}, Pointer:{Pointer.current.position.x},{Pointer.current.position.y}");
			SelectObjectClicked?.Invoke();
		}
	}
}