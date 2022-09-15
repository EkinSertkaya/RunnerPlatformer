using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBehaviour : MonoBehaviour
{
    float rotationAmount = 2f;
    [SerializeField] float rotationSpeed = 2f;

    [SerializeField] bool changeDirection = false;

    void Update()
    {
        RotationManager();
    }

    void RotationManager()
    {
        if (!changeDirection)
        {
            transform.rotation = Quaternion.Euler(0f, rotationAmount, 0f);
            rotationAmount += rotationSpeed * Time.deltaTime;
        }
        else if (changeDirection)
        {
            transform.rotation = Quaternion.Euler(0f, -rotationAmount, 0f);
            rotationAmount += rotationSpeed * Time.deltaTime;
        }
    }
}
