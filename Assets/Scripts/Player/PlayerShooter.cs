using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    private IWeapon weapon;
    private float   fireTimer;

    public void SetWeapon(IWeapon w)
    {
        weapon    = w;
        fireTimer = 0f;
    }

    private void Update()
    {
        if (weapon == null || Mouse.current == null || Camera.main == null) return;

        fireTimer += Time.deltaTime;

        if (Mouse.current.leftButton.isPressed && fireTimer >= 1f / weapon.FireRate)
        {
            fireTimer = 0f;
            Vector2 mouseScreen = Mouse.current.position.ReadValue();
            Vector3 mouseWorld  = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, 10f));
            Vector2 direction   = ((Vector2)mouseWorld - (Vector2)transform.position).normalized;
            weapon.Fire(direction);
        }
    }
}
