// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Systems/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""abb565b5-0a63-40ec-87bc-7b9532c2ef1e"",
            ""actions"": [
                {
                    ""name"": ""ToggleInventory"",
                    ""type"": ""Button"",
                    ""id"": ""146a496c-c427-4e85-83dd-9415c7dc457a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectItem"",
                    ""type"": ""Button"",
                    ""id"": ""92a9ec98-9656-4613-9f77-331bbfda4c71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""2d64996c-e36b-47e3-a1d7-b35a3966753c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HotBarScroll"",
                    ""type"": ""Value"",
                    ""id"": ""6895775d-2b7c-43b5-9fd9-97e20f64832c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleConsole"",
                    ""type"": ""Button"",
                    ""id"": ""f404d028-4bf1-44ef-8df6-49b941380196"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""ec6e009d-266e-4691-9501-7f025760e5ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ebfcb152-b74d-4ea8-8f60-503b424a7776"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ToggleInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dfe20a6-a97d-4879-90e5-e37cf510114f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f7d1a29-afa4-4e29-b55e-61e8cc38a87f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90c19f3a-a4b2-48bf-8fae-e6b17d5b6899"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HotBarScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""688c1fa7-13d3-408d-b3f0-839d0a09e5b1"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ToggleConsole"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bd71583-1783-44b0-93e1-ebfd78e737e3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Movement"",
            ""id"": ""db86c3c3-b1d9-41fe-ba75-9b75c89345be"",
            ""actions"": [
                {
                    ""name"": ""MoveHorizontaly"",
                    ""type"": ""Value"",
                    ""id"": ""06ee7a22-6041-41a7-a049-a54f61f81271"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IncreaseSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""7dd3712a-f9bc-4e37-9a27-1f9add39e570"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveVerticly"",
                    ""type"": ""Value"",
                    ""id"": ""c693f134-ec57-4392-9755-f32dab42fd9b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3f678e02-a9fe-45f0-80f3-2d5ce7cd2cff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""18ccf4ae-49ef-4350-9fd3-b8f9becfdea0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e75b10dc-d3eb-4753-a4d6-87c1f669ac9d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2aefee1b-ad54-42bd-9f33-10be7dd82215"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6c604e26-74e5-4c99-b0de-2fd86c5f3138"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e370845b-7aa1-4adc-9d33-e94181515b9c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb77d8eb-aaa0-4a98-b0ae-0b22fc0f27d1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e973d5da-4b85-4995-bb58-f656fa59caa9"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""IncreaseSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b11b7c1f-a20b-4e81-b34f-725d93ef3109"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""IncreaseSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""25c8078e-2c27-4b21-afff-e70477909f88"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVerticly"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""aca8ad7f-1f88-4c4a-a471-5edc9cabbf29"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveVerticly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c93376f3-426a-41a4-96a9-b9056df35692"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveVerticly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a31e7945-83e7-4f20-bdc3-ceb4d53d118c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""61833e12-715e-4aff-abe8-c5c9b2fe172d"",
            ""actions"": [
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Button"",
                    ""id"": ""fba6f3be-544c-4511-b72d-0f05381f31db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartRotating"",
                    ""type"": ""Button"",
                    ""id"": ""da9b422d-2496-4763-aa44-9dce30fd499b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""997e6cc3-9030-4b20-a4ce-9af40d67c68d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fec333c-1d8c-400e-94ac-b3c47f0a2486"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""StartRotating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""4ce555a2-4e6f-45f6-a0eb-cb64da7d2254"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""1345d0d2-38c7-44c5-9e05-448aaeba7b9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2e14083b-ae2c-44f2-a3df-9e43b600dcfe"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""16832413-d93a-4b24-8daa-48fe5f1ed582"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""550e1a0d-0e23-4a7c-a5a3-4e398a3bcdd1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""1d95db8f-fe0a-4018-bc70-6fe0a5e3006e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""928d50d7-3ef3-494b-b9c6-884f42080e73"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f55a63c5-3b38-4d77-b2a4-ded25a6518d5"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RotateBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_ToggleInventory = m_UI.FindAction("ToggleInventory", throwIfNotFound: true);
        m_UI_SelectItem = m_UI.FindAction("SelectItem", throwIfNotFound: true);
        m_UI_DropItem = m_UI.FindAction("DropItem", throwIfNotFound: true);
        m_UI_HotBarScroll = m_UI.FindAction("HotBarScroll", throwIfNotFound: true);
        m_UI_ToggleConsole = m_UI.FindAction("ToggleConsole", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_MoveHorizontaly = m_Movement.FindAction("MoveHorizontaly", throwIfNotFound: true);
        m_Movement_IncreaseSpeed = m_Movement.FindAction("IncreaseSpeed", throwIfNotFound: true);
        m_Movement_MoveVerticly = m_Movement.FindAction("MoveVerticly", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MouseDelta = m_Camera.FindAction("MouseDelta", throwIfNotFound: true);
        m_Camera_StartRotating = m_Camera.FindAction("StartRotating", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Attack = m_Combat.FindAction("Attack", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_Interact = m_Interaction.FindAction("Interact", throwIfNotFound: true);
        m_Interaction_RotateBuilding = m_Interaction.FindAction("RotateBuilding", throwIfNotFound: true);
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

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_ToggleInventory;
    private readonly InputAction m_UI_SelectItem;
    private readonly InputAction m_UI_DropItem;
    private readonly InputAction m_UI_HotBarScroll;
    private readonly InputAction m_UI_ToggleConsole;
    private readonly InputAction m_UI_Cancel;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleInventory => m_Wrapper.m_UI_ToggleInventory;
        public InputAction @SelectItem => m_Wrapper.m_UI_SelectItem;
        public InputAction @DropItem => m_Wrapper.m_UI_DropItem;
        public InputAction @HotBarScroll => m_Wrapper.m_UI_HotBarScroll;
        public InputAction @ToggleConsole => m_Wrapper.m_UI_ToggleConsole;
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @ToggleInventory.started -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleInventory;
                @SelectItem.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelectItem;
                @SelectItem.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelectItem;
                @SelectItem.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelectItem;
                @DropItem.started -= m_Wrapper.m_UIActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnDropItem;
                @HotBarScroll.started -= m_Wrapper.m_UIActionsCallbackInterface.OnHotBarScroll;
                @HotBarScroll.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnHotBarScroll;
                @HotBarScroll.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnHotBarScroll;
                @ToggleConsole.started -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleConsole;
                @ToggleConsole.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleConsole;
                @ToggleConsole.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleConsole;
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleInventory.started += instance.OnToggleInventory;
                @ToggleInventory.performed += instance.OnToggleInventory;
                @ToggleInventory.canceled += instance.OnToggleInventory;
                @SelectItem.started += instance.OnSelectItem;
                @SelectItem.performed += instance.OnSelectItem;
                @SelectItem.canceled += instance.OnSelectItem;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
                @HotBarScroll.started += instance.OnHotBarScroll;
                @HotBarScroll.performed += instance.OnHotBarScroll;
                @HotBarScroll.canceled += instance.OnHotBarScroll;
                @ToggleConsole.started += instance.OnToggleConsole;
                @ToggleConsole.performed += instance.OnToggleConsole;
                @ToggleConsole.canceled += instance.OnToggleConsole;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_MoveHorizontaly;
    private readonly InputAction m_Movement_IncreaseSpeed;
    private readonly InputAction m_Movement_MoveVerticly;
    private readonly InputAction m_Movement_Jump;
    public struct MovementActions
    {
        private @PlayerInput m_Wrapper;
        public MovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveHorizontaly => m_Wrapper.m_Movement_MoveHorizontaly;
        public InputAction @IncreaseSpeed => m_Wrapper.m_Movement_IncreaseSpeed;
        public InputAction @MoveVerticly => m_Wrapper.m_Movement_MoveVerticly;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @MoveHorizontaly.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveHorizontaly;
                @MoveHorizontaly.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveHorizontaly;
                @MoveHorizontaly.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveHorizontaly;
                @IncreaseSpeed.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnIncreaseSpeed;
                @IncreaseSpeed.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnIncreaseSpeed;
                @IncreaseSpeed.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnIncreaseSpeed;
                @MoveVerticly.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVerticly;
                @MoveVerticly.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVerticly;
                @MoveVerticly.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVerticly;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveHorizontaly.started += instance.OnMoveHorizontaly;
                @MoveHorizontaly.performed += instance.OnMoveHorizontaly;
                @MoveHorizontaly.canceled += instance.OnMoveHorizontaly;
                @IncreaseSpeed.started += instance.OnIncreaseSpeed;
                @IncreaseSpeed.performed += instance.OnIncreaseSpeed;
                @IncreaseSpeed.canceled += instance.OnIncreaseSpeed;
                @MoveVerticly.started += instance.OnMoveVerticly;
                @MoveVerticly.performed += instance.OnMoveVerticly;
                @MoveVerticly.canceled += instance.OnMoveVerticly;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MouseDelta;
    private readonly InputAction m_Camera_StartRotating;
    public struct CameraActions
    {
        private @PlayerInput m_Wrapper;
        public CameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseDelta => m_Wrapper.m_Camera_MouseDelta;
        public InputAction @StartRotating => m_Wrapper.m_Camera_StartRotating;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @MouseDelta.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseDelta;
                @StartRotating.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartRotating;
                @StartRotating.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartRotating;
                @StartRotating.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartRotating;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseDelta.started += instance.OnMouseDelta;
                @MouseDelta.performed += instance.OnMouseDelta;
                @MouseDelta.canceled += instance.OnMouseDelta;
                @StartRotating.started += instance.OnStartRotating;
                @StartRotating.performed += instance.OnStartRotating;
                @StartRotating.canceled += instance.OnStartRotating;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_Attack;
    public struct CombatActions
    {
        private @PlayerInput m_Wrapper;
        public CombatActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Combat_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_Interact;
    private readonly InputAction m_Interaction_RotateBuilding;
    public struct InteractionActions
    {
        private @PlayerInput m_Wrapper;
        public InteractionActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Interaction_Interact;
        public InputAction @RotateBuilding => m_Wrapper.m_Interaction_RotateBuilding;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @RotateBuilding.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnRotateBuilding;
                @RotateBuilding.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnRotateBuilding;
                @RotateBuilding.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnRotateBuilding;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @RotateBuilding.started += instance.OnRotateBuilding;
                @RotateBuilding.performed += instance.OnRotateBuilding;
                @RotateBuilding.canceled += instance.OnRotateBuilding;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IUIActions
    {
        void OnToggleInventory(InputAction.CallbackContext context);
        void OnSelectItem(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
        void OnHotBarScroll(InputAction.CallbackContext context);
        void OnToggleConsole(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IMovementActions
    {
        void OnMoveHorizontaly(InputAction.CallbackContext context);
        void OnIncreaseSpeed(InputAction.CallbackContext context);
        void OnMoveVerticly(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnStartRotating(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnAttack(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnRotateBuilding(InputAction.CallbackContext context);
    }
}
