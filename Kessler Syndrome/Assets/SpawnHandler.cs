using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public WorldBoundsHandler worldBoundsHandler;

    [System.NonSerialized]
    public LevelScriptableObject levelData;

    public Player player;

    public float massScaleFactor = 1.5f;

    private Bounds highOrbitBounds;

    private Bounds mediumOrbitBounds;

    private Bounds lowOrbitBounds;

    public Bounds HighOrbitBounds { get => highOrbitBounds; set => highOrbitBounds = value; }

    public Bounds MediumOrbitBounds { get => mediumOrbitBounds; set => mediumOrbitBounds = value; }

    public Bounds LowOrbitBounds { get => lowOrbitBounds; set => lowOrbitBounds = value; }



    private Dictionary<Vector3, GameObject> highOrbitObjectsToSpawn = new Dictionary<Vector3, GameObject>();
    private Dictionary<Vector3, GameObject> mediumOrbitObjectsToSpawn = new Dictionary<Vector3, GameObject>();
    private Dictionary<Vector3, GameObject> lowOrbitObjectsToSpawn = new Dictionary<Vector3, GameObject>();

    private float highOrbitArea;
    private float mediumOrbitArea;
    private float lowOrbitArea;

    private float highOrbitTotalMass;
    private float mediumOrbitTotalMass;
    private float lowOrbitTotalMass;

    


    // Start is called before the first frame update
    void Start()
    {
        //player.spawnHandler = this;
        //levelData = player.levelData;
        //Debug.Log(levelData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSpawnHandler(LevelScriptableObject levelObject)
    {
        levelData = levelObject;
        SetSpawnBounds();
        //generate by section
        GenerateSpawnPoints(highOrbitTotalMass, highOrbitObjectsToSpawn, highOrbitBounds, levelData.highOrbitObjects);
        GenerateSpawnPoints(mediumOrbitTotalMass, mediumOrbitObjectsToSpawn, mediumOrbitBounds, levelData.mediumOrbitObjects);
        GenerateSpawnPoints(lowOrbitTotalMass, lowOrbitObjectsToSpawn, lowOrbitBounds, levelData.lowOrbitObjects);
        //testing
        SpawnObjects(highOrbitObjectsToSpawn);
        SpawnObjects(mediumOrbitObjectsToSpawn);
        SpawnObjects(lowOrbitObjectsToSpawn);

    }

    private void SetSpawnBounds()
    {
        Bounds worldBounds = worldBoundsHandler.WorldBounds;
        Debug.Log("world bounfd" + worldBounds);
        /*
        float totalSpawnWidth = worldBounds.max.x - worldBounds.min.x - levelData.endSpawnOffset;
        Debug.Log("total spawn width: " + totalSpawnWidth);

        // 
        Debug.Log("worldBounds.min.x: "+worldBounds.min.x);
        float highOrbitEdge = worldBounds.min.x + (totalSpawnWidth * (2f / 3f));
        Debug.Log("high orbit edge: " + highOrbitEdge);

        float mediumOrbitEdge = highOrbitEdge + ((worldBounds.max.x - highOrbitEdge) * (2f / 3f));

        float lowOrbitEdge = mediumOrbitEdge + ((worldBounds.max.x - mediumOrbitEdge) * (1f / 3f));

        float upperLimit = worldBounds.max.y;
        float lowerLimit = worldBounds.min.y;

        highOrbitBounds = new Bounds(worldBounds.min, new Vector3(highOrbitEdge, upperLimit));
        mediumOrbitBounds = new Bounds(new Vector3(highOrbitEdge, lowerLimit), new Vector3(mediumOrbitEdge, upperLimit));
        lowOrbitBounds = new Bounds(new Vector3(mediumOrbitEdge, lowerLimit), new Vector3(lowOrbitEdge, upperLimit));

        //these are the volumes which will be used along with the density to determine the total mass to be allocated to objects
        highOrbitArea = highOrbitEdge * (upperLimit - lowerLimit);
        mediumOrbitArea = (highOrbitEdge - mediumOrbitEdge) * (upperLimit - lowerLimit);
        lowOrbitArea = (mediumOrbitEdge - lowOrbitEdge) * (upperLimit - lowerLimit);
        */

        // Calculate total spawn width
        float totalSpawnWidth = worldBounds.max.x - worldBounds.min.x - levelData.endSpawnOffset;
        Debug.Log("total spawn width: " + totalSpawnWidth);

        float highOrbitWidth = totalSpawnWidth * (2f / 3f);
        float mediumOrbitWidth = totalSpawnWidth * (1f / 6f); // 1/6 for medium orbit
        float lowOrbitWidth = totalSpawnWidth * (1f / 6f); // 1/6 for low orbit

        // Set bounds for each orbit
        highOrbitBounds = new Bounds(new Vector3(worldBounds.min.x + (highOrbitWidth / 2), (worldBounds.max.y + worldBounds.min.y) / 2), new Vector3(highOrbitWidth, worldBounds.size.y));
        mediumOrbitBounds = new Bounds(new Vector3(highOrbitBounds.max.x + (mediumOrbitWidth / 2), (worldBounds.max.y + worldBounds.min.y) / 2), new Vector3(mediumOrbitWidth, worldBounds.size.y));
        lowOrbitBounds = new Bounds(new Vector3(mediumOrbitBounds.max.x + (lowOrbitWidth / 2), (worldBounds.max.y + worldBounds.min.y) / 2), new Vector3(lowOrbitWidth, worldBounds.size.y));

        highOrbitArea = highOrbitWidth * worldBounds.size.y;
        mediumOrbitArea = mediumOrbitWidth * worldBounds.size.y;
        lowOrbitArea = lowOrbitWidth * worldBounds.size.y;


        //determine the total mass of each area to allocate to objects
        highOrbitTotalMass = levelData.highOrbitObjectDensity * highOrbitArea * massScaleFactor;
        //Debug.Log("levelData.highOrbitOobjectDensity: " + levelData.highOrbitObjectDensity);
        //Debug.Log("area for high orbit: " + highOrbitArea);
        Debug.Log("Total Mass for high orbit: " + highOrbitTotalMass);
        mediumOrbitTotalMass = levelData.mediumOrbitObjectDensity * mediumOrbitArea * massScaleFactor;
        lowOrbitTotalMass = levelData.lowOrbitObjectDensity * lowOrbitArea * massScaleFactor;
        Debug.Log("Total Mass for low orbit: " + lowOrbitTotalMass);

    }

    private void GenerateSpawnPoints(float totalMass, Dictionary<Vector3, GameObject> objectsToSpawn, Bounds orbitBounds, GameObject[] prefabs)
    {
        //Debug.Log("Generating Spawn Points");
        //do for 3 separate for high orbit, medium orbit, and low orbit
        // first 20km are high orbit (2/3 of total spawn area)
        // second 7km are medium orbit
        // third 3km are low orbit
        // create something to indicate the concentration of space junk

        //create spawn areas

        //calculate number of objects of each type
        //numbers: 60% small objects, 30% medium objects, 10% large Objects
        //mass: 50% large objects, 30% medium objects, 10% small objects
        //generate large objects first, then medium, then small
        //factor in mass
        // density = mass / volume
        // density will be directly proportional to mass

        List<GameObject> smallPrefabs = new List<GameObject>();
        List<GameObject> mediumPrefabs = new List<GameObject>();
        List<GameObject> largePrefabs = new List<GameObject>();
        List<GameObject> specialPrefabs = new List<GameObject>();

        FilterSpaceJunk(prefabs, smallPrefabs, mediumPrefabs, largePrefabs, specialPrefabs);

        List<Vector2> spawnPoints = new List<Vector2>();
        List<float> objectRadii = new List<float>();

        int numLargeObjects = (int)(totalMass * 0.5f / 20);
        if (numLargeObjects >= 1 && largePrefabs.Count >= 1) AddSpawnPoints(spawnPoints, objectRadii, objectsToSpawn, orbitBounds, largePrefabs, numLargeObjects);

        int numMediumObjects = (int)(totalMass * 0.5f / 10);
        if (numMediumObjects >= 1 && mediumPrefabs.Count >= 1) AddSpawnPoints(spawnPoints, objectRadii, objectsToSpawn, orbitBounds, mediumPrefabs, numMediumObjects);

        int numSmallObjects = (int)(totalMass * 0.3f / 5);
        if (numSmallObjects >= 1 && smallPrefabs.Count >= 1) AddSpawnPoints(spawnPoints, objectRadii, objectsToSpawn, orbitBounds, smallPrefabs, numSmallObjects);

        int numSpecialObjects = Random.Range(3, 5);
        if (numSpecialObjects >= 1 && specialPrefabs.Count >= 1) AddSpawnPoints(spawnPoints, objectRadii, objectsToSpawn, orbitBounds, specialPrefabs, numSpecialObjects);
    }

    private void AddSpawnPoints(List<Vector2> spawnPoints, List<float> objectRadii, Dictionary<Vector3, GameObject> objectsToSpawn, Bounds orbitBounds, List<GameObject> prefabs, int numObjects)
    {

        int spawnedObjects = 0;
        while (spawnedObjects < numObjects)
        {
            //Debug.Log("prefabs.Count: " + prefabs.Count);
            GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
            float r = prefab.GetComponent<Collider2D>().bounds.size.magnitude / 2;

            //Debug.Log("calculated radius");

            // generate random point within bounds
            Vector3 randomPoint = new Vector3(
                Random.Range(orbitBounds.min.x, orbitBounds.max.x),
                Random.Range(orbitBounds.min.y, orbitBounds.max.y), -2
            );

            if (IsValidSpawnPoint(spawnPoints, objectRadii, randomPoint, r))
            {
                spawnPoints.Add(randomPoint);
                objectRadii.Add(r);
                // add the prefab to the list of objects to spawn
                objectsToSpawn.Add(randomPoint, prefab);
                spawnedObjects++;
            }
        }
        //Debug.Log("finished adding spawn points for specific sized objects");
    }

    private bool IsValidSpawnPoint(List<Vector2> spawnPoints, List<float> objectRadii, Vector2 point, float r)
    {
        for (int i=0; i<spawnPoints.Count; i++)
        {
            if (Vector2.Distance(spawnPoints[i], point) < (objectRadii[i] + r)) return false;
        }
        return true;
    }

    private void FilterSpaceJunk(GameObject[] prefabs, List<GameObject> smallPrefabs, List<GameObject> mediumPrefabs, List<GameObject> largePrefabs, List<GameObject> specialPrefabs)
    {

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject obj = prefabs[i];
            // probably not needed
            if (obj.tag == "Space Junk")
            {
                float mass = obj.GetComponent<SpaceJunkHandler>().initData.mass;
                Debug.Log("mass: " + mass);
                if (mass <= 5)
                {
                    smallPrefabs.Add(obj);
                }
                else if (mass <= 10)
                {
                    mediumPrefabs.Add(obj);
                }
                else if (mass <= 20)
                {
                    largePrefabs.Add(obj);
                }
                else {
                    specialPrefabs.Add(obj);
                }
            }
        }
    }


    public void SpawnObjects(Dictionary<Vector3, GameObject> objectsToSpawn)
    {
        //Debug.Log("spawning objects");
        foreach (KeyValuePair<Vector3, GameObject> entry in objectsToSpawn)
        {
            Vector3 spawnPosition = entry.Key;
            GameObject objectToSpawn = entry.Value;

            // Generate a random rotation for the Z-axis only
            float randomZRotation = Random.Range(0f, 360f);
            Quaternion zRotation = Quaternion.Euler(0, 0, randomZRotation);

            // Instantiate the object with the random Z-axis rotation
            GameObject obj = Instantiate(objectToSpawn, spawnPosition, zRotation);
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            
        }
    }

    public float GetJunkLevel(float playerPosX)
    {
        
        if (playerPosX <= highOrbitBounds.max.x)
        {
            Debug.Log("player pos: " + playerPosX + " in high orbit at pos: "+highOrbitBounds.max.x);
            return levelData.highOrbitObjectDensity;
        }
        else if (playerPosX <= mediumOrbitBounds.max.x)
        {
            Debug.Log("player pos: " + playerPosX + " in medium orbit at pos: " + mediumOrbitBounds.max.x);
            return levelData.mediumOrbitObjectDensity;
        }
        else if (playerPosX <= lowOrbitBounds.max.x)
        {
            Debug.Log("player pos: " + playerPosX + " in low orbit at pos: " + lowOrbitBounds.max.x);
            return levelData.lowOrbitObjectDensity;
        } else
        {
            Debug.Log("player pos: " + playerPosX + " in earth atmosphere at: " + worldBoundsHandler.WorldBounds.max.x);
            return 0;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 P1 = new Vector2(highOrbitBounds.max.x, highOrbitBounds.max.y);
        Vector3 P2 = new Vector2(highOrbitBounds.max.x, highOrbitBounds.min.y);
        Vector3 P3 = new Vector2(mediumOrbitBounds.max.x, mediumOrbitBounds.max.y);
        Vector3 P4 = new Vector2(mediumOrbitBounds.max.x, mediumOrbitBounds.min.y);
        Vector3 P5 = new Vector2(lowOrbitBounds.max.x, lowOrbitBounds.max.y);
        Vector3 P6 = new Vector2(lowOrbitBounds.max.x, lowOrbitBounds.max.y);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(P1, P2);
        Gizmos.DrawLine(P3, P4);
        Gizmos.DrawLine(P5, P6);
    }
}
