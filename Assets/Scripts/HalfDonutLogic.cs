using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutLogic : MonoBehaviour
{
    [SerializeField] float pushDistance = -0.126f;
    [SerializeField] float pullDistance = 0.162f;
    [SerializeField] float donutSpeedInDistance = 0.01f;

    bool donutCycleComplete = true;

    private void Update()
    {
        if (donutCycleComplete)
        {

            transform.Translate(new Vector3(-donutSpeedInDistance * Time.deltaTime, 0f, 0f));

            if (transform.localPosition.x < pushDistance)
            {
                donutCycleComplete = false;
            }
        }
        else if (!donutCycleComplete)
        {

            transform.Translate(new Vector3(donutSpeedInDistance * Time.deltaTime, 0f, 0f));
            if (transform.localPosition.x > pullDistance)
            {
                donutCycleComplete = true;
            }
        }
    }
}
