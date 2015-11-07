using UnityEngine;
using System.Collections;

public class Genevieve : MonoBehaviour
{
    private CameraController cameraController;
    private CharacterController characterController;
    private float speed = 5.0f;
    private float gravity = 15.0f;
    private Interactable interactableHeld = null;
    public void Init(CameraController cameraController)
    {
        this.cameraController = cameraController;
        characterController = GetComponent<CharacterController>();
    }

    public void Refresh()
    {
        Vector2 nextMove2d = Vector2.zero;
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            nextMove2d += cameraController.GetDirection();
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            nextMove2d -= cameraController.GetDirection();
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            nextMove2d += cameraController.GetOrthoDirection();
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            nextMove2d -= cameraController.GetOrthoDirection();
        if (nextMove2d.sqrMagnitude > 0.001f)
            nextMove2d = nextMove2d.normalized * speed;
        else
            nextMove2d = Vector3.zero;
        Vector3 nextMove = new Vector3(nextMove2d.x, -gravity, nextMove2d.y);
        characterController.Move(nextMove * Time.deltaTime);
    }
    public void RefreshAfterCameraUpdate()
    {
        Interactable interactable = cameraController.GetInteractable();
        if (interactable != null)
        {
            if (Input.GetMouseButtonDown(0))
                interactable.Interact(this);
            else
                interactable.MouseOver(this);
        }
    }
}
