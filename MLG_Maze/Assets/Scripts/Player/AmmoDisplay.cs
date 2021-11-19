using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public static int ammoNumber;
    public bool isFiring;

    // Update is called once per frame
    private void Start()
    {
        //Init pocket ammo player
        ammoNumber = 15;
    }
    void Update()
    {
        //Ammo number for ui
        ammoText.text = ammoNumber.ToString();
      

        //Reloading protection, cant reload if no ammo

        if(ammoNumber > 0 && Gun.currentAmmo !=0 && Input.GetKeyDown(KeyCode.R) && Gun.currentAmmo != Gun.magazineSize)
        {
            ammoNumber = ammoNumber - (Gun.magazineSize - Gun.currentAmmo);
        }
    }
}
