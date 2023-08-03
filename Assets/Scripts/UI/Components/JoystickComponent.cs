using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestAssignment.UI.Components
{
    public class JoystickComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField]
        private RectTransform _joystick;
        private float _maxOffsetDistance = 100;

        private RectTransform _rectTransform;

        private float _sensetivity = 0.5f;

        public event Action<Vector2> OnJoystickInput;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
        }

        private void OnDisable()
        {
            MoveJoystick(_rectTransform.position);
        }
        public void OnDrag(PointerEventData eventData)
        {
            MoveJoystick(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            MoveJoystick(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            MoveJoystick(_rectTransform.position);
        }

        private void MoveJoystick(Vector3 position)
        {
            position = position - _rectTransform.position;
            position = Vector3.ClampMagnitude(position * _sensetivity, _maxOffsetDistance);

            _joystick.localPosition = position;

            OnJoystickInput?.Invoke(position / _maxOffsetDistance);
        }
    }
}
