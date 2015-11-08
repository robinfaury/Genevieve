using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    private Transform childCam;
    private Transform cameraPivotPoint;
    private Transform cameraAnchor;
    private Genevieve genevieve;
    private float scrollSpeed = 1.0f;
    private float genevieveToCamDistance = 3.0f;
    Vector2 mouseAbsolute;
    Vector2 smoothMouse;

    //public Vector2 clampInDegrees = new Vector2(360, 180);
    public Vector2 angleRangeX = new Vector2(0, 0);
    public Vector2 angleRangeY = new Vector2(-80, 70);
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);

    public void Init(Genevieve genevieve) // Main callback
    {
        this.genevieve = genevieve;
        cameraPivotPoint = transform.GetChild(0);
        cameraAnchor = cameraPivotPoint.GetChild(0);
        childCam = cameraAnchor.GetChild(0);
    }
    public void UpdateCamera() // Main callback
    {
        if (gameManager.running)
            genevieveToCamDistance = Mathf.Clamp(genevieveToCamDistance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, 0.1f, 2.0f);

        transform.position = genevieve.transform.position;

        childCam.transform.localPosition = new Vector3(0, 0, -genevieveToCamDistance);

        if (gameManager.running)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Vector2 mouseDelta;
        if (gameManager.running)
            mouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
        else
            mouseDelta = Vector2.zero;

        smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        mouseAbsolute += smoothMouse;

        if (angleRangeX.x < angleRangeX.y)
            mouseAbsolute.x = Mathf.Clamp(mouseAbsolute.x, angleRangeX.x, angleRangeX.y);
        if (angleRangeY.x < angleRangeY.y)
            mouseAbsolute.y = Mathf.Clamp(mouseAbsolute.y, angleRangeY.x, angleRangeY.y);

        transform.localRotation = Quaternion.AngleAxis(mouseAbsolute.x, Vector3.up);
        cameraPivotPoint.localRotation = Quaternion.AngleAxis(-mouseAbsolute.y, Vector3.right);
    }


    public Vector2 GetDirection()
    {
        Vector2 r = new Vector2(cameraAnchor.transform.position.x - childCam.transform.position.x, cameraAnchor.transform.position.z - childCam.transform.position.z);
        if (r.sqrMagnitude < 0.001f)
            return new Vector2(0, 1);
        return r.normalized;
    }
    public Vector2 GetOrthoDirection()
    {
        Vector2 r = new Vector2(cameraAnchor.transform.position.z - childCam.transform.position.z, childCam.transform.position.x - cameraAnchor.transform.position.x);
        if (r.sqrMagnitude < 0.001f)
            return new Vector2(1, 0);
        return r.normalized;
    }

    public Interactable GetInteractable(out Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            point = hit.point;
            return hit.transform.gameObject.GetComponent<Interactable>();
        }
        point = Vector3.zero;
        return null;
    }
}
