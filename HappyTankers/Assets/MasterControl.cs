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
                    ""name"": ""LeftTrack"",
                    ""type"": ""Button"",
                    ""id"": ""157d32a1-b197-41b7-852d-5aaf4ce77330"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightTrack"",
                    ""type"": ""Button"",
                    ""id"": ""d8e754b2-19b5-4cce-b182-3db830fc003d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
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
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""a3656a2c-5959-4885-a476-49a388659fd2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftTrack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""29953dc1-2343-4278-ba25-6b070807a889"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""LeftTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a2bd1100-2bb7-4499-a573-c787ce059a2d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""LeftTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""XBox"",
                    ""id"": ""9f9e8ded-f58b-4e1b-aadb-ea45a023f4b8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftTrack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""64492e87-fe43-4ceb-9f34-aa20879163e2"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";XBox"",
                    ""action"": ""LeftTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cf90a3f6-1264-4ffb-85ff-092867efa5c3"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";XBox"",
                    ""action"": ""LeftTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""3279c3f5-31c3-4c57-b146-69e470fb05e3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTrack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""775eab21-a155-47de-889a-ad97d16ba0fe"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b9c1d8d3-a6f7-413b-b0e0-f670744de3ff"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""XBox"",
                    ""id"": ""6229478e-7f9b-49c8-a87a-ab92becd87a4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTrack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9fe55a79-4116-4f83-8b19-395c61531af9"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";XBox"",
                    ""action"": ""RightTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5339d15f-30f0-4eaf-a51b-adaec6d5adbb"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";XBox"",
                    ""action"": ""RightTrack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
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
        m_Player_LeftTrack = m_Player.GetAction("LeftTrack");
        m_Player_RightTrack = m_Player.GetAction("RightTrack");
        m_Player_RotateTurret = m_Player.GetAction("RotateTurret");
        m_Player_XBoxRotateTurret = m_Player.GetAction("XBoxRotateTurret");
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
    private readonly InputAction m_Player_LeftTrack;
    private readonly InputAction m_Player_RightTrack;
    private readonly InputAction m_Player_RotateTurret;
    private readonly InputAction m_Player_XBoxRotateTurret;
    public struct PlayerActions
    {
        private MasterControl m_Wrapper;
        public PlayerActions(MasterControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftTrack => m_Wrapper.m_Player_LeftTrack;
        public InputAction @RightTrack => m_Wrapper.m_Player_RightTrack;
        public InputAction @RotateTurret => m_Wrapper.m_Player_RotateTurret;
        public InputAction @XBoxRotateTurret => m_Wrapper.m_Player_XBoxRotateTurret;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                LeftTrack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftTrack;
                LeftTrack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftTrack;
                LeftTrack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftTrack;
                RightTrack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightTrack;
                RightTrack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightTrack;
                RightTrack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightTrack;
                RotateTurret.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateTurret;
                RotateTurret.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateTurret;
                RotateTurret.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateTurret;
                XBoxRotateTurret.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXBoxRotateTurret;
                XBoxRotateTurret.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXBoxRotateTurret;
                XBoxRotateTurret.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXBoxRotateTurret;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                LeftTrack.started += instance.OnLeftTrack;
                LeftTrack.performed += instance.OnLeftTrack;
                LeftTrack.canceled += instance.OnLeftTrack;
                RightTrack.started += instance.OnRightTrack;
                RightTrack.performed += instance.OnRightTrack;
                RightTrack.canceled += instance.OnRightTrack;
                RotateTurret.started += instance.OnRotateTurret;
                RotateTurret.performed += instance.OnRotateTurret;
                RotateTurret.canceled += instance.OnRotateTurret;
                XBoxRotateTurret.started += instance.OnXBoxRotateTurret;
                XBoxRotateTurret.performed += instance.OnXBoxRotateTurret;
                XBoxRotateTurret.canceled += instance.OnXBoxRotateTurret;
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
        void OnLeftTrack(InputAction.CallbackContext context);
        void OnRightTrack(InputAction.CallbackContext context);
        void OnRotateTurret(InputAction.CallbackContext context);
        void OnXBoxRotateTurret(InputAction.CallbackContext context);
    }
}
