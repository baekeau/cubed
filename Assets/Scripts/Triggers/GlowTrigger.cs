using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowTrigger : MonoBehaviour
{
   public Transform objectToScale; // Reference to the object that will be scaled and rotated
    public float scaleFactor = 1.5f; // Factor by which the object will be scaled
    public float scaleUpDuration = 2f; // Duration of the scale up animation
    public float rotationSpeed = 90f; // Speed of the rotation in degrees per second
    public string playerTag = "Player"; // Tag of the player game object

    private Vector3 originalScale; // Original scale of the object
    private bool isAnimating = false; // Flag to track if the animation is currently playing
    private bool isRotating = false; // Flag to track if the object is currently rotating

    private void Start()
    {
        // Store the original scale of the object
        originalScale = objectToScale.localScale;
    }
    private void Update()
    {
        // Continuously rotate the object if the rotation flag is set
        if (isRotating)
        {
            objectToScale.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger belongs to the player and the animation is not already playing
        if (other.CompareTag(playerTag) && !isAnimating)
        {
            StartCoroutine(ScaleUpAndRotate());
        }
    }

    private IEnumerator ScaleUpAndRotate()
    {
        isAnimating = true;
        isRotating = true;
        float elapsedTime = 0f;

        while (elapsedTime < scaleUpDuration)
        {
            // Calculate the interpolation factor based on the elapsed time
            float t = elapsedTime / scaleUpDuration;

            // Lerp the object's scale between the original scale and the target scale
            objectToScale.localScale = Vector3.Lerp(originalScale, originalScale * scaleFactor, t);


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the target scale at the end of the animation
        objectToScale.localScale = originalScale * scaleFactor;
        isAnimating = false;
    }

    private void OnTriggerExit(Collider other)
    {
        isRotating = false;
        // Check if the collider that exited the trigger belongs to the player and the animation is currently playing
        if (other.CompareTag(playerTag) && isAnimating)
        {
            StopCoroutine(ScaleUpAndRotate());
            objectToScale.localScale = originalScale;
            isAnimating = false;
        }
    }

    }
