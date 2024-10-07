using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public WorldBoundsHandler worldBoundsHandler;

    private Camera c;

    // target is the player
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float cameraSpeed;

    private Vector3 followSpeed;

    private Bounds cameraBounds;

    public Bounds CameraBounds { get => cameraBounds; }

    private float minX;

    private float minY;

    private float maxX;

    private float maxY;

    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private float smoothTime = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        worldBoundsHandler.InitBounds();
        InitProperties();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        c = Camera.main;
    }

    private void FixedUpdate()
    {
        Follow();
    }

    private void InitProperties()
    {
        c = GetComponent<Camera>();
        target.gameObject.GetComponent<Player>().Ch = this;
        // get world bounds coordinates
        cameraBounds = GetCameraBounds(c.orthographicSize, c.aspect);
        Debug.Log(cameraBounds);
        minX = cameraBounds.min.x;
        minY = cameraBounds.min.y;
        maxX = cameraBounds.max.x;
        maxY = cameraBounds.max.y;
        c.transform.position = target.position + offset;
        // follow player and clamp to coordinates
        followSpeed = new Vector3(cameraSpeed, cameraSpeed, 0);

    }

    public void Follow()
    {
       // Debug.Log("following");
        // determine the new position
        Vector3 newPos = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minY, maxY), -10);

        // smoothly pan the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref followSpeed, smoothTime);
    }

    public Bounds GetCameraBounds(float orthographicSize, float aspect)
    {

        float h = 2 * orthographicSize;
        float w = 2 * orthographicSize * aspect;

        Bounds b = worldBoundsHandler.WorldBounds;
        Debug.Log("world bounds for camera: "+b.min+", "+b.max);
        Vector3 minBound = new Vector3(b.min.x + (w / 2), b.min.y + (h / 2), -10);
        Vector3 maxBound = new Vector3(b.max.x - (w / 2), b.max.y - (h / 2), -10);

        cameraBounds.SetMinMax(minBound, maxBound);
        return cameraBounds;
    }

    private void OnDrawGizmos()
    {
        Vector3 P1 = new Vector2(minX, minY);
        Vector3 P2 = new Vector2(minX, maxY);
        Vector3 P3 = new Vector3(maxX, maxY);
        Vector3 P4 = new Vector3(maxX, minY);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(P1, P2);
        Gizmos.DrawLine(P2, P3);
        Gizmos.DrawLine(P3, P4);
        Gizmos.DrawLine(P4, P1);
    }
}
