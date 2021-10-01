using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static Player instance;
    //public float force = 100;
    //private Rigidbody prd;
    public bool jump = false;
    public GameObject trigger;
    public AudioClip[] Audios;
    public Animator anim;
    public float Score;
    public float scoreSpeed = 1;
    public TMPro.TextMeshProUGUI ScoreText;

    public float Bone = 0;
    public TMPro.TextMeshProUGUI BoneText;

    public float health = 10f;
    public TMPro.TextMeshProUGUI HealthText;
    
    public string prefHighScore = "High Score";
     public float HighScore;
    public GameObject loosepanel;
    public TMPro.TextMeshProUGUI HighScoreText;

  

    void Start()
    {
        anim = GetComponent<Animator>();
        Score = 0f;
        Bone = 0f;
        health = 10f;
        HighScore = PlayerPrefs.GetInt(prefHighScore, 0);
        Time.timeScale = 1;
        HighScoreText.text = "Your High Score is :" + HighScore;

    }

    void FixedUpdate()
    {
        transform.Translate(0, 0, 0.05f);
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            jump = true;
        }
        else
            jump = false;
        if (jump == true)
        {
            anim.SetBool("isJump", jump);
            transform.Translate(0, 0.1f, 0.1f);
        }
        else if (jump == false)
        {
            anim.SetBool("isJump", jump);

        }

        ScoreText.text = "Score: " + (int)Score;
        Score += Time.deltaTime * 10f * scoreSpeed;
        BoneText.text = Bone + "";
        HealthText.text = health + "";

        if (health <= 0f)
         {
             anim.SetInteger("Animation", 1);
             Debug.Log("You dead");
             loosepanel.SetActive(true);
             Time.timeScale = 0;
         }
    }
    public void AddBone()
    {
        Bone += 1f;
    }
    public void AddHealth()
    {
        health -= 1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Bone")
        {
            AddBone();
            PlayPickUPSound();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            AddHealth();
            PlayHitSound();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("You dead");
            PlayHitSound();
            loosepanel.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void PlayHitSound()
    {
        AudioSource.PlayClipAtPoint(Audios[0], new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
    public void PlayPickUPSound()
    {
        AudioSource.PlayClipAtPoint(Audios[1], new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
    private void OnDisable()
    {
        if (Score > HighScore)
        {
            PlayerPrefs.SetInt(prefHighScore, (int)Score);
        }
    }
}
