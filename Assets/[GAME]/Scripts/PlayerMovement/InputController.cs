using CubeWinner.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TaylanGame.controllers
{

    public class InputController : MonoBehaviour
    {
        #region Variables
        private Vector3 _mouseFirstPosition;
        private Vector3 _mouseHoldPosition;
        private Vector3 _remoteControl;

        public Vector3 RemoteControl => _remoteControl;

        [SerializeField] private float InputSpeed;
        [SerializeField] private float xMinumumClamp;
        [SerializeField] private float xMaximumClamp;

        private float _xValue;

        

        public bool _isMouseUp;
        #endregion
        #region Methods

        private void Update()
        {
            ManageRigidbodyPositionsWithInputs();
        }

        private void ManageRigidbodyPositionsWithInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetFirstClick();
            }
            else if (Input.GetMouseButton(0))
            {
                RemoteControlWithClamp();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                MouseUpPosition();
            }
        }

        private void GetFirstClick()
        {
            _mouseFirstPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        private void RemoteControlWithClamp()
        {
            _mouseHoldPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            var dist = (_mouseHoldPosition - _mouseFirstPosition) * InputSpeed;
            var value = _xValue - dist.x;
            _remoteControl.x = Mathf.Clamp(value, xMinumumClamp, xMaximumClamp);
            CubeManager.Instance.HoldClickMovement(_remoteControl.x);
         
        }
        private void MouseUpPosition()
        {
            _xValue = -.5f;
            _isMouseUp = true;
            CubeManager.Instance.SetMouseUp();

        }
        }
    }
         #endregion
