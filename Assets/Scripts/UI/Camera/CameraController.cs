using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 15f;
    [SerializeField] private float panSpeed = 50f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private Vector2 panLimit = new Vector2(100f, 100f);

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll);

        PanCamera();
    }

    private void ZoomCamera(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment * zoomSpeed, minZoom, maxZoom);
    }

    private void PanCamera()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness || Input.GetKey("w"))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness || Input.GetKey("s"))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness || Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness || Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}