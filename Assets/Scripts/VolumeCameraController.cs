using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class VolumeCameraController : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.5f; // Smoothing factor for camera movement
    private Vector3 _offset; // Offset between the camera and the character
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
        LerpVolumeCamera();
    }

    private void LerpVolumeCamera()
    {
        Vector3 targetPosition = _target?.transform.position ?? transform.position;
        Vector3 desiredPosition = targetPosition + _offset; 
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
    }

    public void RotateCamera(float rotationX)
    {
        transform.rotation *= Quaternion.Euler(0, rotationX, 0f);
    }

}
