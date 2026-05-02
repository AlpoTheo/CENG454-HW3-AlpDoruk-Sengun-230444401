using UnityEngine;

// Owns an IWeapon reference (set at runtime by GameSetup).
// Depends only on the IWeapon interface — never on BaseWeapon or any decorator.
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
        if (weapon == null) return;

        fireTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && fireTimer >= 1f / weapon.FireRate)
        {
            fireTimer = 0f;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction  = (mouseWorld - transform.position).normalized;
            weapon.Fire(direction);
        }
    }
}
