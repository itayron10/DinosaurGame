using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeSpawner : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] TreeGroup[] treeGroups; // all the tree groups that this tree spawner will spawn

    [Header("Settings")]
    [SerializeField] Vector3 spawnAreaOffset; // the offset from the world position of this gameoject that will be applied to the spawning area
    [SerializeField] Vector3 spawnAreaSize; // the size of this spawning area
    [SerializeField] int minSpawnHeight = 0; // any tree spawns under this height get destroyed

    void Start() => SpawnTrees();

    private void SpawnTrees()
    {
        if (treeGroups.Length < 1) { return; }
        // loop all the tree groups and spawn the tree group
        for (int i = 0; i < treeGroups.Length; i++) SpawnTreeGroup(treeGroups[i]);
    }

    private void SpawnTreeGroup(TreeGroup treeGroup)
    {
        // loop all the times we need to spawn a tree for this tree group based on this tree group's tree amount
        for (int i = 0; i < treeGroup.treeAmount; i++)
            SpawnTree(treeGroup.treePrefab, treeGroup.rotateToGround);
    }

    private void SpawnTree(GameObject treePrefab, bool rotateToGround)
    {
        // calculate the correction radius based on the y size 
        float CorrectionRadius = spawnAreaSize.y;

        CalculateFirstAndLastPositions(out Vector3 firstPos, out Vector3 lastPos);
        Vector3 randomPos = GetRandomPosBetwennTwoPoints(firstPos, lastPos);

        Vector3 treeSpawnPos = NavmeshHandeler.GetClosestPointOnNavmesh(randomPos, CorrectionRadius);


        
        if (treeSpawnPos.y < minSpawnHeight) { return; }

        GameObject treeInstance = Instantiate(treePrefab, treeSpawnPos, Quaternion.identity, transform);
        if (rotateToGround)
            treeInstance.transform.up = NavmeshHandeler.GetNormal(treeSpawnPos, Vector3.down);
        treeInstance.transform.rotation = CalculateRandomTreeRotation(treeInstance.transform);

    }

    private Quaternion CalculateRandomTreeRotation(Transform objectToRotate)
    {
        return new Quaternion(objectToRotate.rotation.x, Random.rotation.y, objectToRotate.rotation.z, objectToRotate.rotation.w);
    }

    private void CalculateFirstAndLastPositions(out Vector3 firstPos, out Vector3 lastPos)
    {
        // calculate the first and last pos that the tree can spawn in based on the terrain world position, the spawn area offset and the spawn size
        firstPos = spawnAreaOffset - spawnAreaSize / 2 + transform.position;
        lastPos = spawnAreaOffset + spawnAreaSize / 2 + transform.position;
    }

    private static Vector3 GetRandomPosBetwennTwoPoints(Vector3 firstPos, Vector3 lastPos)
    {
        // get a random position inside the spawning area 
        return new Vector3(Random.Range(firstPos.x, lastPos.x),
            Random.Range(firstPos.y, lastPos.y),
            Random.Range(firstPos.z, lastPos.z));
    }

    private void OnDrawGizmosSelected()
    {
        // draw the spawning area
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(spawnAreaOffset + transform.position, spawnAreaSize);
    }
}

[System.Serializable]
class TreeGroup
{
    public string treeGroupName; // used for easier editor workflow
    public GameObject treePrefab; // this tree prefab will be spawned from this tree group
    public int treeAmount; // how much times to spawn the tree of this tree group
    public bool rotateToGround = false;
}
