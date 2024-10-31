using UnityEngine;

public class GunController1 : MonoBehaviour
{
    [SerializeField] private Animator gunAnimation;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDitance = 1.5f;

    private bool gunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - gun.position;

        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDitance, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(direction);
        }

        GunFlipController(mousePos);
    }

    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if (mousePos.x > gun.position.x && !gunFacingRight)
            GunFlip();
    }

    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    private void Shoot(Vector3 direction)
    {
        gunAnimation.SetTrigger("Shoot");

        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * bulletSpeed;

        Destroy(newBullet, 5 );
    }
}
