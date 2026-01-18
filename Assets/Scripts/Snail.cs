using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Snail : MonoBehaviour
{
    private int layerMask = (1 << 8);
    [SerializeField] private GameObject currentTile;

    [SerializeField] private CubeState cubeState;

    [SerializeField] private float lerpDuration = 1f;

    [SerializeField] private GameObject model;

    private bool isLerping = false;

    private float sideFlag = 0;
    void Start()
    {
        UpdateFogOfWar(); // Need to do this at start of round
        Vector3 newRotation = new Vector3(0,0,0);
        transform.rotation = Quaternion.Euler(newRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.Instance.GetIsMovingSnail() && !isLerping)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                //UnityEngine.Debug.Log(Vector3.Distance(currentTile.transform.position, face.transform.position));
                if(Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1 && face.GetComponent<Tile>().traversable == true 
                    && face.GetComponent<Tile>().currentPassive == null && face.GetComponent<Tile>().tileType != Tile.TileType.Unassigned)
                {
                    float yDistance = currentTile.transform.position.y - face.transform.position.y;
                    float xDistance = currentTile.transform.position.x - face.transform.position.x;
                    float zDistance = currentTile.transform.position.z - face.transform.position.z;

                    UnityEngine.Debug.Log("X DISTANCE: " + xDistance + ", Y DISTANCE: " + yDistance + ", Z DISTANCE: " + zDistance);
                    UnityEngine.Debug.Log("Flag: " + sideFlag);
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
                                sideFlag = 2;
                                UnityEngine.Debug.Log("MOVE TO RIGHT: ");
                                Vector3 currentEuler = new Vector3(0, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance <= -0.4 && yDistance >= 0.4 && zDistance == 0)
                            {
                                sideFlag = 1;
                                UnityEngine.Debug.Log("MOVE TO LEFT: ");
                                Vector3 currentEuler = new Vector3(0, 0, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            break;

                        case 1:
                            UnityEngine.Debug.Log("LEFT!!!!");

                            if (yDistance >= 0.9)
                            {
                                UnityEngine.Debug.Log("DOWN!!!!");

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
                                sideFlag = 0;
                                UnityEngine.Debug.Log("MOVE TO TOP: ");
                                Vector3 currentEuler = new Vector3(0, 180, 0);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance >= 0.4 && yDistance == 0 && zDistance <= -0.4)
                            {
                                sideFlag = 2;
                                UnityEngine.Debug.Log("MOVE TO RIGHT: ");
                                Vector3 currentEuler = new Vector3(-90, -90, -90);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }

                            break;
                        case 2:
                            UnityEngine.Debug.Log("RIGHT!!!!");

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
                                sideFlag = 0;
                                UnityEngine.Debug.Log("MOVE TO TOP: ");
                                Vector3 currentEuler = new Vector3(180, -90, 180);
                                transform.rotation = Quaternion.Euler(currentEuler);
                            }
                            else if (xDistance <= -0.4 && yDistance == 0 && zDistance >= 0.4)
                            {
                                sideFlag = 1;
                                UnityEngine.Debug.Log("MOVE TO LEFT: ");
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
                    face.GetComponent<Tile>().ProcTile(); // TODO; MOVE THIS!

                    // Iterate through all tiles, and if they are close enough, reveal them
                    UpdateFogOfWar();

                    UpdateOcean();
                    UpdateDesert();

                    if (face.GetComponent<Tile>().collectibleOccupant)
                    {
                        face.GetComponent<Tile>().HideShipPiece();
                        GameManager.Instance.AddToPiecesHeld();
                    }

                    if (face.GetComponent<Tile>().hasRocket)
                    {
                        model.SetActive(false);
                        if (GameManager.Instance.GetPiecesHeld() > 0)
                        {
                            int pieces = GameManager.Instance.GetPiecesHeld();
                            for (int i = 0; i < pieces; i++)
                            {
                                GameManager.Instance.SubtractFromPiecesHeld();
                                GameManager.Instance.AddToPiecesRetrieved();
                            }
                            if (GameManager.Instance.GetPiecesRetrieved() == 3)
                            {
                                GameManager.Instance.ResetAllValues();
                                SceneManager.LoadScene(0);
                            }
                        }
                    }
                }
            }
        }
        
    }

    void UpdateFogOfWar()
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
                if(Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1)
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
                        face.GetComponent<Tile>().ProcTile();
                    }
                }
            }
        }
    }

    void UpdateDesert()
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
                        face.GetComponent<Tile>().updateDesert();
                    }
                }
            }
        }
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

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isLerping = false;

        GameManager.Instance.IncrementTurnCount();
    }
}
