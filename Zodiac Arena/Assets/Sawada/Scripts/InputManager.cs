using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : LazySingleton<InputManager> , ControlMap.IMovimentMapActions 
{
    public event Action<Vector2> DirectionChangedEvent;
    
    public ControlMap controlMap;

    private void Start()
    {
        if (created)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            created = true;
            DontDestroyOnLoad(gameObject);
        }
        controlMap = new ControlMap();
        controlMap.MovimentMap.SetCallbacks(this);
        controlMap.Enable();
    }

    private void OnDestroy()
    {
        controlMap?.Dispose();
    }
    public void OnDirection(InputAction.CallbackContext context)
    {
            
        //print("OnDirection");
        DirectionChangedEvent?.Invoke(context.ReadValue<Vector2>());
    }

}
