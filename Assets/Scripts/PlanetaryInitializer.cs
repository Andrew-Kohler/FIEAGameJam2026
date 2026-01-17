using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetaryInitializer : MonoBehaviour
{
    public CubeState cubeState;

    [Header("Volcano Init Params")]
    [SerializeField] private GameObject volcanoPrefab;

    [Header("Desert Init Params")]
    [SerializeField] private int numberOfCacti = 2;

    [Header("Jungle Init Params")]
    [SerializeField] private int numberOfTrees = 3;
    [SerializeField] private int numberOfFogBushes = 2;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject bushPrefab;

    [Header("Snow Init Params")]
    [SerializeField] private int numberOfIceTiles = 2;
    [SerializeField] private int numberOfRocks = 3;
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
                break;
            case Tile.TileType.Jungle:
                List<int> tileList = new List<int>(); // Build a list with items representing the amount of each tile we need
                for (int i = 0; i < numberOfFogBushes; i++)
                {
                    tileList.Add(0);
                }
                Debug.Log(tileList.Count);
                for (int j = 0; j < numberOfTrees; j++)
                {
                    tileList.Add(1);
                }
                Debug.Log(tileList.Count);
                while(tileList.Count < 9)
                {
                    tileList.Add(2);
                }
                Debug.Log(tileList.Count);

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
                break;
            default: break;
        }
    }

}
