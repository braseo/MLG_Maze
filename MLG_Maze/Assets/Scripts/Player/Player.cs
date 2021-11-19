using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
		//Init healthBar
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
		//If player die, goes to loose menu
		if(currentHealth == 0)
        {
			SceneManager.LoadScene("You Loose");
			SceneManager.UnloadSceneAsync("MainScene");
		}
    }

	void OnTriggerEnter(Collider other)
	{
		//if ennemy hit player, he get dmg
		if (other.name == "Ennemy(Clone)")
			TakeDamage(20);
	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
}
