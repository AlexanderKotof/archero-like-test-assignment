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

        private Vector3 _defaultPosition;

        public event Action<Vector2> OnJoystickInput;

        private void Awake()
        {
            _defaultPosition = transform.position;
        }

        private void OnDisable()
        {
            MoveJoystick(_defaultPosition);
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
            MoveJoystick(_defaultPosition);
        }

        private void MoveJoystick(Vector3 position)
        {
            position = position - _defaultPosition;
            position = Vector3.ClampMagnitude(position, _maxOffsetDistance);

            _joystick.localPosition = position;

            OnJoystickInput?.Invoke(position / _maxOffsetDistance);
        }
    }
}
