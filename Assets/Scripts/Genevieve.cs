using UnityEngine;
using System.Collections;

public class Genevieve : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 5.0f;
    private float gravity = 15.0f;
    public void Init()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Refresh()
    {
        Vector3 nextMove = Vector3.zero;
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            nextMove.z += 1.0f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            nextMove.z -= 1.0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            nextMove.x += 1.0f;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            nextMove.x -= 1.0f;
        if (nextMove.sqrMagnitude > 0.001f)
            nextMove = nextMove.normalized * speed;
        else
            nextMove = Vector3.zero;
        nextMove.y = -gravity;
        characterController.Move(nextMove * Time.deltaTime);
    }
}
