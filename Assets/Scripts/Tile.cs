using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType { Wheat, Volcano, Snow, Ocean, Desert, Jungle};
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
               GameObject passive = Instantiate(passiveOccupant, this.transform.parent.position, this.transform.parent.rotation, this.transform.parent);
            }
        }
        
    }

    public void AssignType(TileType type)
    {
        tileType = type;
        switch (type)
        {
            case TileType.Volcano:
                GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log("Initialized volcano");
                break;
            case TileType.Snow:
                GetComponent<MeshRenderer>().material.color = Color.white;
                Debug.Log("Initialized snow");
                break;
            case TileType.Jungle:
                GetComponent<MeshRenderer>().material.color = Color.green;
                Debug.Log("Initialized jungle");
                break;
            case TileType.Wheat:
                GetComponent<MeshRenderer>().material.color = Color.yellow;
                Debug.Log("Initialized wheat");
                break;
            case TileType.Ocean:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                Debug.Log("Initialized ocean");
                break;
            case TileType.Desert:
                GetComponent<MeshRenderer>().material.color = Color.orange;
                Debug.Log("Initialized desert");
                break;
            default:
                break;
        }
    }
}
