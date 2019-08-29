// GENERATED AUTOMATICALLY FROM 'Assets/MasterControl.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class MasterControl : IInputActionCollection
{
    private InputActionAsset asset;
    public MasterControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MasterControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3f14c98b-03a9-4542-85d0-a5ab3f470dd4"",
            ""actions"": [
                {
                    ""name"": ""RotateTurret"",
                    ""type"": ""Value"",
                    ""id"": ""92fe82ba-4ed3-41fc-b15c-1c3a1916990b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""XBoxRotateTurret"",
                    ""type"": ""Button"",
                    ""id"": ""1655cb6d-34bb-4aa9-91ec-70637221ad76"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""42f4e971-fcc5-4be8-b65e-ef3a98e1b2b5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shield"",
                    ""type"": ""Button"",
                    ""id"": ""294743f8-5418-40cc-99cf-e60fb4d2323a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireStrong"",
                    ""type"": ""Button"",
                    ""id"": ""08bcd499-37d7-4659-bc53-c0fdf3769e16"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovementAxis"",
                    ""type"": ""Button"",
                    ""id"": ""b1593a26-19cf-4165-bd98-17460a994571"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""116ce69d-149b-4e94-b43d-42a3f09ba672"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RotateTurret"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Xbox"",
                    ""id"": ""6e690df7-1603-4896-8d30-9f4dcf96b778"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XBoxRotateTurret"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a87bd442-a50d-4400-a534-18380dd8d491"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XBoxRotateTurret"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a0956128-56c9-454c-8801-410ad9b220b0"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XBoxRotateTurret"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""597a5843-48ba-4563-af85-9d3d55cc9e09"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XBoxRotateTurret"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0425d4d7-6bdb-4e92-ac4d-72ba4c674703"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XBoxRotateTurret"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8ad7e5fa-17e2-46ea-bc82-4eec9137c2a7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc2b1b0c-87a0-4dc8-9150-4146e1b08891"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";XBox"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee167cd2-0fe0-4dfc-a672-38ee98e148ea"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Shield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed70f836-e997-4c5d-9a77-08bc41b2ee3a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""FireStrong"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f11954a0-e57e-4a29-a6fb-8d98f482e586"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f029994d-6e1a-43ee-8e2e-f889fdedf0b8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""224084f2-3225-420a-bbd4-8ab17dfaca50"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1932f81b-efe3-4e9c-874e-6e8564fbff3c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6e2be21f-8f6b-48a3-a7ba-80466ff24f09"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""basedOn"": """",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XBox"",
            ""basedOn"": """",
            ""bindingGroup"": ""XBox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_RotateTurret = m_Player.GetAction("RotateTurret");
        m_Player_XBoxRotateTurret = m_Player.GetAction("XBoxRotateTurret");
        m_Player_Fire = m_Player.GetAction("Fire");
        m_Player_Shield = m_Player.GetAction("Shield");
        m_Player_FireStrong = m_Player.GetAction("FireStrong");
        m_Player_MovementAxis = m_Player.GetAction("MovementAxis");
    }

    ~MasterControl()
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
    private readonly InputAction m_Player_RotateTurret;
    private readonly InputAction m_Player_XBoxRotateTurret;
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_Shield;
    private readonly InputAction m_Player_FireStrong;
    private readonly InputAction m_Player_MovementAxis;
    public struct PlayerActions
    {
        private MasterControl m_Wrapper;
        public PlayerActions(MasterControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotateTurret => m_Wrapper.m_Player_RotateTurret;
        public InputAction @XBoxRotateTurret => m_Wrapper.m_Player_XBoxRotateTurret;
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @Shield => m_Wrapper.m_Player_Shield;
        public InputAction @FireStrong => m_Wrapper.m_Player_FireStrong;
        public InputAction @MovementAxis => m_Wrapper.m_Player_MovementAxis;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                RotateTurret.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateTurret;
                RotateTurret.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateTurret;
                RotateTurret.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateTurret;
                XBoxRotateTurret.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXBoxRotateTurret;
                XBoxRotateTurret.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXBoxRotateTurret;
                XBoxRotateTurret.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXBoxRotateTurret;
                Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                Shield.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShield;
                Shield.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShield;
                Shield.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShield;
                FireStrong.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireStrong;
                FireStrong.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireStrong;
                FireStrong.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireStrong;
                MovementAxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementAxis;
                MovementAxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementAxis;
                MovementAxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementAxis;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                RotateTurret.started += instance.OnRotateTurret;
                RotateTurret.performed += instance.OnRotateTurret;
                RotateTurret.canceled += instance.OnRotateTurret;
                XBoxRotateTurret.started += instance.OnXBoxRotateTurret;
                XBoxRotateTurret.performed += instance.OnXBoxRotateTurret;
                XBoxRotateTurret.canceled += instance.OnXBoxRotateTurret;
                Fire.started += instance.OnFire;
                Fire.performed += instance.OnFire;
                Fire.canceled += instance.OnFire;
                Shield.started += instance.OnShield;
                Shield.performed += instance.OnShield;
                Shield.canceled += instance.OnShield;
                FireStrong.started += instance.OnFireStrong;
                FireStrong.performed += instance.OnFireStrong;
                FireStrong.canceled += instance.OnFireStrong;
                MovementAxis.started += instance.OnMovementAxis;
                MovementAxis.performed += instance.OnMovementAxis;
                MovementAxis.canceled += instance.OnMovementAxis;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_XBoxSchemeIndex = -1;
    public InputControlScheme XBoxScheme
    {
        get
        {
            if (m_XBoxSchemeIndex == -1) m_XBoxSchemeIndex = asset.GetControlSchemeIndex("XBox");
            return asset.controlSchemes[m_XBoxSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnRotateTurret(InputAction.CallbackContext context);
        void OnXBoxRotateTurret(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnShield(InputAction.CallbackContext context);
        void OnFireStrong(InputAction.CallbackContext context);
        void OnMovementAxis(InputAction.CallbackContext context);
    }
}
