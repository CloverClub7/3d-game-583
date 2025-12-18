using UnityEngine;

public class BottleEffect : MonoBehaviour
{
    [Header("Components")]
    public GameObject gun;
    public GameObject player;
    public GameObject scoreUI;
    public float doubleScoreTimeLength = 30f;
    public float doubleDamageTimeLength = 30f;

    [Header("UI")]
    public GameObject damageUI;
    public GameObject comboUI;

    private GunFire gunFire;
    private PlayerHealth playerHealth;
    private ScoreCounter scoreCounter;

    private bool doubleScoreActive = false;
    private bool doubleDamageActive = false;
    private float scoreTimer = 0;
    private float damageTimer = 0;

    void doubleScore()
    {
        if (doubleScoreActive)
        {
            scoreUI.SetActive(true);
            Debug.Log("Double score timer reset.");
            scoreTimer = 0;
        }

        else
        {
            Debug.Log("Double score active.");
            comboUI.SetActive(true);
            scoreCounter.scoreAmount *= 2;
            doubleScoreActive = true;
        }
    }

    void doubleDamage()
    {
        if (doubleDamageActive)
        {
            Debug.Log("Double damage timer reset.");
            damageTimer = 0;
        }

        else
        {
            Debug.Log("Double damage active.");
            damageUI.SetActive(true);
            doubleDamageActive = true;
            gunFire.damage *= 2;
        }
    }

    void Start()
    {
        damageUI.SetActive(false);
        comboUI.SetActive(false);

        gunFire = gun.GetComponent<GunFire>();
        playerHealth = player.GetComponent<PlayerHealth>();
        scoreCounter = scoreUI.GetComponent<ScoreCounter>();
    }

    void FixedUpdate()
    {
        if (doubleScoreActive)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer > doubleScoreTimeLength)
            {
                scoreCounter.scoreAmount /= 2;
                scoreTimer = 0;
                doubleScoreActive = false;
                comboUI.SetActive(false);
                Debug.Log("Double score ended.");
            }
        }

        if (doubleDamageActive)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer > doubleDamageTimeLength)
            {
                gunFire.damage /= 2;
                damageTimer = 0;
                doubleDamageActive = false;
                damageUI.SetActive(false);
                Debug.Log("Double damage ended.");
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bottle"))
        {
            Bottles bottle = collision.gameObject.GetComponent<Bottles>();
            
            switch(bottle.bottleType)
            {
                case 0:
                    playerHealth.currentHealth = playerHealth.maxHealth;
                    Debug.Log("Player health restored.");
                    break;
                case 1:
                    doubleDamage();
                    break;
                case 2:
                    doubleScore();
                    break;
                default:
                    Debug.Log("Bottle has invalid integer value.");
                    break;
            }


            Destroy(collision.gameObject);
        }
    }
}