using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int lives = 3;
    public float respawnTime = 3.0f;
    private float safetyTime = 3.0f;
    public ParticleSystem explosion;
    public float score = 0;
    public GameObject scoreboard;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject highscoretext;
    private float highscore = 0;
    public GameObject gameover;
    public GameObject restarttext;
    public GameObject _spawner;
    public GameObject _asteroid;
    

    Text scoreNumber;
    Text highscoreNumber;

    public void Awake()
    {
        scoreNumber = scoreboard.GetComponent<Text>();
        highscoreNumber = highscoretext.GetComponent<Text>();
    }
    public void AsteroidDestroyed(ASteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.8f)
        {
            this.score += 3;
            Debug.Log("Score + 3");
        }
        else if (asteroid.size < 1.15f)
        {
            this.score += 2;
            Debug.Log("Score + 2");
        }
        else if (asteroid.size < 1.5f)
        {
            this.score += 1;
            Debug.Log("Score + 1");
        }

        scoreNumber.text = score.ToString();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;
        if (this.lives < 0)
        {
            GameOver();
        } else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
        HeartsUI();
    }
    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void Respawn()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Safety");
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
        this.gameover.gameObject.SetActive(false);
        this.restarttext.gameObject.SetActive(false);

        Invoke(nameof(TurnOnCollisions), this.safetyTime);
    }

    private void GameOver()
    {
        this.player.gameObject.SetActive(false);
        Highscore();
        this.gameover.gameObject.SetActive(true);
        this.restarttext.gameObject.SetActive(true);
        this.lives = 3;
        this.score = 0;
        scoreNumber.text = score.ToString();
        HeartsUI();
        Invoke(nameof(Respawn), 3.5f);
    }

    public void HeartsUI()
    {
        if (this.lives == 3)
        {
            this.life1.gameObject.SetActive(true);
            this.life2.gameObject.SetActive(true);
            this.life3.gameObject.SetActive(true);
        }
        else if (this.lives == 2)
        {
            this.life1.gameObject.SetActive(true);
            this.life2.gameObject.SetActive(true);
            this.life3.gameObject.SetActive(false);
        }
        else if (this.lives == 1)
        {
            this.life1.gameObject.SetActive(true);
            this.life2.gameObject.SetActive(false);
            this.life3.gameObject.SetActive(false);
        }
        else if (this.lives == 0)
        {
            this.life1.gameObject.SetActive(false);
            this.life2.gameObject.SetActive(false);
            this.life3.gameObject.SetActive(false);
        }
    }

    public void Highscore()
    {
        if (this.score > this.highscore)
        {
            highscoreNumber.text = score.ToString();
            this.highscore = this.score;
        }
    }
}
