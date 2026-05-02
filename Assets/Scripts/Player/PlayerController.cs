using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private bool        loggedFirstInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("[PlayerController] Awake — Rigidbody2D " + (rb != null ? "found" : "MISSING"));
    }

    private void Update()
    {
        AimAtMouse();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Keyboard.current == null) return;

        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)    input.y += 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)  input.y -= 1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)  input.x -= 1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x += 1f;

        if (input != Vector2.zero && !loggedFirstInput)
        {
            Debug.Log("[PlayerController] First WASD input detected: " + input);
            loggedFirstInput = true;
        }

        rb.linearVelocity = input.normalized * moveSpeed;
    }

    private void AimAtMouse()
    {
        if (Mouse.current == null || Camera.main == null) return;

        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld  = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, 10f));
        Vector2 dir         = ((Vector2)mouseWorld - (Vector2)transform.position).normalized;
        float   angle       = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation  = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
