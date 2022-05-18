// GENERATED AUTOMATICALLY FROM 'Assets/InputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""186a8feb-94e0-44d3-8334-137119e045fb"",
            ""actions"": [
                {
                    ""name"": ""ShortcutButton"",
                    ""type"": ""Button"",
                    ""id"": ""01a1e8f6-5e7e-4c68-b878-2969df1a9e0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LightButton"",
                    ""type"": ""Button"",
                    ""id"": ""0d492423-cb99-48ac-98ff-bb51ead9816a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LaunchButton"",
                    ""type"": ""Button"",
                    ""id"": ""39d8de0a-989b-4c73-adfe-d0232eda1640"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeavyButton"",
                    ""type"": ""Button"",
                    ""id"": ""f8351a42-10c8-47b3-b37a-0a7dd1a16f2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6dd038e2-dfa2-4f19-81d7-bda5a3b821e9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ContextConfirm"",
                    ""type"": ""Button"",
                    ""id"": ""6a480aab-e6ea-4a27-b63f-1c74bf84ae5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MenuButton"",
                    ""type"": ""Button"",
                    ""id"": ""b0edabfd-1d2b-4536-9552-6ba575bd6942"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""e535bb67-8a85-4f1c-91dd-e0f4b621e744"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cb5f49ba-2a12-41c4-a60b-c6ac9b83b6e7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1584ec2-3746-4b9f-9ff1-9788107b35a1"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dda854b3-8c3d-44b1-b399-254573a4c9ae"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LaunchButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be10c12e-6841-48b5-9e9e-3846b3898d9a"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LaunchButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""386579ad-da6d-44af-b125-59554a7a3180"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35d3f02b-ccb1-4ccc-9315-e15037cd7414"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b2fa617-56e7-4aa3-94b6-bfdc4d409cf7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShortcutButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c0339e2-5242-418b-b957-20c784a6cf71"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShortcutButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0c513989-146e-4229-bf04-a24161d7b6d2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""04b44693-1b44-4dc1-8e24-ca39f8edeb04"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""d4ead18b-13b3-46e9-bd5b-75d1da37bc13"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""2d931a31-8af3-4202-a66b-328c8ebfe7b3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""2de73833-9ac0-421e-b939-a68343aab43c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5279c1b3-863a-4ba1-9e30-f136118dbc1e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""38bcca75-6e92-4530-bb1c-ce7d009bc9e4"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1341471c-6f8a-4ebf-bd59-cf9b409fe2d3"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cd8058ac-7172-480f-a58b-85a7171fbc0b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""38f8e36a-4a36-441a-bc78-afa173c9e741"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""84fa7912-064d-4112-bdc0-ee973134dd6e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ContextConfirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""667f2e62-f77d-4918-8a2a-f52cf9acafae"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ContextConfirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a17bb799-3e3d-4d2e-b066-940a9dca9b92"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78605b43-1a3e-4661-ad23-b58f4d86bc13"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""544ccd4d-181b-4c7c-9712-8179a5d95f73"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d765cb9a-8142-4794-8955-5d6c8f837fc6"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_ShortcutButton = m_Player.FindAction("ShortcutButton", throwIfNotFound: true);
        m_Player_LightButton = m_Player.FindAction("LightButton", throwIfNotFound: true);
        m_Player_LaunchButton = m_Player.FindAction("LaunchButton", throwIfNotFound: true);
        m_Player_HeavyButton = m_Player.FindAction("HeavyButton", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_ContextConfirm = m_Player.FindAction("ContextConfirm", throwIfNotFound: true);
        m_Player_MenuButton = m_Player.FindAction("MenuButton", throwIfNotFound: true);
        m_Player_Dodge = m_Player.FindAction("Dodge", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_ShortcutButton;
    private readonly InputAction m_Player_LightButton;
    private readonly InputAction m_Player_LaunchButton;
    private readonly InputAction m_Player_HeavyButton;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_ContextConfirm;
    private readonly InputAction m_Player_MenuButton;
    private readonly InputAction m_Player_Dodge;
    public struct PlayerActions
    {
        private @InputController m_Wrapper;
        public PlayerActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @ShortcutButton => m_Wrapper.m_Player_ShortcutButton;
        public InputAction @LightButton => m_Wrapper.m_Player_LightButton;
        public InputAction @LaunchButton => m_Wrapper.m_Player_LaunchButton;
        public InputAction @HeavyButton => m_Wrapper.m_Player_HeavyButton;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @ContextConfirm => m_Wrapper.m_Player_ContextConfirm;
        public InputAction @MenuButton => m_Wrapper.m_Player_MenuButton;
        public InputAction @Dodge => m_Wrapper.m_Player_Dodge;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @ShortcutButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShortcutButton;
                @ShortcutButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShortcutButton;
                @ShortcutButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShortcutButton;
                @LightButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightButton;
                @LightButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightButton;
                @LightButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightButton;
                @LaunchButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLaunchButton;
                @LaunchButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLaunchButton;
                @LaunchButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLaunchButton;
                @HeavyButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyButton;
                @HeavyButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyButton;
                @HeavyButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyButton;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @ContextConfirm.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContextConfirm;
                @ContextConfirm.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContextConfirm;
                @ContextConfirm.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContextConfirm;
                @MenuButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuButton;
                @MenuButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuButton;
                @MenuButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenuButton;
                @Dodge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ShortcutButton.started += instance.OnShortcutButton;
                @ShortcutButton.performed += instance.OnShortcutButton;
                @ShortcutButton.canceled += instance.OnShortcutButton;
                @LightButton.started += instance.OnLightButton;
                @LightButton.performed += instance.OnLightButton;
                @LightButton.canceled += instance.OnLightButton;
                @LaunchButton.started += instance.OnLaunchButton;
                @LaunchButton.performed += instance.OnLaunchButton;
                @LaunchButton.canceled += instance.OnLaunchButton;
                @HeavyButton.started += instance.OnHeavyButton;
                @HeavyButton.performed += instance.OnHeavyButton;
                @HeavyButton.canceled += instance.OnHeavyButton;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ContextConfirm.started += instance.OnContextConfirm;
                @ContextConfirm.performed += instance.OnContextConfirm;
                @ContextConfirm.canceled += instance.OnContextConfirm;
                @MenuButton.started += instance.OnMenuButton;
                @MenuButton.performed += instance.OnMenuButton;
                @MenuButton.canceled += instance.OnMenuButton;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnShortcutButton(InputAction.CallbackContext context);
        void OnLightButton(InputAction.CallbackContext context);
        void OnLaunchButton(InputAction.CallbackContext context);
        void OnHeavyButton(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnContextConfirm(InputAction.CallbackContext context);
        void OnMenuButton(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
    }
}
