namespace SexyFishHorse.CitiesSkylines.Birdcage.Services
{
    using UnityEngine;

    public class InputService
    {
        private bool leftCtrlDown;

        private bool rightCtrlDown;

        public bool AnyControlDown
        {
            get
            {
                return leftCtrlDown || rightCtrlDown;
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
                leftCtrlDown = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                leftCtrlDown = false;
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                rightCtrlDown = true;
            }

            if (Input.GetKeyUp(KeyCode.RightControl))
            {
                rightCtrlDown = false;
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
