using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject wallColorPercentileTextObject;
    [SerializeField] GameObject[] allRunners;
    public GameObject endGameUI;

    TextMeshProUGUI percetileText;
    TextMeshProUGUI playerStanding;

    Vector3 farClippingPlane;
    Vector3 nearClippingPlane;
    Vector3 screenFarClippingPlane;
    Vector3 screenNearClippingPlane;
    Vector3 raycastDirection;
    Vector3 cachedPlayerPos;

    PlayerMovement playerMovementScript;
    
    GameObject playerGameObject;

    [HideInInspector] public PaintedWallBehaviour paintedWallBehaviourScript;

    public RaycastHit raycastHitInfo;

    int playerStandingNumber;
    int paintedVerticePercentile = 0;

    [SerializeField] float raycastLenght = 50f;
    [SerializeField] float playerFallLimit = -2f;

    private void Start()
    {
        ComponentGetter();
        StartCoroutine(WallPaintPercentage());
        StartCoroutine(StandingTable());
    }

    private void Update()
    {
        RayCastInPerspective();
        PlayerFall();
        PlayerStandingUIUpdate();
        EndGame();
    }

    void ComponentGetter()
    {
        playerMovementScript = GameObject.Find("Boy").GetComponent<PlayerMovement>();
        playerGameObject = GameObject.Find("Boy");
        cachedPlayerPos = playerGameObject.transform.position;
        paintedWallBehaviourScript = GameObject.FindGameObjectWithTag("Painted Wall").GetComponent<PaintedWallBehaviour>();
        paintedWallBehaviourScript.gameObject.SetActive(false);
        percetileText = wallColorPercentileTextObject.GetComponent<TextMeshProUGUI>();
        playerStanding = GameObject.Find("Player  Standing Text").GetComponent<TextMeshProUGUI>();
        endGameUI = GameObject.Find("EndGame UI");
        endGameUI.SetActive(false);
    }

    IEnumerator WallPaintPercentage()
    {
        int currentPaintedVerticeCount = 0;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            currentPaintedVerticeCount = 0;

            for (int i = 0; i < paintedWallBehaviourScript.vertexColors.Length; ++i)
            {
                if (paintedWallBehaviourScript.vertexColors[i] == Color.red)
                {
                    ++currentPaintedVerticeCount;
                }
            }

            if (currentPaintedVerticeCount > 0)
            {
                paintedVerticePercentile = (currentPaintedVerticeCount * 100) / paintedWallBehaviourScript.vertexColors.Length;
            }

            percetileText.text = "%" + paintedVerticePercentile;
        }
    }

    IEnumerator StandingTable()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            for (int i = allRunners.Length - 1; i >= 0; --i)
            {
                if (!allRunners[i])
                {
                    continue;
                }
                for (int k = allRunners.Length - 1; k >= 0; --k)
                {
                    if (!allRunners[k])
                    {
                        continue;
                    }
                    if (allRunners[i].transform.position.z < allRunners[k].transform.position.z)
                    {
                        GameObject temp;
                        temp = allRunners[i];
                        allRunners[i] = allRunners[k];
                        allRunners[k] = temp;
                    }
                }
            }

            for (int i = 0; i < allRunners.Length; ++i)
            {
                if (allRunners[i] && allRunners[i].tag == "Player")
                {
                    playerStandingNumber = i + 1;
                }
            }
        }
    }

    void RayCastInPerspective()
    {
        farClippingPlane = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        nearClippingPlane = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        screenFarClippingPlane = Camera.main.ScreenToWorldPoint(farClippingPlane);
        screenNearClippingPlane = Camera.main.ScreenToWorldPoint(nearClippingPlane);

        raycastDirection = (screenFarClippingPlane - screenNearClippingPlane).normalized;

        Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), raycastDirection, out raycastHitInfo, raycastLenght);

        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), raycastDirection * 50f, Color.yellow);
    }

    void PlayerFall()
    {
        if (playerGameObject.transform.position.y < playerFallLimit)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        playerGameObject.transform.position = cachedPlayerPos;
    }

    void PlayerStandingUIUpdate()
    {
        playerStanding.text = playerStandingNumber + "/11";
    }

    void EndGame()
    {
        if(paintedVerticePercentile >= 100)
        {
            endGameUI.SetActive(true);
        }
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }




}
