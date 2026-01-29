using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;
using static Tile;

public class Snail : MonoBehaviour
{
    private int layerMask = (1 << 8);
    [SerializeField] private GameObject currentTile;

    [SerializeField] private CubeState cubeState;
    private ReadCube readCube;

    [SerializeField] private float lerpDuration = 1.5f;
    [SerializeField] private float rotateSpeed = 1.5f; // Controls smoothness of rotation


    [SerializeField] private GameObject model;

    public int sideFlag = 0;
    // Top = 0, Left = 1, Right = 2, Bottom = 3, Back Left = 4, Back Right = 5

    public event Action<int> OnZoneEntered;
    public event Action<int> OnZoneExited;

    public List<Collider> rotationZones;

    public bool isLerping = false;

    private bool initialUpdate = false;

    public bool isFrosted = false;
    public bool isCold = false;

    [SerializeField] private Animator snailAnimator;

    [SerializeField] private RotationUI rotationUI;

    public delegate void OnItemPickup(int number);
    public static event OnItemPickup onItemPickup;

    GameObject lastFace;

    void Start()
    {
        UpdateFogOfWar(); // Need to do this at start of round
        Vector3 newRotation = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(newRotation);
        readCube = FindFirstObjectByType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Collider zoneCollider in rotationZones)
        {
            if (zoneCollider == null) continue;

            // Check if object is inside the zone bounds
            if (zoneCollider.bounds.Contains(transform.position))
            {
                int zoneFlag = zoneCollider.GetComponent<RotationZone>().flag;

                if (zoneFlag != null)
                {
                    sideFlag = zoneFlag;
                    //UnityEngine.Debug.Log("INSIDE: " + zoneFlag);

                    break; // stop at first zone found
                }
            }
        }

        if (!initialUpdate)
        {
            UpdateFogOfWar();
            initialUpdate = true;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.Instance.GetIsMovingSnail() && !isLerping)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;


                //UnityEngine.Debug.Log("ANDREW: " + Vector3.Distance(currentTile.transform.position, face.transform.position));
                if (Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1 && face.GetComponent<Tile>().traversable == true
                    && face.GetComponent<Tile>().currentPassive == null && face.GetComponent<Tile>().tileType != Tile.TileType.Unassigned)
                {
                    float xDistance = Mathf.Abs(currentTile.transform.position.x - face.transform.position.x);
                    float yDistance = Mathf.Abs(currentTile.transform.position.y - face.transform.position.y);
                    float zDistance = Mathf.Abs(currentTile.transform.position.z - face.transform.position.z);

                    int halfAxisCount = (Mathf.Abs(xDistance - 0.5f) < 0.01f ? 1 : 0) + (Mathf.Abs(yDistance - 0.5f) < 0.01f ? 1 : 0) + (Mathf.Abs(zDistance - 0.5f) < 0.01f ? 1 : 0);

                    if (halfAxisCount == 2)
                    {
                        snailAnimator.SetBool("isLerping", true);
                    }

                    Vector3 moveDir = (face.transform.position - currentTile.transform.position).normalized;

                    Vector3 tileUp = face.transform.up;

                    tileUp = hit.normal;

                    Vector3 planarDir = Vector3.ProjectOnPlane(moveDir, tileUp).normalized;

                    Quaternion modelOffset = Quaternion.Euler(0, -90, 0);
                    Quaternion targetRot = Quaternion.identity;
                    if (lastFace != face) 
                    {
                        targetRot = Quaternion.LookRotation(planarDir, tileUp) * modelOffset;
                    }
                    else 
                    {
                        targetRot = transform.rotation;
                    }
                    StartCoroutine(DoLerpPosition(face.transform.position, lerpDuration, targetRot));

                    if (Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1)
                        currentTile = face;

                    StartCoroutine(TurnOrder(face));
                }
                lastFace = face;
            }
        }

    }



    public void UpdateFogOfWar()
    {
        readCube = FindFirstObjectByType<ReadCube>();
        readCube.ReadState();
        List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.upTiles, cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                };

        // If the face exists within a side
        foreach (List<GameObject> cubeSide in cubeSides)
        {
            foreach (GameObject face in cubeSide)
            {
                if (face.GetComponentInChildren<Tile>() != null)
                {
                    if (Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1)
                    {
                        //Debug.Log("NAME: " + face.name + " " + face.gameObject.transform.parent.name + " " + face.gameObject.transform.parent.transform.parent.name);
                        face.GetComponentInChildren<Tile>().RevealFromFog();
                    }
                }
                
            }
        }
    }

    void UpdateOcean()
    {
        readCube = FindFirstObjectByType<ReadCube>();
        readCube.ReadState();
        List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.upTiles, cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                };

        // If the face exists within a side
        foreach (List<GameObject> cubeSide in cubeSides)
        {
            foreach (GameObject face in cubeSide)
            {
                if (face.GetComponentInChildren<Tile>() != null)
                {
                    if(face.GetComponentInChildren<Tile>().tileType == Tile.TileType.Ocean)
                    {
                        face.GetComponentInChildren<Tile>().TickDownOceanTile();
                    }
                }
                else
                {
                    Debug.Log("Face without a tile" + face.name + " " + face.gameObject.transform.parent.name + " " + face.gameObject.transform.parent.transform.parent.name);
                }
            }
        }
    }

    void UpdateDesert()
    {
        /*List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.upTiles, cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                };

        // If the face exists within a side
        foreach (List<GameObject> cubeSide in cubeSides)
        {
            foreach (GameObject face in cubeSide)
            {
                if (face.GetComponent<Tile>().tileType == Tile.TileType.Desert)
                {
                    {
                        face.GetComponent<Tile>().updateDesert();
                    }
                }
            }
        }*/
    }

    public GameObject GetCurrentTile()
    {
        return currentTile;
    }

    IEnumerator DoLerpPosition(Vector3 targetPosition, float duration, Quaternion targetRotation)
    {
        isLerping = true;
        float time = 0;
        Vector3 startPosition = transform.position;
        snailAnimator.SetFloat("LERPMove", 0);
        


        while (time < duration)
        {
            float t = time / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            snailAnimator.SetFloat("LERPMove", t);

          
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        snailAnimator.SetBool("isLerping", false);

        isLerping = false;

    }

    IEnumerator TurnOrder(GameObject face)
    {
        if (face.GetComponent<Tile>().collectibleOccupant)
        {
            face.GetComponent<Tile>().HideShipPiece();
            GameManager.Instance.AddToPiecesHeld();
            Debug.Log("Sending " + face.GetComponent<Tile>().collectibleNumber);
            onItemPickup?.Invoke(face.GetComponent<Tile>().collectibleNumber);
        }

        if (face.GetComponent<Tile>().hasRocket)
        {
            if (GameManager.Instance.GetPiecesHeld() > 0)
            {
                int pieces = GameManager.Instance.GetPiecesHeld();
                for (int i = 0; i < pieces; i++)
                {
                    GameManager.Instance.SubtractFromPiecesHeld();
                    GameManager.Instance.AddToPiecesRetrieved();
                }
            }
        }
        if (face.GetComponent<Tile>().tileType == Tile.TileType.Ocean)
        {
            face.GetComponent<Tile>().ProcTile();
        }
        yield return new WaitForSeconds(.2f);
        UpdateOcean();
        //UpdateDesert();
        yield return new WaitForSeconds(.3f);

        if (face.GetComponent<Tile>().tileType != Tile.TileType.Ocean)
        {
            face.GetComponent<Tile>().ProcTile();
        }
        yield return new WaitForSeconds(.7f);

        GameManager.Instance.IncrementTurnCount();
        // Iterate through all tiles, and if they are close enough, reveal them
        UpdateFogOfWar();
        isLerping = false;

        // UNSTABLE MODE: A shift happens at the end of every turn!
        if (GameManager.Instance.challengeMode)
        {
            rotationUI.RandomRotation();
        }


        yield return null;

    }

   
}
