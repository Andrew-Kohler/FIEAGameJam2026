using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class Snail : MonoBehaviour
{
    private int layerMask = (1 << 8);
    [SerializeField] private GameObject currentTile;

    [SerializeField] private CubeState cubeState;

    [SerializeField] private float lerpDuration = 1f;

    private bool isLerping = false;
    void Start()
    {
        UpdateFogOfWar(); // Need to do this at start of round
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
                Debug.Log(Vector3.Distance(currentTile.transform.position, face.transform.position));
                if(Vector3.Distance(currentTile.transform.position, face.transform.position) <= 1.1 && face.GetComponent<Tile>().traversable == true 
                    && face.GetComponent<Tile>().passiveOccupant == null && face.GetComponent<Tile>().tileType != Tile.TileType.Unassigned)
                {
                    StartCoroutine(DoLerpPosition(face.transform.position, lerpDuration));

                    currentTile = face;
                    face.GetComponent<Tile>().ProcTile(); // TODO; MOVE THIS!

                    // Iterate through all tiles, and if they are close enough, reveal them
                    UpdateFogOfWar();
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
