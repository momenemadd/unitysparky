using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int health = 3;
    public int lives = 3;
    public static int score = 0;
    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;
public int coins=0;
public TextMeshProUGUI coinCounter;

public TextMeshProUGUI liveCounter;

public Image healthBar;
public float maxHealth = 3f;
    private SpriteRenderer sr;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    private bool isRespawning = false;  // Prevent multiple respawns in quick succession

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log($"PlayerStats: Start (health={health}, lives={lives}, sr={(sr==null?"null":"ok")})");
    }

    void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
        {
            flickerTime = flickerTime + Time.deltaTime;
        }
        else if (flickerTime >= flickerDuration)
        {
            sr.enabled = !(sr.enabled);
            flickerTime = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"PlayerStats: TakeDamage called (damage={damage}, isImmune={isImmune}, health={health})");

        if (isImmune == false)
        {
            health = health - damage;
            if (health < 0)
                health = 0;
                healthBar.fillAmount=health / maxHealth;

            if (lives > 0 && health == 0)
            {
                Debug.Log($"PlayerStats: Health reached 0, respawning (lives remaining={lives - 1})");
                // Guard against multiple respawns in quick succession
                if (!isRespawning)
                {
                    isRespawning = true;
                    FindObjectOfType<LevelManager>().RespawnPlayer();
                    health = 3;
                    lives--;
                    healthBar.fillAmount =1f;
                    isRespawning = false;
                }
            }
            else if (lives == 0 && health == 0)
            {
                Debug.Log("Gameover");
                  PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Gameover");
                            }

            Debug.Log("Player Health:" + health.ToString());
            Debug.Log("Player Lives:" + lives.ToString());
            Debug.Log($"PlayerStats: Damage applied ({damage}), immunity started");

            // Start immunity only when damage was actually applied
            isImmune = true;
            immunityTime = 0f;
        }
        else
        {
            Debug.Log("PlayerStats: TakeDamage ignored - currently immune");
        }

    }

    void Update()
    {
        // Debug helper: press K to simulate taking 1 damage
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("PlayerStats: Debug key K pressed - forcing TakeDamage(1)");
            TakeDamage(1);
        }

        if (isImmune == true)
        {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                sr.enabled = true;
                    Debug.Log("PlayerStats: Immunity ended");
            }
        }
        coinCounter.text = " " + score.ToString();
        liveCounter.text = " " + lives.ToString();
    }
}

