using SingletonPattern;
using UnityEngine;

namespace Misc
{
    public class KeyTransformations : MonoBehaviour
    {
        // Rotation speed in degrees per second
        public float rotationSpeed = 100f;

        // Minimum and maximum scale values
        public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 maxScale = new Vector3(2f, 2f, 2f);

        // Time it takes to transition between scales
        public float scaleSpeed = 2f;

        // Internal variables to track scaling direction
        private bool _scalingUp = true;
        private float _scaleTimer = 0f;

        void Update()
        {
            if (GameManager.Instance.runGame)
            {
                // Rotate the object continuously
                transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.Self);
                transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime), Space.Self);
                transform.Rotate(Vector3.right * (rotationSpeed * Time.deltaTime), Space.Self);

                // Update the scale over time
                _scaleTimer += Time.deltaTime * scaleSpeed;

                if (_scalingUp)
                {
                    transform.localScale = Vector3.Lerp(minScale, maxScale, _scaleTimer);
                    if (_scaleTimer >= 1f)
                    {
                        _scalingUp = false;
                        _scaleTimer = 0f; // Reset the timer for the reverse scale
                    }
                }
                else
                {
                    transform.localScale = Vector3.Lerp(maxScale, minScale, _scaleTimer);
                    if (_scaleTimer >= 1f)
                    {
                        _scalingUp = true;
                        _scaleTimer = 0f; // Reset the timer for the forward scale
                    }
                }
            }
        }
    }
}