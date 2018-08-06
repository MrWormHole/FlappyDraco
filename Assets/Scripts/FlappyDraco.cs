using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlappyDraco : MonoBehaviour {

    public static FlappyDraco instance;

    [SerializeField]
    private Rigidbody2D rgb2D;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float forwardSpeed = 3f; 

    [SerializeField]
    private float bounceSpeed = 6f;

    private bool didFlap;

    public bool isAlive;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapClick, pointClip, diedClip;

    [SerializeField]
    private int score;
    public Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isAlive = true;
        score = 0;
        scoreText.text = "Score: 0";
        rgb2D = GetComponent<Rigidbody2D>();
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => FlapTheBird());

        setCamerasX();
    }

	void FixedUpdate () {

        if(isAlive)
        {
            Vector3 temp = transform.position;
            temp.x +=forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if(didFlap) {
                didFlap = false;
                rgb2D.velocity = new Vector2(0, bounceSpeed);
                anim.SetTrigger("Flap");
                audioSource.PlayOneShot(flapClick);
            } 

            if(rgb2D.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -rgb2D.velocity.y / 8);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
		
	}
    //utilities start here
    void setCamerasX()
    {
        CameraFollow.offsetX = (Camera.main.transform.position.x - transform.position.x) - 0.2f;
    }
    public float getPositionX()
    {
        return transform.position.x;
    }

    public void FlapTheBird() {
        didFlap = true;
    }
    //utilities end here

    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
        {
            if(isAlive)
            {
                isAlive = false;
                Destroy(rgb2D);
                anim.SetTrigger("Die");
                audioSource.PlayOneShot(diedClip);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "PipeHolder")
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            audioSource.PlayOneShot(pointClip);
        }
    }
}
