using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    public InputAction KeyboardMoveAction;
    Vector2 move;
    Vector2 keyboardmove;
    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        KeyboardMoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime;
        transform.position = position;

        keyboardmove = KeyboardMoveAction.ReadValue<Vector2>();
        Debug.Log(keyboardmove);
        Vector2 keyboardposition = (Vector2)transform.position + keyboardmove * 3.0f * Time.deltaTime;
        transform.position = keyboardposition;

    }
    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        Vector2 keyboardposition = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
        rigidbody2d.MovePosition(keyboardposition);
    }
}
