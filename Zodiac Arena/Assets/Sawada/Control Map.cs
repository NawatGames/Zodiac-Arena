// GENERATED AUTOMATICALLY FROM 'Assets/Sawada/Control Map.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControlMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Control Map"",
    ""maps"": [
        {
            ""name"": ""MovimentMap"",
            ""id"": ""20f792a8-7d47-49f0-8341-625904a747e0"",
            ""actions"": [
                {
                    ""name"": ""Direction"",
                    ""type"": ""Button"",
                    ""id"": ""751c7a3d-c092-4bd8-9eba-7b50b6dad6e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""wasd"",
                    ""id"": ""f9b149ab-433a-430d-aed4-781cc36906d5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e1cc4d49-a5fc-4121-a3a7-3d2e7c51eb6a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""aaa954b3-edc2-4bf8-ad4f-aa1cb838d556"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MovimentMap
        m_MovimentMap = asset.FindActionMap("MovimentMap", throwIfNotFound: true);
        m_MovimentMap_Direction = m_MovimentMap.FindAction("Direction", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // MovimentMap
    private readonly InputActionMap m_MovimentMap;
    private IMovimentMapActions m_MovimentMapActionsCallbackInterface;
    private readonly InputAction m_MovimentMap_Direction;
    public struct MovimentMapActions
    {
        private @ControlMap m_Wrapper;
        public MovimentMapActions(@ControlMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Direction => m_Wrapper.m_MovimentMap_Direction;
        public InputActionMap Get() { return m_Wrapper.m_MovimentMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovimentMapActions set) { return set.Get(); }
        public void SetCallbacks(IMovimentMapActions instance)
        {
            if (m_Wrapper.m_MovimentMapActionsCallbackInterface != null)
            {
                @Direction.started -= m_Wrapper.m_MovimentMapActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_MovimentMapActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_MovimentMapActionsCallbackInterface.OnDirection;
            }
            m_Wrapper.m_MovimentMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
            }
        }
    }
    public MovimentMapActions @MovimentMap => new MovimentMapActions(this);
    public interface IMovimentMapActions
    {
        void OnDirection(InputAction.CallbackContext context);
    }
}
