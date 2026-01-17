using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType { Unassigned, Wheat, Volcano, Snow, Ocean, Desert, Jungle};
    public TileType tileType; // What biome this tile occupies

    public bool isInFogOfWar = true;
    public bool traversable = true;

    public GameObject passiveOccupant; // Things that are intraversable. The snail cannot move onto a space with a passive occupant.
    public GameObject activeOccupant;  // Things that move around the stage (that aren't the snail). These are...just dust devils.

    [Header("Ocean Data")]
    private int turnsUntilSwap = 2;
    private int turnsUntilSwapCounter;

    [Header("Volcano Data")]
    private float chanceToMeteor = .5f;

    GameObject currentPassive;
    GameObject currentActive;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealFromFog()
    {
        if (isInFogOfWar)
        {
            isInFogOfWar = false;
            if (passiveOccupant != null)
            {
                if (currentPassive != null) Destroy(currentPassive);
               currentPassive = Instantiate(passiveOccupant, this.transform.position, this.transform.parent.localRotation, this.transform.parent);
            }
            if (activeOccupant != null)
            {
                if (currentActive != null) Destroy(currentActive);
                currentActive = Instantiate(activeOccupant, this.transform.position, this.transform.parent.localRotation, this.transform.parent);
            }
        }
        
    }

    public void SendToFog()
    {
        if (currentPassive != null)
        {
            currentPassive.GetComponent<Animator>().Play("Shrink",0,0);
        }
        if (currentActive != null)
        {
            currentActive.GetComponent<Animator>().Play("Shrink",0,0);
        }
        isInFogOfWar = true;
    }

    public void AssignType(TileType type)
    {
        tileType = type;
        switch (type)
        {
            case TileType.Volcano:
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case TileType.Snow:
                GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case TileType.Jungle:
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case TileType.Wheat:
                GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case TileType.Ocean:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case TileType.Desert:
                GetComponent<MeshRenderer>().material.color = Color.orange;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Procs this tile's effects.
    /// </summary>
    public void ProcTile()
    {
        switch (tileType)
        {
            case TileType.Volcano:
                float change = Random.Range(0, 1.001f);
                if(change > chanceToMeteor)
                {
                    // TODO: Make a nice timed-out coroutine doing an animation for this tile
                    GameManager.Instance.SetHealth(GameManager.Instance.GetHealth() - 1);
                }
                break;
            case TileType.Snow:

                break;
            case TileType.Jungle:
                if(activeOccupant != null)
                {
                    CubeState cubeState = FindFirstObjectByType<CubeState>();

                    List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                    {
                        cubeState.upTiles, cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                    };
                    foreach(List<GameObject> faces in cubeSides)
                    {
                        foreach(GameObject face in faces)
                        {
                            if (!face.GetComponent<Tile>().isInFogOfWar)
                            {
                                // Remove anything that was on the tile and send it back into the fog
                                face.GetComponent<Tile>().SendToFog();
                            }
                        }
                    }
                }
                break;
            case TileType.Wheat:
                // No effects proc on the wheat tile
                break;
            case TileType.Ocean:
                // No INDIVIDUAL effects proc on the water tile
                break;
            case TileType.Desert:
                // No INDIVIDUAL effects proc on the desert tile
                break;
            default:
                break;
        }
    }
}
