
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables Declaration
    public float damage;
    public float range;

    
    public static int magazineSize;
    public static int currentAmmo;
    private bool UIReload;

    private GameObject[] IUIAmmo;
    public GameObject IUIElements;
    public GameObject BulletHole;
    
    private ParticleSystem muzzleFlash;

    public Camera fpsCam;
    #endregion

    //Init gun values
    private void Start()
    {
        magazineSize = 7;
        muzzleFlash = transform.GetChild(0).GetComponent<ParticleSystem>();

        currentAmmo = magazineSize;

        //Get IUI element wich are child of gun
        IUIAmmo = new GameObject[IUIElements.transform.childCount];
        for (int i = 0; i < IUIElements.transform.childCount; i++)
        {
            IUIAmmo[i] = IUIElements.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        //hit trigger
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            IUI();
        }
        //Call reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            UIReload = true;
            IUI();
        } 
    }

    public void Shoot()
    {
        //Cant shoot during pause
        if (!PauseMenu.isPaused)
        {
            if (currentAmmo > 0)
            {
                //update ammo 
                currentAmmo--;
                muzzleFlash.Play();

                //Instant a raycast to hit other gameobject
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    Debug.Log(hit.transform.name);
                    //get the target components
                    Target target = hit.transform.GetComponent<Target>();

                    if (hit.transform.CompareTag("Ennemy"))
                    {
                        target.TakeDamage(damage);
                    }
                    //Instantiate(BulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
    }

    void Reload()
    {
        //Cant reload full mag if no enough ammo
        if(AmmoDisplay.ammoNumber < magazineSize)
        {
            currentAmmo = AmmoDisplay.ammoNumber;
            AmmoDisplay.ammoNumber = 0;
            UIReload = true;
        }
        //Full reload
        if(AmmoDisplay.ammoNumber > magazineSize)
        {
            UIReload = true;
            currentAmmo = magazineSize;
            AmmoDisplay.ammoNumber = AmmoDisplay.ammoNumber - magazineSize;
        }
        //Cant reload no enough ammo
        if(AmmoDisplay.ammoNumber == 0)
        {
            UIReload = false;
            return;
        }
    }

    //IUI update on gun 3DModel
    void IUI()
    {
        if(UIReload == true)
        {
            for(int i = 0; i < IUIAmmo.Length; i++)
            {
                IUIAmmo[i].GetComponent<Renderer>().material.color = Color.green;
            }
            UIReload = false;
        }
        IUIAmmo[currentAmmo].GetComponent<Renderer>().material.color = Color.red; 
    }
}
