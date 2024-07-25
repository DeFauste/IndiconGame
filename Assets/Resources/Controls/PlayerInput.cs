//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Resources/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PCGameplay"",
            ""id"": ""ac868f04-3ef0-4e8f-bc28-df17243991a0"",
            ""actions"": [
                {
                    ""name"": ""Moveble"",
                    ""type"": ""Value"",
                    ""id"": ""8e3469b7-5245-4e31-9cb4-b37941f0e34b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d4c92086-d891-4c8b-868a-48efe49f68e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""F"",
                    ""type"": ""Button"",
                    ""id"": ""bfe57617-31e0-430e-807f-ec4aa5e2c22d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""954218d4-2428-474a-ac3d-3982e9d8ed1f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moveble"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""120711af-b495-463b-a233-bf9faa0906de"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moveble"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0b2847e4-4ff5-4151-bb7c-c0ba8661aad8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moveble"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""64c6a02e-b2bf-4609-bf07-52e3724f117c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9f4ff0d-9dc4-41d4-a466-50f6a2857587"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PCHotKey"",
            ""id"": ""c34ea8b8-ab18-49e7-85ef-cd3115cfed0b"",
            ""actions"": [
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""0c39b2f1-72de-4f21-b47c-4a91b5d63c50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""b3112e0e-1f13-4348-9da2-0657f831ff0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""582a111b-5d00-4ecd-b1a5-468f1dffb6dd"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3d09304-810c-478c-851e-4b3848b239d3"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PCGameplay
        m_PCGameplay = asset.FindActionMap("PCGameplay", throwIfNotFound: true);
        m_PCGameplay_Moveble = m_PCGameplay.FindAction("Moveble", throwIfNotFound: true);
        m_PCGameplay_Jump = m_PCGameplay.FindAction("Jump", throwIfNotFound: true);
        m_PCGameplay_F = m_PCGameplay.FindAction("F", throwIfNotFound: true);
        // PCHotKey
        m_PCHotKey = asset.FindActionMap("PCHotKey", throwIfNotFound: true);
        m_PCHotKey_Restart = m_PCHotKey.FindAction("Restart", throwIfNotFound: true);
        m_PCHotKey_Pause = m_PCHotKey.FindAction("Pause", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PCGameplay
    private readonly InputActionMap m_PCGameplay;
    private List<IPCGameplayActions> m_PCGameplayActionsCallbackInterfaces = new List<IPCGameplayActions>();
    private readonly InputAction m_PCGameplay_Moveble;
    private readonly InputAction m_PCGameplay_Jump;
    private readonly InputAction m_PCGameplay_F;
    public struct PCGameplayActions
    {
        private @PlayerInput m_Wrapper;
        public PCGameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Moveble => m_Wrapper.m_PCGameplay_Moveble;
        public InputAction @Jump => m_Wrapper.m_PCGameplay_Jump;
        public InputAction @F => m_Wrapper.m_PCGameplay_F;
        public InputActionMap Get() { return m_Wrapper.m_PCGameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCGameplayActions set) { return set.Get(); }
        public void AddCallbacks(IPCGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_PCGameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCGameplayActionsCallbackInterfaces.Add(instance);
            @Moveble.started += instance.OnMoveble;
            @Moveble.performed += instance.OnMoveble;
            @Moveble.canceled += instance.OnMoveble;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @F.started += instance.OnF;
            @F.performed += instance.OnF;
            @F.canceled += instance.OnF;
        }

        private void UnregisterCallbacks(IPCGameplayActions instance)
        {
            @Moveble.started -= instance.OnMoveble;
            @Moveble.performed -= instance.OnMoveble;
            @Moveble.canceled -= instance.OnMoveble;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @F.started -= instance.OnF;
            @F.performed -= instance.OnF;
            @F.canceled -= instance.OnF;
        }

        public void RemoveCallbacks(IPCGameplayActions instance)
        {
            if (m_Wrapper.m_PCGameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPCGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_PCGameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PCGameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PCGameplayActions @PCGameplay => new PCGameplayActions(this);

    // PCHotKey
    private readonly InputActionMap m_PCHotKey;
    private List<IPCHotKeyActions> m_PCHotKeyActionsCallbackInterfaces = new List<IPCHotKeyActions>();
    private readonly InputAction m_PCHotKey_Restart;
    private readonly InputAction m_PCHotKey_Pause;
    public struct PCHotKeyActions
    {
        private @PlayerInput m_Wrapper;
        public PCHotKeyActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Restart => m_Wrapper.m_PCHotKey_Restart;
        public InputAction @Pause => m_Wrapper.m_PCHotKey_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PCHotKey; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCHotKeyActions set) { return set.Get(); }
        public void AddCallbacks(IPCHotKeyActions instance)
        {
            if (instance == null || m_Wrapper.m_PCHotKeyActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCHotKeyActionsCallbackInterfaces.Add(instance);
            @Restart.started += instance.OnRestart;
            @Restart.performed += instance.OnRestart;
            @Restart.canceled += instance.OnRestart;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IPCHotKeyActions instance)
        {
            @Restart.started -= instance.OnRestart;
            @Restart.performed -= instance.OnRestart;
            @Restart.canceled -= instance.OnRestart;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IPCHotKeyActions instance)
        {
            if (m_Wrapper.m_PCHotKeyActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPCHotKeyActions instance)
        {
            foreach (var item in m_Wrapper.m_PCHotKeyActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PCHotKeyActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PCHotKeyActions @PCHotKey => new PCHotKeyActions(this);
    public interface IPCGameplayActions
    {
        void OnMoveble(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnF(InputAction.CallbackContext context);
    }
    public interface IPCHotKeyActions
    {
        void OnRestart(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}