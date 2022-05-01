using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{
    [Header("Touche")]
    public InputAction fire;
    public InputAction reload;

    public float timeBetweenShooting = 1f;
    public float range = 100f;
    public float reloadTime = 1f;

    public int magazineSize = 8;
    public int bullertsPerTap = 1;
    private int bulletsLeft;

    private bool readyToShoot;
    private bool reloading;

    public Transform attackPoint;
    public RaycastHit rayHit;
    public TextMeshProUGUI magazineInfoText;
    public TextMeshProUGUI reloadingInfoText;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        magazineInfoText.SetText(bulletsLeft + " / " + magazineSize);

        fire.performed += context => {
            if (readyToShoot && !reloading && bulletsLeft > 0) OnShoot();

            if (bulletsLeft <= 0) OnReload();
        };
        reload.performed += context => {
            if (!reloading && bulletsLeft == magazineSize) OnReload();
        };
    }

    private void OnShoot()
    {
        Debug.Log("Fireee");

        readyToShoot = false;

        Vector3 direction = attackPoint.forward;

        //RayCast
        if (Physics.Raycast(attackPoint.position, direction, out rayHit, range))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("monster"))
            {
                Destroy(rayHit.collider.gameObject);
            }
                
        }

        //Graphics
        // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        magazineInfoText.SetText(bulletsLeft + " / " + magazineSize);

        Invoke("ResetShot", timeBetweenShooting);
    }

    private void OnReload()
    {
        Debug.Log("Reloaddd");

        reloading = true;
        reloadingInfoText.SetText("Reloading ...");

        Invoke("ReloadFinished", reloadTime);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloadingInfoText.SetText("");
        magazineInfoText.SetText(bulletsLeft + " / " + magazineSize);

        reloading = false;
    }

    private void OnEnable()
    {
        fire.Enable();
        reload.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
        reload.Disable();
    }
}
