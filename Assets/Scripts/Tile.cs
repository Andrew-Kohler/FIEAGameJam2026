using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public enum TileType { Unassigned, Wheat, Volcano, Snow, Ocean, Desert, Jungle};
    public TileType tileType; // What biome this tile occupies

    public bool isInFogOfWar = true;
    public bool traversable = true;

    public GameObject passiveOccupant; // Things that are intraversable. The snail cannot move onto a space with a passive occupant.
    public GameObject activeOccupant;  // Things that move around the stage (that aren't the snail). These are...just dust devils.
    public GameObject collectibleOccupant;

    [Header("Ocean Data")]
    public int turnsUntilSwap = 2;
    public int turnsUntilSwapCounter;

    [Header("Volcano Data")]
    private float chanceToMeteor = .3f;
    [SerializeField] private GameObject meteor;

    public GameObject currentPassive;
    public GameObject currentActive;
    public GameObject currentCollectible;

    public bool hasRocket = false;

    public delegate void OnRockImpact();
    public static event OnRockImpact onRockImpact;



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
            if (tileType == TileType.Ocean)
            {
                if (currentPassive != null) Destroy(currentPassive);
                if (currentActive != null) Destroy(currentActive);
                if (traversable)
                {
                    Debug.Log("ACTIVE SPAWN FROM FOG");
                    currentActive = Instantiate(activeOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                }
                else
                {
                    Debug.Log("PASSIVE SPAWN FROM FOG");
                    currentPassive = Instantiate(passiveOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                }
                if (collectibleOccupant != null)
                {
                    Debug.Log("COLLECT SPAWN FROM FOG");
                    currentCollectible = Instantiate(collectibleOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                }
            }
            else
            {
                if (passiveOccupant != null)
                {
                    Debug.Log("PASSIVE SPAWN FROM FOG - NONOCEAN");
                    if (currentPassive != null) Destroy(currentPassive);
                    currentPassive = Instantiate(passiveOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                }
                if (activeOccupant != null)
                {
                    Debug.Log("ACTIVE SPAWN FROM FOG - NONOCEAN");
                    if (currentActive != null) Destroy(currentActive);
                    currentActive = Instantiate(activeOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                }
                if (collectibleOccupant != null)
                {
                    Debug.Log("COLLECT SPAWN FROM FOG - NONOCEAN");
                    if (currentCollectible != null) Destroy(currentCollectible);
                    currentCollectible = Instantiate(collectibleOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                }
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
        if (currentCollectible != null)
        {
            currentCollectible.GetComponent<Animator>().Play("Shrink", 0, 0);
        }
        isInFogOfWar = true;
    }

    public void HideShipPiece()
    {
        if (collectibleOccupant != null)
        {
            Destroy(currentCollectible);
            collectibleOccupant = null;
        }
    }

    public void updateDesert()
    {
        if (currentActive == null)
        {
            float chance = Random.Range(0, 1.0f);
            if (chance < .3f && GameManager.Instance.GetDustDevilCount() < 3)
            {
                if (!isInFogOfWar)
                {
                    GameManager.Instance.SetDustDevilCount(GameManager.Instance.GetDustDevilCount() + 1);
                    if (currentActive != null) Destroy(currentActive);
                    currentActive = Instantiate(activeOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                }
            }
        }
    }

    public void AssignType(TileType type)
    {
        tileType = type;
        switch (type)
        {
            case TileType.Volcano:
                GetComponent<MeshRenderer>().material.color = Color.darkRed;
                break;
            case TileType.Snow:
                GetComponent<MeshRenderer>().material.color = Color.whiteSmoke;
                break;
            case TileType.Jungle:
                GetComponent<MeshRenderer>().material.color = Color.darkOliveGreen;
                break;
            case TileType.Wheat:
                GetComponent<MeshRenderer>().material.color = Color.goldenRod;
                break;
            case TileType.Ocean:
                GetComponent<MeshRenderer>().material.color = Color.cornflowerBlue;
                break;
            case TileType.Desert:
                GetComponent<MeshRenderer>().material.color = Color.orangeRed;
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
                if (change < chanceToMeteor)
                {
                    StartCoroutine(DoMagmaBall());

                }
                FindFirstObjectByType<Snail>().isFrosted = false;
                break;
            case TileType.Snow:
                if (FindFirstObjectByType<Snail>().isFrosted)
                {
                    FindFirstObjectByType<Snail>().isFrosted = false;
                    GameManager.Instance.SetHealth(GameManager.Instance.GetHealth() - 1);
                }
                else
                {
                    FindFirstObjectByType<Snail>().isFrosted = true;
                }
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
                FindFirstObjectByType<Snail>().isFrosted = false;
                break;
            case TileType.Wheat:
                // No effects proc on the wheat tile
                FindFirstObjectByType<Snail>().isFrosted = false;
                break;
            case TileType.Ocean: // Called when all tiles across the world are proc'd
                turnsUntilSwapCounter--;

                if(turnsUntilSwapCounter == 0)
                {
                    turnsUntilSwapCounter = turnsUntilSwap;
                    traversable = !traversable;
                    if (!isInFogOfWar)
                    {
                        //SendToFog();
                        //isInFogOfWar = false;
                        if (currentPassive != null) Destroy(currentPassive);
                        if (currentActive != null) Destroy(currentActive);
                        if (!traversable) // The passive is the water, the active is the one you can stand on
                        {
                            Debug.Log("PASSIVE SPAWN FROM REFRESH");
                            currentPassive = Instantiate(passiveOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                        }
                        else
                        {
                            Debug.Log("ACTIVE SPAWN FROM REFRESH");
                            currentActive = Instantiate(activeOccupant, this.transform.position, this.transform.parent.rotation, this.transform.parent);
                        }
                    }
                }
                //FindFirstObjectByType<Snail>().isFrosted = false;
                break;
            case TileType.Desert:
                if (currentActive != null)
                {
                    Destroy(currentActive);
                    GameManager.Instance.SetDustDevilCount(GameManager.Instance.GetDustDevilCount() - 1);
                    // How do we get the player to a random, valid tile

                    CubeState cubeState = FindFirstObjectByType<CubeState>();

                    List<GameObject> validTiles = new List<GameObject>();

                    List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                    {
                        cubeState.upTiles, cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                    };
                    foreach (List<GameObject> faces in cubeSides)
                    {
                        foreach (GameObject face in faces)
                        {
                            if (face.GetComponent<Tile>().currentPassive == null && face.GetComponent<Tile>().tileType != TileType.Desert)
                            {
                               validTiles.Add(face);
                            }
                        }
                    }

                    int index = Random.Range(0, validTiles.Count);

                    // Waiting until Chrys is done to finish implementing this
                }
                //FindFirstObjectByType<Snail>().isFrosted = false;

                break;
            default:
                break;
        }
    }

    IEnumerator DoMagmaBall()
    {
        // TODO: Make a nice timed-out coroutine doing an animation for this tile
        Vector3 playerPos = FindFirstObjectByType<Snail>().transform.position;

        Vector3 spawnPos = new Vector3(playerPos.x, playerPos.y + 10f, playerPos.z);

        GameObject newMeteor = Instantiate(meteor, spawnPos, meteor.transform.rotation);

        while(newMeteor.transform.position.y > playerPos.y)
        {
            newMeteor.transform.position = new Vector3(playerPos.x, newMeteor.transform.position.y - 6 * Time.deltaTime, playerPos.z);
            yield return null;
        }
        Destroy(newMeteor);
        GameManager.Instance.SetHealth(GameManager.Instance.GetHealth() - 1);
        onRockImpact?.Invoke();
        yield return null;
    }
}


