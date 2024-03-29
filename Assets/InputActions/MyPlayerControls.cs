//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputActions/MyPlayerControls.inputactions
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

public partial class @MyPlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MyPlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyPlayerControls"",
    ""maps"": [
        {
            ""name"": ""Infantry"",
            ""id"": ""eeac4c6c-f71a-4dc6-8641-b4b14e925094"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bdf3e7fd-0c6e-4a73-bab2-7e120186b4c4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""4c67c1b4-b2cb-41fe-9cf1-f456cfbad73b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Binoculars"",
                    ""type"": ""Button"",
                    ""id"": ""e4e63fb9-52fe-4806-916d-f5c3c318160c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""758c77d3-ba4a-4e1f-9288-1c3ee9138888"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""6696d9d2-97c4-4d7c-9a3b-40665056869e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""1ef98581-d816-4637-9c03-2921b820883e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""68eed999-4679-4bb5-ad0b-517153b98572"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Binoculars"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""90d86168-d416-4a77-afa2-4ef7841533dd"",
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
                    ""id"": ""ab100986-3837-4431-b8e5-63863e3ec624"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""72a2bce5-3000-4645-b324-ae97e29c2323"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4cf3445c-1af5-4610-b53e-33023304abfa"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""79266288-10cf-414e-8288-38a859be748a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c67f9abf-d6ea-4830-9d7d-e360289a1d43"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e32395fc-ea59-4bfd-bc4a-3deea3e5f541"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1b1f704-5638-4973-9262-da8e846ca2d9"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f56e0792-92de-417a-9efb-b5195563f5a2"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""8bebd4f2-2410-454a-bdf0-aa72560e1062"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0002cfda-b19d-480e-a51e-c352d6587068"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChangeSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""8d2d2349-37f1-4056-ab09-95cc06f2a150"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeZoomLevel"",
                    ""type"": ""Value"",
                    ""id"": ""c3b88b7f-e3ff-415c-a2a3-39d223deacb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fa7173ee-d4b2-4fcd-b837-3b77220fcb1b"",
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
                    ""id"": ""af3f54ae-76de-46b1-bcda-cad9efbc06e8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a99d2283-86c9-4d13-ad57-81dedc19482b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e029a4d0-5daf-48a0-86dd-164005ca8db3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4078f260-26f6-4d18-9607-056fdb0290f7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e7c6353c-f4eb-42c4-9d65-4207bd342da2"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeZoomLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1d24731-34a8-4c58-a281-81418adc0162"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Vehicle"",
            ""id"": ""4888aaa0-bd8a-4397-909a-46b511b0ae19"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""19634098-1a8e-4fea-b0ba-396615cbe70e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3473748d-e2dc-4c78-93c9-fe57f265a6f8"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Infantry
        m_Infantry = asset.FindActionMap("Infantry", throwIfNotFound: true);
        m_Infantry_Move = m_Infantry.FindAction("Move", throwIfNotFound: true);
        m_Infantry_Run = m_Infantry.FindAction("Run", throwIfNotFound: true);
        m_Infantry_Binoculars = m_Infantry.FindAction("Binoculars", throwIfNotFound: true);
        m_Infantry_Map = m_Infantry.FindAction("Map", throwIfNotFound: true);
        m_Infantry_Fire = m_Infantry.FindAction("Fire", throwIfNotFound: true);
        m_Infantry_Reload = m_Infantry.FindAction("Reload", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Move = m_Camera.FindAction("Move", throwIfNotFound: true);
        m_Camera_ChangeSpeed = m_Camera.FindAction("ChangeSpeed", throwIfNotFound: true);
        m_Camera_ChangeZoomLevel = m_Camera.FindAction("ChangeZoomLevel", throwIfNotFound: true);
        // Vehicle
        m_Vehicle = asset.FindActionMap("Vehicle", throwIfNotFound: true);
        m_Vehicle_Newaction = m_Vehicle.FindAction("New action", throwIfNotFound: true);
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

    // Infantry
    private readonly InputActionMap m_Infantry;
    private IInfantryActions m_InfantryActionsCallbackInterface;
    private readonly InputAction m_Infantry_Move;
    private readonly InputAction m_Infantry_Run;
    private readonly InputAction m_Infantry_Binoculars;
    private readonly InputAction m_Infantry_Map;
    private readonly InputAction m_Infantry_Fire;
    private readonly InputAction m_Infantry_Reload;
    public struct InfantryActions
    {
        private @MyPlayerControls m_Wrapper;
        public InfantryActions(@MyPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Infantry_Move;
        public InputAction @Run => m_Wrapper.m_Infantry_Run;
        public InputAction @Binoculars => m_Wrapper.m_Infantry_Binoculars;
        public InputAction @Map => m_Wrapper.m_Infantry_Map;
        public InputAction @Fire => m_Wrapper.m_Infantry_Fire;
        public InputAction @Reload => m_Wrapper.m_Infantry_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Infantry; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InfantryActions set) { return set.Get(); }
        public void SetCallbacks(IInfantryActions instance)
        {
            if (m_Wrapper.m_InfantryActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_InfantryActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InfantryActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InfantryActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_InfantryActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_InfantryActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_InfantryActionsCallbackInterface.OnRun;
                @Binoculars.started -= m_Wrapper.m_InfantryActionsCallbackInterface.OnBinoculars;
                @Binoculars.performed -= m_Wrapper.m_InfantryActionsCallbackInterface.OnBinoculars;
                @Binoculars.canceled -= m_Wrapper.m_InfantryActionsCallbackInterface.OnBinoculars;
                @Map.started -= m_Wrapper.m_InfantryActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_InfantryActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_InfantryActionsCallbackInterface.OnMap;
                @Fire.started -= m_Wrapper.m_InfantryActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_InfantryActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_InfantryActionsCallbackInterface.OnFire;
                @Reload.started -= m_Wrapper.m_InfantryActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_InfantryActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_InfantryActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_InfantryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Binoculars.started += instance.OnBinoculars;
                @Binoculars.performed += instance.OnBinoculars;
                @Binoculars.canceled += instance.OnBinoculars;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public InfantryActions @Infantry => new InfantryActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Move;
    private readonly InputAction m_Camera_ChangeSpeed;
    private readonly InputAction m_Camera_ChangeZoomLevel;
    public struct CameraActions
    {
        private @MyPlayerControls m_Wrapper;
        public CameraActions(@MyPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Camera_Move;
        public InputAction @ChangeSpeed => m_Wrapper.m_Camera_ChangeSpeed;
        public InputAction @ChangeZoomLevel => m_Wrapper.m_Camera_ChangeZoomLevel;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @ChangeSpeed.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnChangeSpeed;
                @ChangeSpeed.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnChangeSpeed;
                @ChangeSpeed.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnChangeSpeed;
                @ChangeZoomLevel.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnChangeZoomLevel;
                @ChangeZoomLevel.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnChangeZoomLevel;
                @ChangeZoomLevel.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnChangeZoomLevel;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ChangeSpeed.started += instance.OnChangeSpeed;
                @ChangeSpeed.performed += instance.OnChangeSpeed;
                @ChangeSpeed.canceled += instance.OnChangeSpeed;
                @ChangeZoomLevel.started += instance.OnChangeZoomLevel;
                @ChangeZoomLevel.performed += instance.OnChangeZoomLevel;
                @ChangeZoomLevel.canceled += instance.OnChangeZoomLevel;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Vehicle
    private readonly InputActionMap m_Vehicle;
    private IVehicleActions m_VehicleActionsCallbackInterface;
    private readonly InputAction m_Vehicle_Newaction;
    public struct VehicleActions
    {
        private @MyPlayerControls m_Wrapper;
        public VehicleActions(@MyPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Vehicle_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Vehicle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(VehicleActions set) { return set.Get(); }
        public void SetCallbacks(IVehicleActions instance)
        {
            if (m_Wrapper.m_VehicleActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_VehicleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public VehicleActions @Vehicle => new VehicleActions(this);
    public interface IInfantryActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnBinoculars(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnChangeSpeed(InputAction.CallbackContext context);
        void OnChangeZoomLevel(InputAction.CallbackContext context);
    }
    public interface IVehicleActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
