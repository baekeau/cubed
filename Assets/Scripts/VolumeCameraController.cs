using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class VolumeCameraController : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.5f; // Smoothing factor for camera movement
    [SerializeField] private Vector3 _offset; // Offset between the camera and the character
    private GameObject _target; 
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(WaitForPlayerSpawn());
    }

    private IEnumerator WaitForPlayerSpawn()
    {
        // Wait for Player spawn
        while (GameManager.Instance.Player == null)
        {
            Debug.Log("Looking for player");
            yield return null;
        }
        
        Debug.Log("Found player");
        
        _target = GameManager.Instance.Player;
        // calculate the initial offset based on the spawned character's position
        _offset = transform.position - _target.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 _targetPosition = _target?.transform.position ?? transform.position;
        Vector3 desiredPosition = _targetPosition + _offset; 
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
    }

    public void RotateCamera(float rotationX, float rotationY)
    {
        transform.rotation *= Quaternion.Euler(0, rotationX, 0f);
    }

}
