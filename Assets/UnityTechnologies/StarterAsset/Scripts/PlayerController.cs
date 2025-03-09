using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Movement
    public InputAction MoveAction;
    public InputAction KeyboardMoveAction;
    public float speed = 3.0f;
    Vector2 move;
    Vector2 keyboardmove;
  

    //Collisions
    Rigidbody2D rigidbody2d;

    //Health
    public int maxHealth = 5;
    int currentHealth;

    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;
    public int health { get { return currentHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        //Activate Movement
        MoveAction.Enable();
        KeyboardMoveAction.Enable();

        //Activate Collisions
        rigidbody2d = GetComponent<Rigidbody2D>();

        //Health
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Arrow Keys
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
        transform.position = position;

        //WASD
        keyboardmove = KeyboardMoveAction.ReadValue<Vector2>();
        Debug.Log(keyboardmove);
        Vector2 keyboardposition = (Vector2)transform.position + keyboardmove * speed * Time.deltaTime;
        transform.position = keyboardposition;

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

    }
    private void FixedUpdate()
    {
        //Collisios For both the movement types
        //Arrow Keys
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        //WASD
        Vector2 keyboardposition = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(keyboardposition);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }
}
