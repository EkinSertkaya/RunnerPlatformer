using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    Vector3 cachedPos;
    Vector3 targetPos;

    NavMeshAgent opponent;
    
    GameObject firstDestinationPos;
    GameObject secondDestinationPos;
    GameObject thirdDestinationPos;
    GameObject fourthDestinationPos;
    GameObject fifthAIDestination;
    GameObject sixthAIDestination;
    GameObject seventhAIDestination;

    Animator opponentAnimator;

    [SerializeField] float secondDestinationMinPos = 0.1f;
    [SerializeField] float secondDestinationMaxPos = 0.2f;
    [SerializeField] float thirdDestinationMinPos = 1.2f;
    [SerializeField] float thirdDestinationMaxPos = 1.4f;
    [SerializeField] float fourthDestinationMinPos = 2.9f;
    [SerializeField] float fourthDestinationMaxPos = 3f;
    [SerializeField] float fifthDestinationMinPos = 3.3f;
    [SerializeField] float fifthDestinationMaxPos = 3.4f;
    [SerializeField] float sixthDestinationMinPos = 3.6f;
    [SerializeField] float sixthDestinationMaxPos = 3.8f;
    [SerializeField] float seventhDestinationMinPos = 4.1f;

    void Start()
    {
        ComponentGetter();
    }

    private void Update()
    {
        ManageAIDestination();
        ManageAIAnimationRotation();
    }

    void ComponentGetter()
    {
        opponent = GetComponent<NavMeshAgent>();
        firstDestinationPos = GameObject.Find("FirstAIDestination");
        secondDestinationPos = GameObject.Find("SecondAIDestination");
        thirdDestinationPos = GameObject.Find("ThirdAIDestination");
        fourthDestinationPos = GameObject.Find("FourthAIDestination");
        fifthAIDestination = GameObject.Find("FifthAIDestination");
        sixthAIDestination = GameObject.Find("SixthAIDestination");
        seventhAIDestination = GameObject.Find("SeventhAIDestination");
        opponentAnimator = GetComponent<Animator>();
        cachedPos = transform.position;
        targetPos = firstDestinationPos.transform.position;
    }

    void ManageAIDestination()
    {
        if (transform.position.z >= secondDestinationMinPos && transform.position.z < secondDestinationMaxPos)
        {
            targetPos = secondDestinationPos.transform.position;
        }
        if (transform.position.z >= thirdDestinationMinPos && transform.position.z < thirdDestinationMaxPos)
        {
            targetPos = thirdDestinationPos.transform.position;
        }
        if (transform.position.z >= fourthDestinationMinPos && transform.position.z < fourthDestinationMaxPos)
        {
            targetPos = fourthDestinationPos.transform.position;
        }
        if (transform.position.z >= fifthDestinationMinPos && transform.position.z < fifthDestinationMaxPos)
        {
            targetPos = fifthAIDestination.transform.position;
        }
        if (transform.position.z >= sixthDestinationMinPos && transform.position.z < sixthDestinationMaxPos)
        {
            targetPos = sixthAIDestination.transform.position;
        }
        if (transform.position.z >= seventhDestinationMinPos)
        {
            targetPos = seventhAIDestination.transform.position;
        }

        opponent.SetDestination(targetPos);
    }

    void ManageAIAnimationRotation()
    {
        if (opponent.velocity != Vector3.zero)
        {
            opponentAnimator.SetInteger("isRunning", 1);
        }
        else if (opponent.velocity == Vector3.zero)
        {
            opponentAnimator.SetInteger("isRunning", 0);
        }
        opponent.updateRotation = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            opponent.Warp(cachedPos);
            targetPos = firstDestinationPos.transform.position;
        }
    }

}
