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
                },
                {
                    ""name"": ""WeaponChangeLeft"",
                    ""type"": ""Button"",
                    ""id"": ""b904fe94-656b-4e05-9222-ed7e205b3fbc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""WeaponChangeRight"",
                    ""type"": ""Button"",
                    ""id"": ""ee9bde5b-c915-421c-8d2c-18007fa86c06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""HealthButton"",
                    ""type"": ""Button"",
                    ""id"": ""18d94b0b-e67e-4862-a52f-613d632fa325"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ManaButton"",
                    ""type"": ""Button"",
                    ""id"": ""f5e5ee78-4055-4e36-8220-32e1c0904b1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""StaminaButton"",
                    ""type"": ""Button"",
                    ""id"": ""4c415d50-e69d-45b5-a153-8c7fe325206c"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""6a7d1b87-8553-4b58-91b7-02ecbc031b07"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponChangeLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86ab8c2d-3c03-49d1-b60f-666ca9c90822"",
                    ""path"": ""<DualShockGamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponChangeLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""081be089-c8aa-4421-a9fe-c4f143c5218d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponChangeRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77cf3e38-1fb2-4765-a92a-ae98d35debb9"",
                    ""path"": ""<DualShockGamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponChangeRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39c0a023-8e93-4b41-9cf9-6d84b0ac0396"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HealthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29ac7fa1-e347-41e1-9baf-3ec4ec65f764"",
                    ""path"": ""<DualShockGamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HealthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70e76456-49c7-44ae-bafb-f97f254205bb"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManaButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ef84dce-2390-4714-b6ac-4683cd9ee3d3"",
                    ""path"": ""<DualShockGamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManaButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b38db04-9605-419d-abf4-f611d5a85277"",
                    ""path"": ""<DualShockGamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManaButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0fb4115-577e-4093-a02c-599a2dad697c"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StaminaButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e806bf6-8129-4ad5-8ec9-541a56f840af"",
                    ""path"": ""<DualShockGamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StaminaButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Paused"",
            ""id"": ""23a3745a-a776-4265-882b-f2de6960cc09"",
            ""actions"": [
                {
                    ""name"": ""MenuButton"",
                    ""type"": ""Button"",
                    ""id"": ""d4534f28-b31c-48e5-b613-e8d44db168a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4aa5dbf0-b61a-442d-b596-5f15258092e8"",
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
                    ""id"": ""5cdcc5df-5ef3-4d2f-baeb-a2a9058a5abe"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interacting"",
            ""id"": ""b5f3dacd-db8b-46e5-817f-90c67a33252d"",
            ""actions"": [
                {
                    ""name"": ""EndInteraction"",
                    ""type"": ""Button"",
                    ""id"": ""ca26cb18-381b-4614-b7ab-87eed7fa5e68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""faa7d9e2-85af-477d-98f7-83911a73018b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""258ac595-529a-4ad7-93c6-36d064d1963d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndInteraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7cb6c8b-921c-413c-9f66-7b43414416f6"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndInteraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""133cd8d7-d329-460f-9770-b2bfadc76a6d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c27c0c13-eb09-4e9b-a619-63a1d9d5028a"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
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
        m_Player_WeaponChangeLeft = m_Player.FindAction("WeaponChangeLeft", throwIfNotFound: true);
        m_Player_WeaponChangeRight = m_Player.FindAction("WeaponChangeRight", throwIfNotFound: true);
        m_Player_HealthButton = m_Player.FindAction("HealthButton", throwIfNotFound: true);
        m_Player_ManaButton = m_Player.FindAction("ManaButton", throwIfNotFound: true);
        m_Player_StaminaButton = m_Player.FindAction("StaminaButton", throwIfNotFound: true);
        // Paused
        m_Paused = asset.FindActionMap("Paused", throwIfNotFound: true);
        m_Paused_MenuButton = m_Paused.FindAction("MenuButton", throwIfNotFound: true);
        // Interacting
        m_Interacting = asset.FindActionMap("Interacting", throwIfNotFound: true);
        m_Interacting_EndInteraction = m_Interacting.FindAction("EndInteraction", throwIfNotFound: true);
        m_Interacting_Interact = m_Interacting.FindAction("Interact", throwIfNotFound: true);
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
    private readonly InputAction m_Player_WeaponChangeLeft;
    private readonly InputAction m_Player_WeaponChangeRight;
    private readonly InputAction m_Player_HealthButton;
    private readonly InputAction m_Player_ManaButton;
    private readonly InputAction m_Player_StaminaButton;
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
        public InputAction @WeaponChangeLeft => m_Wrapper.m_Player_WeaponChangeLeft;
        public InputAction @WeaponChangeRight => m_Wrapper.m_Player_WeaponChangeRight;
        public InputAction @HealthButton => m_Wrapper.m_Player_HealthButton;
        public InputAction @ManaButton => m_Wrapper.m_Player_ManaButton;
        public InputAction @StaminaButton => m_Wrapper.m_Player_StaminaButton;
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
                @WeaponChangeLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChangeLeft;
                @WeaponChangeLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChangeLeft;
                @WeaponChangeLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChangeLeft;
                @WeaponChangeRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChangeRight;
                @WeaponChangeRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChangeRight;
                @WeaponChangeRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponChangeRight;
                @HealthButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealthButton;
                @HealthButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealthButton;
                @HealthButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealthButton;
                @ManaButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnManaButton;
                @ManaButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnManaButton;
                @ManaButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnManaButton;
                @StaminaButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStaminaButton;
                @StaminaButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStaminaButton;
                @StaminaButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStaminaButton;
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
                @WeaponChangeLeft.started += instance.OnWeaponChangeLeft;
                @WeaponChangeLeft.performed += instance.OnWeaponChangeLeft;
                @WeaponChangeLeft.canceled += instance.OnWeaponChangeLeft;
                @WeaponChangeRight.started += instance.OnWeaponChangeRight;
                @WeaponChangeRight.performed += instance.OnWeaponChangeRight;
                @WeaponChangeRight.canceled += instance.OnWeaponChangeRight;
                @HealthButton.started += instance.OnHealthButton;
                @HealthButton.performed += instance.OnHealthButton;
                @HealthButton.canceled += instance.OnHealthButton;
                @ManaButton.started += instance.OnManaButton;
                @ManaButton.performed += instance.OnManaButton;
                @ManaButton.canceled += instance.OnManaButton;
                @StaminaButton.started += instance.OnStaminaButton;
                @StaminaButton.performed += instance.OnStaminaButton;
                @StaminaButton.canceled += instance.OnStaminaButton;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Paused
    private readonly InputActionMap m_Paused;
    private IPausedActions m_PausedActionsCallbackInterface;
    private readonly InputAction m_Paused_MenuButton;
    public struct PausedActions
    {
        private @InputController m_Wrapper;
        public PausedActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MenuButton => m_Wrapper.m_Paused_MenuButton;
        public InputActionMap Get() { return m_Wrapper.m_Paused; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PausedActions set) { return set.Get(); }
        public void SetCallbacks(IPausedActions instance)
        {
            if (m_Wrapper.m_PausedActionsCallbackInterface != null)
            {
                @MenuButton.started -= m_Wrapper.m_PausedActionsCallbackInterface.OnMenuButton;
                @MenuButton.performed -= m_Wrapper.m_PausedActionsCallbackInterface.OnMenuButton;
                @MenuButton.canceled -= m_Wrapper.m_PausedActionsCallbackInterface.OnMenuButton;
            }
            m_Wrapper.m_PausedActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MenuButton.started += instance.OnMenuButton;
                @MenuButton.performed += instance.OnMenuButton;
                @MenuButton.canceled += instance.OnMenuButton;
            }
        }
    }
    public PausedActions @Paused => new PausedActions(this);

    // Interacting
    private readonly InputActionMap m_Interacting;
    private IInteractingActions m_InteractingActionsCallbackInterface;
    private readonly InputAction m_Interacting_EndInteraction;
    private readonly InputAction m_Interacting_Interact;
    public struct InteractingActions
    {
        private @InputController m_Wrapper;
        public InteractingActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @EndInteraction => m_Wrapper.m_Interacting_EndInteraction;
        public InputAction @Interact => m_Wrapper.m_Interacting_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Interacting; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractingActions set) { return set.Get(); }
        public void SetCallbacks(IInteractingActions instance)
        {
            if (m_Wrapper.m_InteractingActionsCallbackInterface != null)
            {
                @EndInteraction.started -= m_Wrapper.m_InteractingActionsCallbackInterface.OnEndInteraction;
                @EndInteraction.performed -= m_Wrapper.m_InteractingActionsCallbackInterface.OnEndInteraction;
                @EndInteraction.canceled -= m_Wrapper.m_InteractingActionsCallbackInterface.OnEndInteraction;
                @Interact.started -= m_Wrapper.m_InteractingActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_InteractingActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_InteractingActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_InteractingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EndInteraction.started += instance.OnEndInteraction;
                @EndInteraction.performed += instance.OnEndInteraction;
                @EndInteraction.canceled += instance.OnEndInteraction;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public InteractingActions @Interacting => new InteractingActions(this);
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
        void OnWeaponChangeLeft(InputAction.CallbackContext context);
        void OnWeaponChangeRight(InputAction.CallbackContext context);
        void OnHealthButton(InputAction.CallbackContext context);
        void OnManaButton(InputAction.CallbackContext context);
        void OnStaminaButton(InputAction.CallbackContext context);
    }
    public interface IPausedActions
    {
        void OnMenuButton(InputAction.CallbackContext context);
    }
    public interface IInteractingActions
    {
        void OnEndInteraction(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
