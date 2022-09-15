using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformBehaviour : MonoBehaviour
{
    
    float leftRotationAmount = 0.4f;
    float rightRotationAmount = -0.4f;

    [SerializeField] float rotationSpeed = 0.4f;

    private void Start()
    {
        StartCoroutine(RotationManager());
    }

    IEnumerator RotationManager()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);

            if (gameObject.tag == "Rotating Platform Left")
            {
                transform.rotation = Quaternion.Euler(0f, 0f, leftRotationAmount);
                leftRotationAmount += rotationSpeed * Time.deltaTime;
            }

            if (gameObject.tag == "Rotating Platform Right")
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rightRotationAmount);
                rightRotationAmount += -rotationSpeed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        other.transform.parent = transform;
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.transform.parent == transform)
        {
            other.transform.parent = null;
        }
    }
}
