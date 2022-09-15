using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Transform playerTransform;

    [SerializeField] float cameraOffSetX = 0.005f;
    [SerializeField] float cameraOffSetY = 0.2f;
    [SerializeField] float cameraOffSetZ = -0.5f;

    private void Start()
    {
        ComponentGetter();
    }

    void Update()
    {
        ManageCameraMovement();
    }

    void ComponentGetter()
    {
        playerTransform = GameObject.Find("Boy").GetComponent<Transform>();
    }

    void ManageCameraMovement()
    {
        Vector3 cameraOffSet = new Vector3(cameraOffSetX, cameraOffSetY, cameraOffSetZ);
        transform.position = playerTransform.position + cameraOffSet;
    }
}
