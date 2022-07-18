using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using GMTK2022.Utils;

namespace GMTK2022.Core
{
    // --------------------------------------------------------------------------
    // Button setups
    // TODO(Nghia Lam): Should we create a StateMachine to handle these kind of
    // state for the button and later for the Characters?
    // --------------------------------------------------------------------------
    public class InputButton
    {
        /// <summary>
        /// All possible states for a button. Can be used in a state machine.
        /// </summary>
        public enum States { OFF, BUTTON_DOWN, BUTTON_PRESSED, BUTTON_UP }

        public delegate void ButtonDownDelegate();
        public delegate void ButtonPressedDelegate();
        public delegate void ButtonUpDelegate();

        public ButtonDownDelegate ButtonDownHandler;
        public ButtonPressedDelegate ButtonPressedHandler;
        public ButtonUpDelegate ButtonUpHandler;

        public States CurrentState { get; protected set; }

        public InputButton(ButtonDownDelegate btnDown = null, ButtonPressedDelegate btnPressed = null, ButtonUpDelegate btnUp = null)
        {
            ButtonDownHandler = btnDown;
            ButtonUpHandler = btnUp;
            ButtonPressedHandler = btnPressed;
            CurrentState = InputButton.States.OFF;
        }

        public void ChangeState(InputButton.States newState)
        {
            if (CurrentState != newState)
                CurrentState = newState;
        }

        public void TriggerButtonDown()
        {
            if (ButtonDownHandler == null)
                ChangeState(InputButton.States.BUTTON_DOWN);
            else
                ButtonDownHandler();
        }

        public void TriggerButtonPressed()
        {
            if (ButtonPressedHandler == null)
                ChangeState(InputButton.States.BUTTON_PRESSED);
            else
                ButtonPressedHandler();
        }

        public void TriggerButtonUp()
        {
            if (ButtonUpHandler == null)
                ChangeState(InputButton.States.BUTTON_UP);
            else
                ButtonUpHandler();
        }
    }

    // --------------------------------------------------------------------------
    // Input Manager
    // --------------------------------------------------------------------------
    public class InputManager : MonoBehaviour, IManager
    {
        /// A set of input actions to use to read input on
        public InputControl inputActions;

        /// The roll button
        public InputButton Roll { get; protected set; }

        /// The Swap button
        public InputButton Swap { get; protected set; }

        /// The primary movement value (used to move the character around)
        public Vector2 PrimaryAxis { get { return _primaryAxis; } set { _primaryAxis = value; } }

        protected List<InputButton> _buttonList;
        protected Vector2 _primaryAxis = Vector2.zero;

        // Methods
        // -----------------------------------------------------------------------
        public void Initialize()
        {
            // Init buttons
            _buttonList = new List<InputButton>();
            _buttonList.Add(Roll = new InputButton());
            _buttonList.Add(Swap = new InputButton());

            // Bind Actions
            inputActions = new InputControl();
            inputActions.Enable();
            inputActions.InGame.Movement.performed += context => _primaryAxis = context.ReadValue<Vector2>();
            inputActions.InGame.Roll.performed += context => { BindButton(context, Roll); };
            inputActions.InGame.Swap.performed += context => { BindButton(context, Swap); };
        }

        public void LateUpdate()
        {
            ProcessButtonStates();
        }

        public void ProcessButtonStates()
        {
            // For each button, if we were at ButtonDown this frame, we go to
            // ButtonPressed. If we were at ButtonUp, we're now Off
            foreach (InputButton button in _buttonList)
            {
                if (button.CurrentState == InputButton.States.BUTTON_DOWN)
                    button.ChangeState(InputButton.States.BUTTON_PRESSED);
                if (button.CurrentState == InputButton.States.BUTTON_UP)
                    button.ChangeState(InputButton.States.OFF);
            }
        }

        /// <summary>
        /// Changes the state of our button based on the input value
        /// </summary>
        /// <param name="context"></param>
        /// <param name="inputButton"></param>
        protected virtual void BindButton(InputAction.CallbackContext context, InputButton inputButton)
        {
            var control = context.control;

            if (control is ButtonControl button)
            {
                if (button.wasPressedThisFrame)
                    inputButton.ChangeState(InputButton.States.BUTTON_DOWN);
                if (button.wasReleasedThisFrame)
                    inputButton.ChangeState(InputButton.States.BUTTON_UP);
            }
        }

        /// <summary>
        /// On enable we enable our input actions
        /// </summary>
        private void OnEnable()
        {
            if (inputActions != null)
                inputActions.Enable();
        }

        /// <summary>
        /// On disable we disable our input actions
        /// </summary>
        private void OnDisable()
        {
            if (inputActions != null)
                inputActions.Disable();
        }
    }
}