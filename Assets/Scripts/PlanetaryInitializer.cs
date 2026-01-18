using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetaryInitializer : MonoBehaviour
{
    public CubeState cubeState;

    [Header("Volcano Init Params")]
    [SerializeField] private int numberOfPools = 2;
    [SerializeField] private GameObject volcanoPrefab;
    [SerializeField] private GameObject lavaPoolPrefab;

    [Header("Jungle Init Params")]
    [SerializeField] private int numberOfTrees = 3;
    [SerializeField] private int numberOfFogBushes = 2;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject bushPrefab;
    [SerializeField] private GameObject grassPrefab;

    [Header("Ocean Init Params")]
    [SerializeField] private GameObject oceanRisenPrefab;
    [SerializeField] private GameObject oceanRecededPrefab;

    [Header("Snow Init Params")]
    [SerializeField] private int numberOfRocks = 2;
    [SerializeField] private List<GameObject> snowBlockerPrefabs;
    [SerializeField] private GameObject snowyGroundPrefab;
    [SerializeField] private GameObject snowyRockPrefab;


    [Header("Desert Init Params")]
    [SerializeField] private int numberOfCacti = 2;
    [SerializeField] private GameObject cactiPrefab;
    [SerializeField] private GameObject rocksPrefab;

    [Header("Wheat Init Params")]
    [SerializeField] private GameObject wheatPrefab;

    [Header("Collectable Init Params")]
    [SerializeField] private GameObject shipFinPartPrefab;
    [SerializeField] private GameObject shipWindowPartPrefab;
    [SerializeField] private GameObject shipThingyPartPrefab;
    [SerializeField] private GameObject crashedShipPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeBiomes()
    {
        InitializeIndividualBiome(Tile.TileType.Wheat, cubeState.upTiles); // Always initialize wheat outside the random cycle

        List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.downTiles, cubeState.leftTiles, cubeState.rightTiles, cubeState.frontTiles, cubeState.backTiles
                };

        List<Tile.TileType> listOfTypes = new List<Tile.TileType>{ Tile.TileType.Volcano, Tile.TileType.Snow, Tile.TileType.Ocean, Tile.TileType.Desert, Tile.TileType.Jungle };
        for (int i = 0; i < 5; i++) {
            int index = Random.Range(0, listOfTypes.Count); // Randomly select the biomes of the other sides
            InitializeIndividualBiome(listOfTypes[index], cubeSides[i]);
            listOfTypes.RemoveAt(index); // Remove as we go to prevent dupes
        }

        // Get a list of valid tiles for the ship pieces to spawn on
        List<GameObject> validTiles = new List<GameObject>();

        foreach (List<GameObject> faces in cubeSides)
        {
            foreach(GameObject face in faces)
            {
                if(face.GetComponent<Tile>().tileType == Tile.TileType.Ocean || (face.GetComponent<Tile>().passiveOccupant == null && face.GetComponent<Tile>().tileType != Tile.TileType.Wheat)){
                    validTiles.Add(face);
                }
            }
        }

        int randomIndex1 = Random.Range(0, validTiles.Count);
        validTiles[randomIndex1].GetComponent<Tile>().collectibleOccupant = shipFinPartPrefab;
        validTiles[randomIndex1].GetComponent<Tile>().collectibleNumber = 1;
        validTiles.RemoveAt(randomIndex1);

        int randomIndex2 = Random.Range(0, validTiles.Count);
        validTiles[randomIndex2].GetComponent<Tile>().collectibleOccupant = shipThingyPartPrefab;
        validTiles[randomIndex2].GetComponent<Tile>().collectibleNumber = 2;
        validTiles.RemoveAt(randomIndex2);

        int randomIndex3 = Random.Range(0, validTiles.Count);
        validTiles[randomIndex3].GetComponent<Tile>().collectibleOccupant = shipWindowPartPrefab;
        validTiles[randomIndex3].GetComponent<Tile>().collectibleNumber = 3;
        validTiles.RemoveAt(randomIndex3);

    }

    /// <summary>
    /// The method that controls how each biome is initialized. Great for balance changes!
    /// </summary>
    /// <param name="biomeType"></param>
    /// <param name="tiles"></param>
    public void InitializeIndividualBiome(Tile.TileType biomeType, List<GameObject> tiles)
    {
        // Assign them all their type and change their colors
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<Tile>().AssignType(biomeType);    
        }

        switch (biomeType)
        {
            case Tile.TileType.Wheat:
                for (int i = 0; i < tiles.Count; i++)
                {
                    tiles[i].GetComponent<Tile>().activeOccupant = wheatPrefab;
                    if (i == 4)
                    {
                        tiles[4].GetComponent<Tile>().activeOccupant = crashedShipPrefab;
                        tiles[4].GetComponent<Tile>().hasRocket = true;
                    }
                }
                break;
            case Tile.TileType.Volcano:
                int volcanoSpawn = Random.Range(0, 9);
                for(int i = 0; i < tiles.Count; i++)
                {
                    if(i == volcanoSpawn)
                    {
                        tiles[i].GetComponent<Tile>().passiveOccupant = volcanoPrefab;
                    }
                }
                break;
            case Tile.TileType.Snow:
                List<int> snowTileList = new List<int>(); // Build a list with items representing the amount of each tile we need
                for (int i = 0; i < numberOfRocks; i++)
                {
                    snowTileList.Add(0);
                }
                while (snowTileList.Count < 9)
                {
                    snowTileList.Add(1);
                }

                for (int m = 0; m < tiles.Count; m++)
                {
                    int index = Random.Range(0, snowTileList.Count);
                    switch (snowTileList[index])
                    {
                        case 0:
                            tiles[m].GetComponent<Tile>().passiveOccupant = snowBlockerPrefabs[Random.Range(0, snowBlockerPrefabs.Count)];
                            break;
                        case 1:
                            tiles[m].GetComponent<Tile>().activeOccupant = snowyGroundPrefab;
                            break;
                        default:
                            break;
                    }
                    snowTileList.RemoveAt(index); // Remove as we go, so we ultimately have just as much of everything as we need
                }
                break;

            case Tile.TileType.Jungle:
                List<int> tileList = new List<int>(); // Build a list with items representing the amount of each tile we need
                for (int i = 0; i < numberOfFogBushes; i++)
                {
                    tileList.Add(0);
                }
                //Debug.Log(tileList.Count);
                for (int j = 0; j < numberOfTrees; j++)
                {
                    tileList.Add(1);
                }
                //Debug.Log(tileList.Count);
                while(tileList.Count < 9)
                {
                    tileList.Add(2);
                }
                //Debug.Log(tileList.Count);

                // Spawn all these tiles
                for(int m = 0; m < tiles.Count; m++)
                {
                    int index = Random.Range(0, tileList.Count);
                    switch (tileList[index])
                    {
                        case 0:
                            tiles[m].GetComponent<Tile>().activeOccupant = bushPrefab;
                            break;
                        case 1:
                            tiles[m].GetComponent<Tile>().passiveOccupant = treePrefab;
                            break;
                        case 2:
                            break;
                        default:
                            break;
                    }
                    tileList.RemoveAt(index); // Remove as we go, so we ultimately have just as much of everything as we need
                }

                break;
            case Tile.TileType.Desert:
                break;
            case Tile.TileType.Ocean:
                for (int m = 0; m < tiles.Count; m++)
                {
                    float chance = Random.Range(0, 1.001f);
                    tiles[m].GetComponent<Tile>().turnsUntilSwapCounter = tiles[m].GetComponent<Tile>().turnsUntilSwap;
                    tiles[m].GetComponent<Tile>().activeOccupant = oceanRecededPrefab;
                    tiles[m].GetComponent<Tile>().passiveOccupant = oceanRisenPrefab;
                    if (chance < .5f)
                    {
                        tiles[m].GetComponent<Tile>().traversable = false;
                    }
                    else
                    {
                        tiles[m].GetComponent<Tile>().traversable = true;
                    }
                }
                break;
            default: break;
        }
    }

}
