using System;
using UnityEngine;

public class InputManager
{
    public event Action OnTap;
    private bool IsTapped => Input.GetMouseButtonDown(0);

    public void Update()
    {
        if(IsTapped)
            OnTap?.Invoke();
    }
}