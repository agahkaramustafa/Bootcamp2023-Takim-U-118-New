using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerMaxHealth = 100;

    private float currentHealth;
    Animator anim;
    PlayerUI playerUIScript;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerUIScript = FindObjectOfType<PlayerUI>();
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        currentHealth = playerMaxHealth;
        playerUIScript.UpdateHealthBar(playerMaxHealth, currentHealth);        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            TakeDamage(100);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerUIScript.UpdateHealthBar(playerMaxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            // deathhandler script
            playerUIScript.gameObject.SetActive(false);
            anim.SetTrigger("IsDead");
            gameManager.RestartLevel();
        }
    }
}
