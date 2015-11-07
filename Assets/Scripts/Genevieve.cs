using UnityEngine;
using System.Collections;

public class Genevieve : MonoBehaviour
{
    [HideInInspector]
    public CameraController cameraController;
    private CharacterController characterController;
    private float baseSpeed = 2.0f;
    [HideInInspector]
    public float speed = 2.0f;
    private float gravity = 5.0f;
    private Vector2 facingDirection = new Vector2(0, 1);
    private Vector2 wantedFacingDirection = new Vector2(0, 1);
    private Interactable interactableAimed = null;
    private Interactable interactableHeld = null;
    private Animator animator;
    private string[] anims = new string[] { "armature|idle", "armature|walk" };
    private int lastAnim = -1;
    [HideInInspector]
    public int animToPlay;
    public void Init(CameraController cameraController)
    {
        this.cameraController = cameraController;
        characterController = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void UpdatePosition()
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
        {
            nextMove2d = nextMove2d.normalized * speed;
            wantedFacingDirection = nextMove2d;
            animToPlay = 1;
        }
        else
        {
            nextMove2d = Vector3.zero;
            animToPlay = 0;
        }
        Vector3 nextMove = new Vector3(nextMove2d.x, -gravity, nextMove2d.y);
        characterController.Move(nextMove * Time.deltaTime);
        speed = baseSpeed;

        float angleFacingDirection = Mathf.Atan2(facingDirection.y, facingDirection.x);
        float angleWantedFacingDirection = Mathf.Atan2(wantedFacingDirection.y, wantedFacingDirection.x);
        angleFacingDirection = Mathf.MoveTowardsAngle(angleFacingDirection * Mathf.Rad2Deg, angleWantedFacingDirection * Mathf.Rad2Deg, 360.0f * Time.deltaTime) * Mathf.Deg2Rad;
        facingDirection = new Vector2(Mathf.Cos(angleFacingDirection), Mathf.Sin(angleFacingDirection));
        transform.rotation = Quaternion.LookRotation(new Vector3(facingDirection.x, 0, facingDirection.y));
    }
    public void UpdateAfterCamera()
    {
        if(interactableHeld != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                interactableHeld.Throw(this);
                interactableHeld = null;
            }
            else
            {
                interactableHeld.Held(this);
            }
        }
        else
        {
            Vector3 hitPoint;
            Interactable interactable = cameraController.GetInteractable(out hitPoint);
            if (interactableAimed != interactable)
            {
                if (interactableAimed != null)
                    interactableAimed.MouseLeave(this);
                if (interactable != null)
                    interactable.MouseEnter(this);
            }
            if (interactable != null)
            {
                interactable.MouseAimed(this);
                if (Input.GetMouseButtonDown(0) && interactable.IsCloseEnough(this))
                {
                    interactableHeld = interactable;
                    interactableHeld.Take(this);
                }
            }
            interactableAimed = interactable;
        }
    }
    public void UpdateAnims()
    {
        if (animToPlay != lastAnim)
            animator.CrossFade(anims[animToPlay], 0.1f);
        lastAnim = animToPlay;
    }
}
