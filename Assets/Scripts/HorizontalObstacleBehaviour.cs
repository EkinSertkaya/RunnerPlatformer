using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleBehaviour : MonoBehaviour
{
    bool cycleComplete = true;

    [SerializeField] float maxPosInX = 0.26f;
    [SerializeField] float minPosInx = -0.26f;
    [SerializeField] float speedInDistance = 0.008f;

    private void Update()
    {
        HorizontalObstacleMovement();
    }

    void HorizontalObstacleMovement()
    {
        if (transform.position.x <= maxPosInX && cycleComplete)
        {

            transform.Translate(new Vector3(speedInDistance * Time.deltaTime, 0f, 0f));

            if (transform.position.x > maxPosInX)
            {
                cycleComplete = false;
            }
        }

        else if (transform.position.x >= minPosInx && !cycleComplete)
        {

            transform.Translate(new Vector3(-speedInDistance * Time.deltaTime, 0f, 0f));

            if (transform.position.x < minPosInx)
            {
                cycleComplete = true;
            }
        }
    }
    
}
