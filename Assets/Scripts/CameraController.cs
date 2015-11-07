using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Transform childCam;
    private Genevieve genevieve;
    //private Vector3 genevieveToPivotPoint = new Vector3(0, );
    private float scrollSpeed = 1.0f;
    private float genevieveToCamDistance = 3.0f;
    Vector2 mouseAbsolute;
    Vector2 smoothMouse;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public CursorLockMode lockCursor = CursorLockMode.Locked;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;

    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    // Yaw rotation will affect this object instead of the camera if set.
    public GameObject characterBody;
    public void Init(Genevieve genevieve) // Main callback
    {
        this.genevieve = genevieve;
        childCam = transform.GetChild(0);
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (characterBody) targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
    }
    public void Refresh() // Main callback
    {
        genevieveToCamDistance = Mathf.Clamp(genevieveToCamDistance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, 2.0f, 20.0f);

        /*Vector2 orientationChange = Vector2.zero;
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x < Screen.width && Input.mousePosition.y >= 0 && Input.mousePosition.y < Screen.height && !Input.GetKey(KeyCode.Space))
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            if (lastMousePos.x >= 0)
                orientationChange = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - lastMousePos;
            lastMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        else
            lastMousePos = new Vector2(-1, -1);

        if (orientationChange.x != 0)
            genevieveToCamDirection = Quaternion.Euler(0, orientationChange.x * 0.4f, 0) * genevieveToCamDirection;
        if (orientationChange.y != 0)
        {
            Vector2 projectedDir = new Vector2(genevieveToCamDirection.x, genevieveToCamDirection.z);
            if (projectedDir.sqrMagnitude == 0)
                projectedDir = new Vector2(1, 0);
            else
                projectedDir = new Vector2(projectedDir.y, -projectedDir.x);
            genevieveToCamDirection = Quaternion.AngleAxis(orientationChange.y * 0.4f, new Vector3(projectedDir.x, 0, projectedDir.y)) * genevieveToCamDirection;
        }*/
        childCam.transform.localPosition = new Vector3(0, 0, -genevieveToCamDistance);

        if (genevieve != null)
        {
            transform.position = genevieve.transform.position;
            //transform.rotation = Quaternion.LookRotation(-genevieveToCamDirection);
        }
        // Ensure the cursor is always locked when set
        Cursor.lockState = lockCursor;
        Cursor.visible = false;

        // Allow the script to clamp based on a desired target value.
        Quaternion targetOrientation = Quaternion.Euler(targetDirection);
        Quaternion targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        mouseAbsolute += smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            mouseAbsolute.x = Mathf.Clamp(mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        Quaternion xRotation = Quaternion.AngleAxis(-mouseAbsolute.y, targetOrientation * Vector3.right);
        transform.localRotation = xRotation;

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            mouseAbsolute.y = Mathf.Clamp(mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        transform.localRotation *= targetOrientation;

        // If there's a character body that acts as a parent to the camera
        if (characterBody)
        {
            Quaternion yRotation = Quaternion.AngleAxis(mouseAbsolute.x, characterBody.transform.up);
            characterBody.transform.localRotation = yRotation;
            characterBody.transform.localRotation *= targetCharacterOrientation;
        }
        else
        {
            Quaternion yRotation = Quaternion.AngleAxis(mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
    }


    public Vector2 GetDirection()
    {
        Vector2 r = new Vector2(genevieve.transform.position.x - childCam.transform.position.x, genevieve.transform.position.z - childCam.transform.position.z);
        if (r.sqrMagnitude < 0.01f)
            return new Vector2(0, 1);
        return r.normalized;
    }
    public Vector2 GetOrthoDirection()
    {
        Vector2 r = new Vector2(genevieve.transform.position.z - childCam.transform.position.z, childCam.transform.position.x - genevieve.transform.position.x);
        if (r.sqrMagnitude < 0.01f)
            return new Vector2(1, 0);
        return r.normalized;
    }
}
