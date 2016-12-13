using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float speed;
    public Text scoreText;
    public Text winText;

    private Rigidbody rb;
    private int score;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        ChangeText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        Vector3 movement;
        if (Application.isMobilePlatform)
        {
            movement = new Vector3(Input.acceleration.x, 0, -Input.acceleration.z);
        } else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

             movement = new Vector3(moveHorizontal, 0, moveVertical);
        }

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;
        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            ChangeText();
        }
    }

    private void ChangeText()
    {
        scoreText.text = "Score: " + score;
        if(score >= 7)
        {
            winText.text = "YOU WIN!!";
        }
    }
}
