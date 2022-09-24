namespace SexyFishHorse.CitiesSkylines.Birdcage.Services
{
    using UnityEngine;

    public class InputService
    {
        private bool _leftCtrlDown;

        private bool _rightCtrlDown;

        public bool AnyControlDown
        {
            get
            {
                return _leftCtrlDown || _rightCtrlDown;
            }
        }

        public bool PrimaryMouseButtonDownState { get; private set; }

        public void Update()
        {
            SetAnyControlDownState();
            SetPrimaryMouseButtonDownState();
        }

        private void SetAnyControlDownState()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _leftCtrlDown = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                _leftCtrlDown = false;
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                _rightCtrlDown = true;
            }

            if (Input.GetKeyUp(KeyCode.RightControl))
            {
                _rightCtrlDown = false;
            }
        }

        private void SetPrimaryMouseButtonDownState()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PrimaryMouseButtonDownState = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                PrimaryMouseButtonDownState = false;
            }
        }
    }
}
