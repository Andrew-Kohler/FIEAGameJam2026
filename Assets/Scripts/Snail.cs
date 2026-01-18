using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;

public class Snail : MonoBehaviour
{
    private int layerMask = (1 << 8);
    [SerializeField] private GameObject currentTile;

    [SerializeField] private CubeState cubeState;

    [SerializeField] private float lerpDuration = 1.5f;

    [SerializeField] private GameObject model;

    private int sideFlag = 0;
    // Top = 0, Left = 1, Right = 2, Bottom = 3, Back Left = 4, Back Right = 5

    public event Action<int> OnZoneEntered;
    public event Action<int> OnZoneExited;

    public List<Collider> rotationZones;

    public bool isLerping = false;

    private bool initialUpdate = false;

    public bool isFrosted = false;

    [SerializeField] private Animator snailAnimator;

    void Start()
    {
        UpdateFogOfWar(); // Need to do this at start of round
        Vector3 newRotation = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(newRotation);
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
                    float xDistance = currentTile.transform.position.x - face.transform.position.x;
                    float yDistance = currentTile.transform.position.y - face.transform.position.y;
                    float zDistance = currentTile.transform.position.z - face.transform.position.z;

                    UnityEngine.Debug.Log("X: " + xDistance + ", Y: " + yDistance + ", Z: " + zDistance);

                    StartCoroutine(DoLerpPosition(face.transform.position, lerpDuration));

                    if (Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1)
                        currentTile = face;
                    face.GetComponent<Tile>().ProcTile(); // TODO; MOVE THIS!

                    switch (sideFlag)
                    {
                        case 0:
                            UnityEngine.Debug.Log("TOP!!!!");

                            if (zDistance >= 0.9)
                            {
                                Vector3 currentEuler = new Vector3(0, 90, 0);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (zDistance <= -0.9)
                            {
                                Vector3 currentEuler = new Vector3(0, -90, 0);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance >= 0.9)
                            {
                                Vector3 currentEuler = new Vector3(0, 180, 0);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance <= -0.9)
                            {
                                Vector3 currentEuler = new Vector3(0, 0, 0);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance == 0 && yDistance >= 0.4 && zDistance <= -0.4)
                            {
                                snailAnimator.SetBool("isLerping", true);
                                sideFlag = 2;
                                //UnityEngine.Debug.Log("MOVE TO RIGHT: ");
                                Vector3 currentEuler = new Vector3(0, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);

                            }
                            else if (xDistance <= -0.4 && yDistance >= 0.4 && zDistance == 0)
                            {
                                snailAnimator.SetBool("isLerping", true);
                                sideFlag = 1;
                                //UnityEngine.Debug.Log("MOVE TO LEFT: ");
                                Vector3 currentEuler = new Vector3(0, 0, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);

                            }
                            break;

                        case 1:
                            UnityEngine.Debug.Log("LEFT!!!!");

                            if (yDistance >= 0.9)
                            {
                                Vector3 currentEuler = new Vector3(0, 0, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (yDistance <= -0.9)
                            {
                                Vector3 currentEuler = new Vector3(180, 0, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (zDistance >= 0.9)
                            {
                                Vector3 currentEuler = new Vector3(90, 0, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (zDistance <= -0.9)
                            {
                                Vector3 currentEuler = new Vector3(-90, 0, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance >= 0.4 && yDistance <= -0.4 && zDistance == 0)
                            {
                                snailAnimator.SetBool("isLerping", true);
                                sideFlag = 0;
                                //UnityEngine.Debug.Log("MOVE TO TOP: ");
                                Vector3 currentEuler = new Vector3(0, 180, 0);
                                transform.rotation = Quaternion.Euler(currentEuler);

                            }
                            else if (xDistance >= 0.4 && yDistance == 0 && zDistance <= -0.4)
                            {
                                snailAnimator.SetBool("isLerping", true);
                                sideFlag = 2;
                                //UnityEngine.Debug.Log("MOVE TO RIGHT: ");
                                Vector3 currentEuler = new Vector3(-90, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);

                            }

                            break;
                        case 2:
                            //UnityEngine.Debug.Log("RIGHT!!!!");

                            if (xDistance >= 0.9)
                            {
                                Vector3 currentEuler = new Vector3(-90, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance <= -0.9)
                            {
                                Vector3 currentEuler = new Vector3(90, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (yDistance >= 0.9)
                            {
                                Vector3 currentEuler = new Vector3(0, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (yDistance <= -0.9)
                            {
                                Vector3 currentEuler = new Vector3(180, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance == 0 && yDistance <= -0.4 && zDistance >= 0.4)
                            {
                                snailAnimator.SetBool("isLerping", true);
                                sideFlag = 0;
                                //UnityEngine.Debug.Log("MOVE TO TOP: ");
                                Vector3 currentEuler = new Vector3(180, -90, 180);
                                transform.rotation = Quaternion.Euler(currentEuler);

                            }
                            else if (xDistance <= -0.4 && yDistance == 0 && zDistance >= 0.4)
                            {
                                snailAnimator.SetBool("isLerping", true);
                                sideFlag = 1;
                                //UnityEngine.Debug.Log("MOVE TO LEFT: ");
                                Vector3 currentEuler = new Vector3(90, 90, 0);
                                transform.rotation = Quaternion.Euler(currentEuler);

                            }
                            break;

                        default:
                            UnityEngine.Debug.Log("How did we get here? sideFlag should be 0-2");
                            break;

                    }

                    StartCoroutine(DoLerpPosition(face.transform.position, lerpDuration));

                    if (Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1)
                        currentTile = face;
                    //face.GetComponent<Tile>().ProcTile(); // TODO; MOVE THIS!

                    StartCoroutine(TurnOrder(face));
                }
                

            }
        }
    }



    public void UpdateFogOfWar()
    {
        List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.upTiles, cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                };

        // If the face exists within a side
        foreach (List<GameObject> cubeSide in cubeSides)
        {
            foreach (GameObject face in cubeSide)
            {
                if (Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1)
                {
                    face.GetComponent<Tile>().RevealFromFog();
                }
            }
        }
    }

    void UpdateOcean()
    {
        List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.upTiles, cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                };

        // If the face exists within a side
        foreach (List<GameObject> cubeSide in cubeSides)
        {
            foreach (GameObject face in cubeSide)
            {
                if (face.GetComponent<Tile>().tileType == Tile.TileType.Ocean)
                {
                    {
                        face.GetComponent<Tile>().TickDownOceanTile();
                    }
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

    IEnumerator DoLerpPosition(Vector3 targetPosition, float duration)
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

        yield return null;

    }

   
}
