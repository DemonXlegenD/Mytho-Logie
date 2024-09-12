using Nova;
using UnityEngine;
using UnityEngine.Events;

namespace NovaSamples.UIControls
{
    /// <summary>
    /// A UI control which reacts to user input and fires click events
    /// </summary>
    public class Button : UIControl<ButtonVisuals>
    {
        [Tooltip("Event fired when the button is clicked.")]
        public UnityEvent OnClicked = null;

        public UnityEvent OnPress = null;
        public UnityEvent OnRelease = null;

        public UnityEvent OnHover = null;
        public UnityEvent OnUnhover = null;

        private void OnEnable()
        {
            View.UIBlock.AddGestureHandler<Gesture.OnClick, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, OnClicked));
            View.UIBlock.AddGestureHandler<Gesture.OnHover, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, ButtonVisuals.HandleHovered, OnHover));
            View.UIBlock.AddGestureHandler<Gesture.OnUnhover, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, ButtonVisuals.HandleUnhovered, OnUnhover));
            View.UIBlock.AddGestureHandler<Gesture.OnPress, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, ButtonVisuals.HandlePressed, OnPress));
            View.UIBlock.AddGestureHandler<Gesture.OnRelease, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, OnRelease));
        }

        private void OnDisable()
        {
            View.UIBlock.RemoveGestureHandler<Gesture.OnClick, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, OnClicked));
            View.UIBlock.RemoveGestureHandler<Gesture.OnHover, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, ButtonVisuals.HandleHovered, OnHover));
            View.UIBlock.RemoveGestureHandler<Gesture.OnUnhover, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, ButtonVisuals.HandleUnhovered, OnUnhover));
            View.UIBlock.RemoveGestureHandler<Gesture.OnPress, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, ButtonVisuals.HandlePressed, OnPress));
            View.UIBlock.RemoveGestureHandler<Gesture.OnRelease, ButtonVisuals>((evt, visuals) => HandleGesture(evt, visuals, OnRelease));
        }

        /// <summary>
        /// Generic handler for gestures, visual updates, and Unity events.
        /// </summary>
        /// <param name="evt">The gesture event data.</param>
        /// <param name="visuals">The button visuals receiving the event.</param>
        /// <param name="visualUpdateAction">The action to update the visuals.</param>
        /// <param name="unityEvent">The Unity event to invoke.</param>
        private void HandleGesture<T>(T evt, ButtonVisuals visuals, System.Action<T, ButtonVisuals> visualUpdateAction, UnityEvent unityEvent)
        {
            // Update the visuals
            visualUpdateAction?.Invoke(evt, visuals);

            // Invoke the Unity event
            unityEvent?.Invoke();
        }

        /// <summary>
        /// Generic handler for gestures, visual updates, and Unity events.
        /// </summary>
        /// <param name="evt">The gesture event data.</param>
        /// <param name="visuals">The button visuals receiving the event.</param>
        /// <param name="unityEvent">The Unity event to invoke.</param>
        private void HandleGesture<T>(T evt, ButtonVisuals visuals, UnityEvent unityEvent)
        {

            // Invoke the Unity event
            unityEvent?.Invoke();
        }


    }
}
