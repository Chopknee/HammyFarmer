// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Hammy
{
    public class InputMaster : IInputActionCollection, IDisposable
    {
        private InputActionAsset asset;
        public InputMaster()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Hammy"",
            ""id"": ""db08c7bc-298c-4f7c-9524-47f1831d7bf3"",
            ""actions"": [
                {
                    ""name"": ""Roll"",
                    ""type"": ""Value"",
                    ""id"": ""4591b72e-a293-4b2f-9fd5-6f7803e41dcd"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attach"",
                    ""type"": ""Button"",
                    ""id"": ""d49b1e48-8949-4dd8-9682-d8126daf1c62"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""1080663e-b8d3-4215-9bcf-4bb1c67c7204"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c41b2263-68e3-4ca5-81d1-af6228d463aa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""80a7c57c-9c04-408e-8ca7-027da0c5772b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""7f8f5120-e9ca-4cf7-be4a-ce84a6ef5d12"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraVertical"",
                    ""type"": ""Value"",
                    ""id"": ""0c5fa063-5e58-43a9-9eaf-29d8424d0dd1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraHorizontal"",
                    ""type"": ""Value"",
                    ""id"": ""bfb33f42-2c7c-45cc-9a86-dbf4ff4a884d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8e71d797-44a5-49a9-81d1-88bda073cbfd"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Attach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09abaf39-796f-43ad-bd89-f54cdb557c84"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ad98ccf1-15b8-4a38-87a0-7ee1acb16fba"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""59bf1bec-f731-429a-81af-74acb5000985"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5a814b29-88fd-49ff-90f6-2fdc3e67e2db"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""08a89da0-4dc5-4ee3-a301-f6b1a1163664"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b2da385a-f0ec-4fa5-9097-65e7f0d8d66b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""50a4f7ff-6370-4de3-ac87-e2b7ebb3f2bc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ae5e205-439c-40cf-b293-cb7b27330033"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97003b19-7015-455d-aae6-7ec67ac7b628"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fb783ac-26e7-4221-886e-2129afadfc4d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd587ef4-dcd4-44a7-9df0-10d417425212"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffcde620-e1ff-4dd5-b5d9-e56dd3dfee0f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbcb6b71-a1e8-42fa-89b5-5eb2bc9c2333"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8010ba55-e0f3-430d-99d8-74c822064c78"",
                    ""path"": ""<XInputController>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=20),Invert"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf229df5-378a-4722-8811-bcb9201c703d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8671d0fc-5fcd-4c32-9c25-9cb18bd6be0f"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=20)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""941074cd-52b5-4655-a2e7-ddef8de4f937"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcf4861c-7777-4df5-af53-7bc436ff4d5c"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=20)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23175f89-04cf-4b3d-ac25-43ab33b14ce4"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Hammy
            m_Hammy = asset.FindActionMap("Hammy", throwIfNotFound: true);
            m_Hammy_Roll = m_Hammy.FindAction("Roll", throwIfNotFound: true);
            m_Hammy_Attach = m_Hammy.FindAction("Attach", throwIfNotFound: true);
            m_Hammy_Use = m_Hammy.FindAction("Use", throwIfNotFound: true);
            m_Hammy_Jump = m_Hammy.FindAction("Jump", throwIfNotFound: true);
            m_Hammy_Pause = m_Hammy.FindAction("Pause", throwIfNotFound: true);
            m_Hammy_CameraZoom = m_Hammy.FindAction("CameraZoom", throwIfNotFound: true);
            m_Hammy_CameraVertical = m_Hammy.FindAction("CameraVertical", throwIfNotFound: true);
            m_Hammy_CameraHorizontal = m_Hammy.FindAction("CameraHorizontal", throwIfNotFound: true);
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

        // Hammy
        private readonly InputActionMap m_Hammy;
        private IHammyActions m_HammyActionsCallbackInterface;
        private readonly InputAction m_Hammy_Roll;
        private readonly InputAction m_Hammy_Attach;
        private readonly InputAction m_Hammy_Use;
        private readonly InputAction m_Hammy_Jump;
        private readonly InputAction m_Hammy_Pause;
        private readonly InputAction m_Hammy_CameraZoom;
        private readonly InputAction m_Hammy_CameraVertical;
        private readonly InputAction m_Hammy_CameraHorizontal;
        public struct HammyActions
        {
            private InputMaster m_Wrapper;
            public HammyActions(InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Roll => m_Wrapper.m_Hammy_Roll;
            public InputAction @Attach => m_Wrapper.m_Hammy_Attach;
            public InputAction @Use => m_Wrapper.m_Hammy_Use;
            public InputAction @Jump => m_Wrapper.m_Hammy_Jump;
            public InputAction @Pause => m_Wrapper.m_Hammy_Pause;
            public InputAction @CameraZoom => m_Wrapper.m_Hammy_CameraZoom;
            public InputAction @CameraVertical => m_Wrapper.m_Hammy_CameraVertical;
            public InputAction @CameraHorizontal => m_Wrapper.m_Hammy_CameraHorizontal;
            public InputActionMap Get() { return m_Wrapper.m_Hammy; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(HammyActions set) { return set.Get(); }
            public void SetCallbacks(IHammyActions instance)
            {
                if (m_Wrapper.m_HammyActionsCallbackInterface != null)
                {
                    Roll.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnRoll;
                    Roll.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnRoll;
                    Roll.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnRoll;
                    Attach.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnAttach;
                    Attach.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnAttach;
                    Attach.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnAttach;
                    Use.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnUse;
                    Use.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnUse;
                    Use.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnUse;
                    Jump.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnJump;
                    Jump.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnJump;
                    Jump.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnJump;
                    Pause.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnPause;
                    Pause.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnPause;
                    Pause.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnPause;
                    CameraZoom.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraZoom;
                    CameraZoom.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraZoom;
                    CameraZoom.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraZoom;
                    CameraVertical.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraVertical;
                    CameraVertical.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraVertical;
                    CameraVertical.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraVertical;
                    CameraHorizontal.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraHorizontal;
                    CameraHorizontal.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraHorizontal;
                    CameraHorizontal.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnCameraHorizontal;
                }
                m_Wrapper.m_HammyActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Roll.started += instance.OnRoll;
                    Roll.performed += instance.OnRoll;
                    Roll.canceled += instance.OnRoll;
                    Attach.started += instance.OnAttach;
                    Attach.performed += instance.OnAttach;
                    Attach.canceled += instance.OnAttach;
                    Use.started += instance.OnUse;
                    Use.performed += instance.OnUse;
                    Use.canceled += instance.OnUse;
                    Jump.started += instance.OnJump;
                    Jump.performed += instance.OnJump;
                    Jump.canceled += instance.OnJump;
                    Pause.started += instance.OnPause;
                    Pause.performed += instance.OnPause;
                    Pause.canceled += instance.OnPause;
                    CameraZoom.started += instance.OnCameraZoom;
                    CameraZoom.performed += instance.OnCameraZoom;
                    CameraZoom.canceled += instance.OnCameraZoom;
                    CameraVertical.started += instance.OnCameraVertical;
                    CameraVertical.performed += instance.OnCameraVertical;
                    CameraVertical.canceled += instance.OnCameraVertical;
                    CameraHorizontal.started += instance.OnCameraHorizontal;
                    CameraHorizontal.performed += instance.OnCameraHorizontal;
                    CameraHorizontal.canceled += instance.OnCameraHorizontal;
                }
            }
        }
        public HammyActions @Hammy => new HammyActions(this);
        private int m_KeyboardandMouseSchemeIndex = -1;
        public InputControlScheme KeyboardandMouseScheme
        {
            get
            {
                if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
                return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
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
        public interface IHammyActions
        {
            void OnRoll(InputAction.CallbackContext context);
            void OnAttach(InputAction.CallbackContext context);
            void OnUse(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
            void OnCameraZoom(InputAction.CallbackContext context);
            void OnCameraVertical(InputAction.CallbackContext context);
            void OnCameraHorizontal(InputAction.CallbackContext context);
        }
    }
}
