using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float speed;
    public Text scoreText;
    public Text livesText;
    public Text finalText;
    public Text pauseText;
    public int lives;
    public Camera pauseCamera;
    public Camera playCamera;

    private Rigidbody rb;
    private int score;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        ChangeScoreText();
        ChangeLivesText();
        finalText.text = "";
    }

    void FixedUpdate()
    {
        if(transform.position.y >= 0)
        {
            Vector3 movement;
            if (Application.isMobilePlatform)
            {
                movement = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
            }
            else
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                movement = new Vector3(moveHorizontal, 0, moveVertical);
            }

            movement = Camera.main.transform.TransformDirection(movement);
            movement.y = 0;
            rb.AddForce(movement * speed);

        }
    }

    void Update()
    {
        if(transform.position.y < -5)
        {
            lives--;
            ChangeLivesText();
            transform.position = new Vector3(0, 0, 0);

            //removes residual forces acquired while falling
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            speed += 2;
            score++;
            ChangeScoreText();
        }
    }

    private void ChangeScoreText()
    {
        scoreText.text = "Score: " + score;
        if(score >= 7)
        {
            Destroy(this.gameObject);
            finalText.text = "YOU WIN!!";
        }
    }

    private void ChangeLivesText()
    {
        livesText.text = "Lives left: " + lives;
        if (lives == 0)
        {
            Destroy(this.gameObject);
            finalText.text = "YOU LOSE";
        }
    }

    public void OnPauseAndResume()
    {
        
        if(pauseText.text.Equals("Pause"))
        {
            pauseCamera.gameObject.SetActive(true);
            playCamera.gameObject.SetActive(false);
            pauseText.text = "Resume";
            Time.timeScale = 0;
            
        } else
        {
            playCamera.gameObject.SetActive(true);
            pauseCamera.gameObject.SetActive(false);
            Time.timeScale = 1;
            pauseText.text = "Pause";
        }
    }
}
