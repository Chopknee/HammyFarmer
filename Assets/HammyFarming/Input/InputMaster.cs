// GENERATED AUTOMATICALLY FROM 'Assets/HammyFarming/Input/InputMaster.inputactions'

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
                    ""name"": ""Debug"",
                    ""type"": ""Button"",
                    ""id"": ""e64bf18b-6444-42da-a683-e3f1d3551a30"",
                    ""expectedControlType"": """",
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
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea22bb49-582c-4b38-b218-e0027562fd3e"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Debug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""GamepadDebug"",
                    ""id"": ""3295c4b9-33d5-41d9-9c8c-e99c0d9f85e3"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Debug"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""2c6e7964-b6d5-4478-89c9-097b83487b3a"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Debug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""291d3205-71fc-45a8-8c87-3392cf2081d1"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Debug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""92222ec8-0561-4dfb-9a98-74b590536e6b"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""695f4134-171a-41c0-9740-68dafe1ec32a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""a9cc2f55-bbf4-4035-bb24-e05697063c98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""784fbd6e-882c-4f2b-a4f1-a8bc19e518a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f144557e-836b-4a8c-8e5f-e8cfa5955ec7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1ae59ea6-c5a3-4fa0-9742-8540c952e2cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d11a095c-78ab-4edf-a2bf-c0060bc79713"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""03d353cd-e579-471b-ba21-a58fbc3797a7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7798cd41-42dd-4f03-9cbb-4c701a425118"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f8f5ebc7-385d-451e-955e-daeedf913b17"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""984a4224-2369-48bd-a9e3-76ed5d73d140"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceSelect"",
                    ""type"": ""PassThrough"",
                    ""id"": ""810214ed-148b-4b38-ae44-7d8102e7cde9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Stick"",
                    ""id"": ""49992bf5-d0f7-4a60-a40c-f240b12b91eb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fb8a72eb-9c9e-4960-b091-ab0095d03657"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""87d1f299-816d-4633-9a37-62bca75ea4e4"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4ce97aba-d294-49a2-8676-459648fe0cb2"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""afc951c3-310a-4bb5-9d12-bd1d8fd48c25"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4368f880-ac3c-40bf-a470-77dd57e7f68b"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""d6b60727-c094-4bfd-ac16-f0dfbe0e0ed6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cd0028de-d638-492b-803b-8d2342312c3a"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""39d005e0-dda4-4cc0-a139-99592cfeca5a"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""289a67c5-3a31-47cb-8ef3-55b31b828b3a"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""31a14318-5d62-4e14-9bd9-c01ee998e29e"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7c695a22-054c-4697-8dae-653953ccb3ff"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3105e5a1-1469-47c7-9d08-7288acd0604a"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad5f572f-6ce9-4a6e-82f8-14a989ca29b2"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""332fdc5e-11d9-4b94-b516-3a13554954c7"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b359945c-3431-467e-b884-5e17f5df9c75"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""003117f4-dfe8-4351-9ca3-92e07031fc03"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5aaa015e-da5d-4031-afdf-b8afe0381d59"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7398bc0-0a91-4ceb-a210-345ba59b9e10"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7becbbcc-502a-4714-88be-5daf727a84c3"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6af86a9-6d21-495d-a626-bd842db8555f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fc2ace8-9b6c-40f3-8802-eee430cbc1c6"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87978186-85a3-4b0f-b538-14157b2f0a7a"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32ffec93-0a44-48ec-beb1-78eec569259d"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TrackedDeviceSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""InputDevice"",
            ""id"": ""efaed615-008d-45a5-b3b3-c899f41dae23"",
            ""actions"": [
                {
                    ""name"": ""KeyboardAny"",
                    ""type"": ""Button"",
                    ""id"": ""3a2542fa-f69f-41b5-91d5-4aa73955d297"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""KeyboardAnyButton"",
                    ""type"": ""Button"",
                    ""id"": ""d15a7d0b-a1f9-4a5f-98df-91b70d2e6824"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GamepadAny"",
                    ""type"": ""Button"",
                    ""id"": ""94206a9f-fe9b-4bf1-8886-2405dcc08a9f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GamepadAnyButton"",
                    ""type"": ""Button"",
                    ""id"": ""e107e19d-3616-406c-9755-ff5790df1dff"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MainMenuStart"",
                    ""type"": ""Button"",
                    ""id"": ""0a896c56-3451-4a5b-b47b-bac24c4f6e79"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b64cf2be-0520-406c-831c-812eb34daa0a"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""KeyboardAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39099175-21f3-441d-a789-398a7954030f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5c746da-b466-4903-a9c6-07466c295c7e"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""KeyboardAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b9096e9-dcfe-4829-a64c-78b68b32be17"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""KeyboardAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""085623ee-7fce-44c1-8dbb-d6090189655c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5eb37e47-ea5d-46f4-b013-eb16e706b214"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8544e47e-5cc9-42bc-97f9-903977409f23"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90bdacd1-e8e4-494f-8729-a6a31dd6c754"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cda7ae58-5108-44e5-ab9d-672a740a207a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fb80765-7862-458e-a4fb-f3a4b3c8e56e"",
                    ""path"": ""<Gamepad>/dpad/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e067e421-b59e-490a-8c90-41061835c11f"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e65341c0-1d68-4572-9140-78e99238a344"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9ce0ad4-82c6-41a5-8b05-740be1a38be9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76a931b3-9da0-42ce-8312-d9cc5c305940"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6d0e3a5-e373-46c9-874a-2230e89eab84"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd7693bf-efd2-4183-9b07-3aa230bb37dd"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22dde1cc-21dd-4226-9e8b-54dd81669493"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e10aefc0-9bee-491b-84a3-f2fedb6ac58d"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1b9070e-3bc0-46a2-98ff-fcf4503119c5"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1276ca9b-800d-493b-87da-544647d0eb0f"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2760e95e-f330-4d9b-b7c5-07aa484d73c5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAny"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53094bbd-f9ab-48a4-aac9-0bdf30f9c0c5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38869677-b420-4a33-970e-8af77b3d39ee"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b7c547a-8f50-4961-a19c-3b6851b6f0a5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72f79ff2-1b7a-4b36-ad40-384849622538"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e682620a-1517-4051-9d0d-5e49aed8cc53"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""917b340e-5143-4406-b3ff-c80ca49870e7"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cdddb17-719e-449f-97ea-ee8d18d17715"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f474c01-84f5-4dc0-b83f-63aaf8b5cfb5"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc4b2634-dbc7-43dc-90dc-aa91365d48c0"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6969e2c9-d6c3-474d-97ea-655cef5e99b2"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""KeyboardAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e03bd2c1-c7d7-4154-ba67-852235e23647"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4bebb8e-5833-4506-80bc-9986c3a1b912"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48994be7-b022-445b-8e0a-15526c6cf9b9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MainMenuStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac6f8869-39b0-445d-92d8-151039f041a9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MainMenuStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""364d9657-d1fe-458a-afdf-36af4ad4ca9e"",
            ""actions"": [
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""131ad66b-4a39-4398-bf14-686f5db0fb51"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""7d7bcaa2-37e7-4c7a-8d44-561a75d9a84f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivateCameraAsisst"",
                    ""type"": ""Button"",
                    ""id"": ""48efe316-7e5b-4cb8-aeb2-a89c9fadbb32"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Focus Camera"",
                    ""type"": ""Button"",
                    ""id"": ""29190608-2019-4b38-80ac-d8ef67d4aefc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91f1bd67-e168-4660-825d-e358031ace9b"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Focus Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be747abc-c80d-4c7b-a56e-79efe06701e0"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Focus Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1b63202-58f3-4672-b3ce-101762f91e2b"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ActivateCameraAsisst"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5db74f72-e655-4c99-aebb-be9b87efadb5"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ActivateCameraAsisst"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78149a3f-b16c-47a5-bea0-7ac86c412294"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(max=0.07),InvertVector2,ScaleVector2(x=35,y=35)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14fd65c8-99ea-4d5e-adf2-64f9b14366a3"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""204eefa9-b957-4272-a0a9-2e22bd180f0e"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad Zoom"",
                    ""id"": ""63c8d85b-1a28-4bf1-896a-575fcc7dc4f3"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""0a6e3d3d-dbec-4e08-b73a-ae65abd336d9"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""cd308f9d-155c-47f5-9f1a-230095c08bf9"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(max=0.07),Scale(factor=10)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
            m_Hammy_Debug = m_Hammy.FindAction("Debug", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
            m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
            m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
            m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
            m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
            m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
            m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
            m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
            m_UI_TrackedDevicePosition = m_UI.FindAction("TrackedDevicePosition", throwIfNotFound: true);
            m_UI_TrackedDeviceOrientation = m_UI.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
            m_UI_TrackedDeviceSelect = m_UI.FindAction("TrackedDeviceSelect", throwIfNotFound: true);
            // InputDevice
            m_InputDevice = asset.FindActionMap("InputDevice", throwIfNotFound: true);
            m_InputDevice_KeyboardAny = m_InputDevice.FindAction("KeyboardAny", throwIfNotFound: true);
            m_InputDevice_KeyboardAnyButton = m_InputDevice.FindAction("KeyboardAnyButton", throwIfNotFound: true);
            m_InputDevice_GamepadAny = m_InputDevice.FindAction("GamepadAny", throwIfNotFound: true);
            m_InputDevice_GamepadAnyButton = m_InputDevice.FindAction("GamepadAnyButton", throwIfNotFound: true);
            m_InputDevice_MainMenuStart = m_InputDevice.FindAction("MainMenuStart", throwIfNotFound: true);
            // Camera
            m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
            m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
            m_Camera_Look = m_Camera.FindAction("Look", throwIfNotFound: true);
            m_Camera_ActivateCameraAsisst = m_Camera.FindAction("ActivateCameraAsisst", throwIfNotFound: true);
            m_Camera_FocusCamera = m_Camera.FindAction("Focus Camera", throwIfNotFound: true);
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
        private readonly InputAction m_Hammy_Debug;
        public struct HammyActions
        {
            private InputMaster m_Wrapper;
            public HammyActions(InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Roll => m_Wrapper.m_Hammy_Roll;
            public InputAction @Attach => m_Wrapper.m_Hammy_Attach;
            public InputAction @Use => m_Wrapper.m_Hammy_Use;
            public InputAction @Jump => m_Wrapper.m_Hammy_Jump;
            public InputAction @Pause => m_Wrapper.m_Hammy_Pause;
            public InputAction @Debug => m_Wrapper.m_Hammy_Debug;
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
                    Debug.started -= m_Wrapper.m_HammyActionsCallbackInterface.OnDebug;
                    Debug.performed -= m_Wrapper.m_HammyActionsCallbackInterface.OnDebug;
                    Debug.canceled -= m_Wrapper.m_HammyActionsCallbackInterface.OnDebug;
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
                    Debug.started += instance.OnDebug;
                    Debug.performed += instance.OnDebug;
                    Debug.canceled += instance.OnDebug;
                }
            }
        }
        public HammyActions @Hammy => new HammyActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Navigate;
        private readonly InputAction m_UI_Submit;
        private readonly InputAction m_UI_Cancel;
        private readonly InputAction m_UI_Point;
        private readonly InputAction m_UI_Click;
        private readonly InputAction m_UI_ScrollWheel;
        private readonly InputAction m_UI_MiddleClick;
        private readonly InputAction m_UI_RightClick;
        private readonly InputAction m_UI_TrackedDevicePosition;
        private readonly InputAction m_UI_TrackedDeviceOrientation;
        private readonly InputAction m_UI_TrackedDeviceSelect;
        public struct UIActions
        {
            private InputMaster m_Wrapper;
            public UIActions(InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
            public InputAction @Submit => m_Wrapper.m_UI_Submit;
            public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
            public InputAction @Point => m_Wrapper.m_UI_Point;
            public InputAction @Click => m_Wrapper.m_UI_Click;
            public InputAction @ScrollWheel => m_Wrapper.m_UI_ScrollWheel;
            public InputAction @MiddleClick => m_Wrapper.m_UI_MiddleClick;
            public InputAction @RightClick => m_Wrapper.m_UI_RightClick;
            public InputAction @TrackedDevicePosition => m_Wrapper.m_UI_TrackedDevicePosition;
            public InputAction @TrackedDeviceOrientation => m_Wrapper.m_UI_TrackedDeviceOrientation;
            public InputAction @TrackedDeviceSelect => m_Wrapper.m_UI_TrackedDeviceSelect;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                    Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                    Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                    Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                    Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                    Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                    Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                    Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                    Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                    Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    ScrollWheel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                    ScrollWheel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                    ScrollWheel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                    MiddleClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                    MiddleClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                    MiddleClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                    RightClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                    RightClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                    RightClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                    TrackedDevicePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                    TrackedDevicePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                    TrackedDevicePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                    TrackedDeviceOrientation.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                    TrackedDeviceOrientation.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                    TrackedDeviceOrientation.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                    TrackedDeviceSelect.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceSelect;
                    TrackedDeviceSelect.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceSelect;
                    TrackedDeviceSelect.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceSelect;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Navigate.started += instance.OnNavigate;
                    Navigate.performed += instance.OnNavigate;
                    Navigate.canceled += instance.OnNavigate;
                    Submit.started += instance.OnSubmit;
                    Submit.performed += instance.OnSubmit;
                    Submit.canceled += instance.OnSubmit;
                    Cancel.started += instance.OnCancel;
                    Cancel.performed += instance.OnCancel;
                    Cancel.canceled += instance.OnCancel;
                    Point.started += instance.OnPoint;
                    Point.performed += instance.OnPoint;
                    Point.canceled += instance.OnPoint;
                    Click.started += instance.OnClick;
                    Click.performed += instance.OnClick;
                    Click.canceled += instance.OnClick;
                    ScrollWheel.started += instance.OnScrollWheel;
                    ScrollWheel.performed += instance.OnScrollWheel;
                    ScrollWheel.canceled += instance.OnScrollWheel;
                    MiddleClick.started += instance.OnMiddleClick;
                    MiddleClick.performed += instance.OnMiddleClick;
                    MiddleClick.canceled += instance.OnMiddleClick;
                    RightClick.started += instance.OnRightClick;
                    RightClick.performed += instance.OnRightClick;
                    RightClick.canceled += instance.OnRightClick;
                    TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                    TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                    TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                    TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                    TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                    TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
                    TrackedDeviceSelect.started += instance.OnTrackedDeviceSelect;
                    TrackedDeviceSelect.performed += instance.OnTrackedDeviceSelect;
                    TrackedDeviceSelect.canceled += instance.OnTrackedDeviceSelect;
                }
            }
        }
        public UIActions @UI => new UIActions(this);

        // InputDevice
        private readonly InputActionMap m_InputDevice;
        private IInputDeviceActions m_InputDeviceActionsCallbackInterface;
        private readonly InputAction m_InputDevice_KeyboardAny;
        private readonly InputAction m_InputDevice_KeyboardAnyButton;
        private readonly InputAction m_InputDevice_GamepadAny;
        private readonly InputAction m_InputDevice_GamepadAnyButton;
        private readonly InputAction m_InputDevice_MainMenuStart;
        public struct InputDeviceActions
        {
            private InputMaster m_Wrapper;
            public InputDeviceActions(InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @KeyboardAny => m_Wrapper.m_InputDevice_KeyboardAny;
            public InputAction @KeyboardAnyButton => m_Wrapper.m_InputDevice_KeyboardAnyButton;
            public InputAction @GamepadAny => m_Wrapper.m_InputDevice_GamepadAny;
            public InputAction @GamepadAnyButton => m_Wrapper.m_InputDevice_GamepadAnyButton;
            public InputAction @MainMenuStart => m_Wrapper.m_InputDevice_MainMenuStart;
            public InputActionMap Get() { return m_Wrapper.m_InputDevice; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(InputDeviceActions set) { return set.Get(); }
            public void SetCallbacks(IInputDeviceActions instance)
            {
                if (m_Wrapper.m_InputDeviceActionsCallbackInterface != null)
                {
                    KeyboardAny.started -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnKeyboardAny;
                    KeyboardAny.performed -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnKeyboardAny;
                    KeyboardAny.canceled -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnKeyboardAny;
                    KeyboardAnyButton.started -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnKeyboardAnyButton;
                    KeyboardAnyButton.performed -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnKeyboardAnyButton;
                    KeyboardAnyButton.canceled -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnKeyboardAnyButton;
                    GamepadAny.started -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnGamepadAny;
                    GamepadAny.performed -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnGamepadAny;
                    GamepadAny.canceled -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnGamepadAny;
                    GamepadAnyButton.started -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnGamepadAnyButton;
                    GamepadAnyButton.performed -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnGamepadAnyButton;
                    GamepadAnyButton.canceled -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnGamepadAnyButton;
                    MainMenuStart.started -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnMainMenuStart;
                    MainMenuStart.performed -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnMainMenuStart;
                    MainMenuStart.canceled -= m_Wrapper.m_InputDeviceActionsCallbackInterface.OnMainMenuStart;
                }
                m_Wrapper.m_InputDeviceActionsCallbackInterface = instance;
                if (instance != null)
                {
                    KeyboardAny.started += instance.OnKeyboardAny;
                    KeyboardAny.performed += instance.OnKeyboardAny;
                    KeyboardAny.canceled += instance.OnKeyboardAny;
                    KeyboardAnyButton.started += instance.OnKeyboardAnyButton;
                    KeyboardAnyButton.performed += instance.OnKeyboardAnyButton;
                    KeyboardAnyButton.canceled += instance.OnKeyboardAnyButton;
                    GamepadAny.started += instance.OnGamepadAny;
                    GamepadAny.performed += instance.OnGamepadAny;
                    GamepadAny.canceled += instance.OnGamepadAny;
                    GamepadAnyButton.started += instance.OnGamepadAnyButton;
                    GamepadAnyButton.performed += instance.OnGamepadAnyButton;
                    GamepadAnyButton.canceled += instance.OnGamepadAnyButton;
                    MainMenuStart.started += instance.OnMainMenuStart;
                    MainMenuStart.performed += instance.OnMainMenuStart;
                    MainMenuStart.canceled += instance.OnMainMenuStart;
                }
            }
        }
        public InputDeviceActions @InputDevice => new InputDeviceActions(this);

        // Camera
        private readonly InputActionMap m_Camera;
        private ICameraActions m_CameraActionsCallbackInterface;
        private readonly InputAction m_Camera_Zoom;
        private readonly InputAction m_Camera_Look;
        private readonly InputAction m_Camera_ActivateCameraAsisst;
        private readonly InputAction m_Camera_FocusCamera;
        public struct CameraActions
        {
            private InputMaster m_Wrapper;
            public CameraActions(InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
            public InputAction @Look => m_Wrapper.m_Camera_Look;
            public InputAction @ActivateCameraAsisst => m_Wrapper.m_Camera_ActivateCameraAsisst;
            public InputAction @FocusCamera => m_Wrapper.m_Camera_FocusCamera;
            public InputActionMap Get() { return m_Wrapper.m_Camera; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
            public void SetCallbacks(ICameraActions instance)
            {
                if (m_Wrapper.m_CameraActionsCallbackInterface != null)
                {
                    Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                    Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                    Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                    Look.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLook;
                    Look.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLook;
                    Look.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLook;
                    ActivateCameraAsisst.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnActivateCameraAsisst;
                    ActivateCameraAsisst.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnActivateCameraAsisst;
                    ActivateCameraAsisst.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnActivateCameraAsisst;
                    FocusCamera.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnFocusCamera;
                    FocusCamera.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnFocusCamera;
                    FocusCamera.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnFocusCamera;
                }
                m_Wrapper.m_CameraActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Zoom.started += instance.OnZoom;
                    Zoom.performed += instance.OnZoom;
                    Zoom.canceled += instance.OnZoom;
                    Look.started += instance.OnLook;
                    Look.performed += instance.OnLook;
                    Look.canceled += instance.OnLook;
                    ActivateCameraAsisst.started += instance.OnActivateCameraAsisst;
                    ActivateCameraAsisst.performed += instance.OnActivateCameraAsisst;
                    ActivateCameraAsisst.canceled += instance.OnActivateCameraAsisst;
                    FocusCamera.started += instance.OnFocusCamera;
                    FocusCamera.performed += instance.OnFocusCamera;
                    FocusCamera.canceled += instance.OnFocusCamera;
                }
            }
        }
        public CameraActions @Camera => new CameraActions(this);
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
            void OnDebug(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnNavigate(InputAction.CallbackContext context);
            void OnSubmit(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
            void OnClick(InputAction.CallbackContext context);
            void OnScrollWheel(InputAction.CallbackContext context);
            void OnMiddleClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnTrackedDevicePosition(InputAction.CallbackContext context);
            void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
            void OnTrackedDeviceSelect(InputAction.CallbackContext context);
        }
        public interface IInputDeviceActions
        {
            void OnKeyboardAny(InputAction.CallbackContext context);
            void OnKeyboardAnyButton(InputAction.CallbackContext context);
            void OnGamepadAny(InputAction.CallbackContext context);
            void OnGamepadAnyButton(InputAction.CallbackContext context);
            void OnMainMenuStart(InputAction.CallbackContext context);
        }
        public interface ICameraActions
        {
            void OnZoom(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
            void OnActivateCameraAsisst(InputAction.CallbackContext context);
            void OnFocusCamera(InputAction.CallbackContext context);
        }
    }
}
