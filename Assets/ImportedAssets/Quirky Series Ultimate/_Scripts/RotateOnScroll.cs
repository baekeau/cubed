/* Scripted by Omabu - omabuarts@gmail.com */

using UnityEngine;

namespace ImportedAssets.Quirky_Series_Ultimate._Scripts
{
    public class RotateOnScroll : MonoBehaviour
    {
        public float rotationSpeed = 2000f;

        void Update()
        {
            float scrollWheelInput = UnityEngine.Input.GetAxis("Mouse ScrollWheel");

            if (scrollWheelInput != 0f)
            {
                float rotationAmount = scrollWheelInput * rotationSpeed * Time.deltaTime * 10;
                transform.Rotate(Vector3.up, rotationAmount);
            }
        }
    }
}
