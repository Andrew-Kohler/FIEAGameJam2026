using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

public class CubeState : MonoBehaviour
{
    public List<GameObject> frontTiles = new List<GameObject>();
    public List<GameObject> backTiles = new List<GameObject>();
    public List<GameObject> leftTiles = new List<GameObject>();
    public List<GameObject> rightTiles = new List<GameObject>();
    public List<GameObject> upTiles = new List<GameObject>();
    public List<GameObject> downTiles = new List<GameObject>();

    public Snail snail;
    private void OnEnable()
    {
        Tile.onRockImpact += RockImpact;
    }

    private void OnDisable()
    {
        Tile.onRockImpact -= RockImpact;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                // Parent of tile = cube
                // Parent of cube = the middle cube now
                face.transform.parent.transform.parent.transform.parent = cubeSide[4].transform.parent.transform.parent;
            }

            // If the player is on this face, or on its rim, they get carried
            if (face == snail.GetCurrentTile() || Vector3.Distance(face.transform.position, snail.GetCurrentTile().transform.position) < 0.75f)
            {
                snail.gameObject.transform.parent = cubeSide[4].transform.parent.transform.parent;
            }
        }

        Debug.Log(cubeSide[4].transform.parent.transform.parent.name);
        cubeSide[4].transform.parent.transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        snail.gameObject.transform.parent = this.GetComponentInChildren<SelectFace>().gameObject.transform;
        snail.UpdateFogOfWar();

        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
            {
                Debug.Log("Parent before " + littleCube.transform.parent.transform.parent.transform.parent.name);
                littleCube.transform.parent.transform.parent.transform.parent = pivot;
                Debug.Log("Parent after " + littleCube.transform.parent.transform.parent.transform.parent.name);

            }
        }
    }

    private void RockImpact()
    {
        StartCoroutine(DoRockImpact());
    }

    private IEnumerator DoRockImpact()
    {
        GetComponent<Animator>().Play("HoverRumble", 0, 0);
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().Play("HoverIdle", 0, 0);
    }
}
