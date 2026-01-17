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
}
