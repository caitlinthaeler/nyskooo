using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public StatsHandler stats;

    [System.NonSerialized]
    public LevelScriptableObject levelData;

    public LevelController levelController;

    public SpawnHandler spawnHandler;

    [SerializeField]
    private float maxSpeed;

    private Rigidbody2D rb;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float thrust = 5f;  // How fast the spaceship accelerates

    [SerializeField]
    private float drag = 0.5f;  // Air resistance to slow down movement gradually

    [SerializeField]
    private float rotationSmoothing = 5f;  // How smooth the rotation feels

    private int health;

    private CameraHandler ch;

    public CameraHandler Ch { get => ch; set => ch = value; }

    public WorldBoundsHandler wbh;

    private Bounds wb;

    public EarthHandler earthHandler;

    private float goalX;

    private float courseLength;


    // Start is called before the first frame update
    void Start()
    {
        InitPlayer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        GetInput();
        if (health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameSceneManager gameOverScene = FindAnyObjectByType<GameSceneManager>();
        gameOverScene.EndGame();
    }

    private void InitPlayer()
    {
        Debug.Log("level data object: " + levelController.levelData);
        levelData = levelController.levelData;

        Debug.Log(spawnHandler.massScaleFactor);
        
        spawnHandler.InitSpawnHandler(levelController.levelData);

        Debug.Log("spawn handler: " + spawnHandler);
        // player start state
        rb = gameObject.GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, -90);
        rb.drag = drag;

        // initiating health
        stats.HealthBar.InitHealth(levelData);
        health = stats.HealthBar.Health;

        // world bounds and goal
        wb = wbh.WorldBounds;
        Debug.Log("world bounds: min: "+wb.min+", max: "+ wb.max);
        goalX = wb.max.x - levelData.endSpawnOffset;
        courseLength = goalX - rb.position.x;

        stats.levelTextHandler.LevelText = levelData.levelName;

    }

    // basic barebones movement
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("going forwards");

            // implement acceleration at some point
            rb.AddForce(transform.right * thrust);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            Vector3 clampedPos = new Vector3(Mathf.Clamp(rb.position.x, wb.min.x, wb.max.x), Mathf.Clamp(rb.position.y, wb.min.y, wb.max.y));
            if ((Vector3)rb.position != clampedPos)
            {
                rb.position = clampedPos;
            }
            ch.Follow();

            float currentDistanceFromGoal = goalX - rb.position.x;
            float perc = Mathf.Clamp01(currentDistanceFromGoal / courseLength);
            earthHandler.SetEarthPosition(perc);
            stats.DistanceToEarth.UpdateDistanceText(perc);
            float junkLevel = spawnHandler.GetJunkLevel(transform.position.x);
            stats.SpaceJunkLevels.UpdateSpaceJunkLevelsDescription(junkLevel);
        }
        // player could be deccelerating, so need to clamp position inside bounds
        else
        {
            Debug.Log("world bounds"+wb);
            Vector3 clampedPos = new Vector3(Mathf.Clamp(rb.position.x, wb.min.x, wb.max.x), Mathf.Clamp(rb.position.y, wb.min.y, wb.max.y));
            if ((Vector3)rb.position != clampedPos)
            {
                rb.velocity = Vector3.zero;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //rotate counter clockwise
            RotatePlayer(rotationSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //rotate counter clockwise
            RotatePlayer(-rotationSpeed);
        }

    }

    private void RotatePlayer(float degrees)
    {

        // Smooth rotation using Mathf.LerpAngle to avoid instant snapping
       // float targetRotation = rb.rotation + degrees * Time.fixedDeltaTime;
        //rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, rotationSmoothing * Time.fixedDeltaTime));
        
        Transform transform = gameObject.transform;
        float currentRotation = transform.rotation.eulerAngles.z;
        //transform.rotation = Quaternion.Euler(0, 0, currentRotation + degrees);
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, currentRotation + degrees, rotationSmoothing * Time.fixedDeltaTime));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("tag: " + tag);
        if (tag == "Space Junk")
        {
            SpaceJunkHandler junk = collision.gameObject.GetComponent<SpaceJunkHandler>();
            int damage = junk.PlayerDamage;
            health = Mathf.Max(health - damage, 0);
            stats.HealthBar.Health = health;

        }
    }
}
