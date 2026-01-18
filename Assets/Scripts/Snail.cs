using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class Snail : MonoBehaviour
{
    private int layerMask = (1 << 8);
    [SerializeField] private GameObject currentTile;

    [SerializeField] private CubeState cubeState;

    [SerializeField] private float lerpDuration = 1f;

    [SerializeField] private GameObject model;

    private bool isLerping = false;

    public int PieceCount = 0;
    void Start()
    {
        UpdateFogOfWar(); // Need to do this at start of round
        Vector3 newRotation = new Vector3(0,0,0);
        transform.rotation = Quaternion.Euler(newRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !isLerping && !GameManager.Instance.GetIsRotatingPiece())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                //UnityEngine.Debug.Log(Vector3.Distance(currentTile.transform.position, face.transform.position));
                if(Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1 && face.GetComponent<Tile>().traversable == true 
                    && face.GetComponent<Tile>().passiveOccupant == null && face.GetComponent<Tile>().tileType != Tile.TileType.Unassigned)
                {
                    float yDistance = currentTile.transform.position.y - face.transform.position.y;
                    float xDistance = currentTile.transform.position.x - face.transform.position.x;
                    float ZDistance = currentTile.transform.position.z - face.transform.position.z;

                    UnityEngine.Debug.Log("DISTANCE: " + yDistance);
                    if (yDistance == 0.5)
                    {
                        UnityEngine.Debug.Log("UP: " + yDistance);
                        Vector3 currentEuler = transform.eulerAngles;
                        currentEuler.x = 90;
                        transform.rotation = Quaternion.Euler(currentEuler);
                    }
                    else if (yDistance == -0.5)
                    {
                        UnityEngine.Debug.Log("DOWN: " + yDistance);
                        Vector3 currentEuler = transform.eulerAngles;
                        currentEuler.x = - 90;
                        transform.rotation = Quaternion.Euler(currentEuler);
                    }
                    StartCoroutine(DoLerpPosition(face.transform.position, lerpDuration));

                    if (Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1)
                    currentTile = face;
                    face.GetComponent<Tile>().ProcTile(); // TODO; MOVE THIS!

                    // Iterate through all tiles, and if they are close enough, reveal them
                    UpdateFogOfWar();

                    // Need to proc all ocean tiles globally
                    UpdateOcean();
                    UpdateDesert();

                    if (face.GetComponent<Tile>().hasRocket)
                    {
                        model.SetActive(false);
                        if(GameManager.Instance.GetPiecesHeld() > 0)
                        {
                            for(int i = 0; i < PieceCount; i++)
                            {
                                GameManager.Instance.SubtractFromPiecesHeld();
                                GameManager.Instance.AddToPiecesRetrieved();
                            }
                            if(GameManager.Instance.GetPiecesRetrieved() == 3)
                            {

                            }
                        }
                    }
                    else
                    {
                        model.SetActive(true);
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
