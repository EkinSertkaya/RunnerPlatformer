using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorStickBehaviour : MonoBehaviour
{
    Rigidbody pushedObjectRB;

    Vector3 forceDirection;

    bool applyingPush = false;

    [SerializeField] float stickPushForce = 3f;
    [SerializeField] float stickPushDuration = 1f;

    private void FixedUpdate()
    {
        ApplyPush();
    }

    void ApplyPush()
    {
        if (applyingPush)
        {
            pushedObjectRB.AddForce(new Vector3(forceDirection.x, 0f, 0f) * stickPushForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 contactPoint = other.contacts[0].point;
            forceDirection = (other.transform.position - contactPoint).normalized;
            pushedObjectRB = other.gameObject.GetComponent<Rigidbody>();
            applyingPush = true;
            StartCoroutine(PushDuration());
        }
    }

    IEnumerator PushDuration()
    {
        yield return new WaitForSeconds(stickPushDuration);

        applyingPush = false;
    }
}
