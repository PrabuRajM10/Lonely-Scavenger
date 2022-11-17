
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public static PlayerMov playerMov;
    public GameObject Planet;
    //public GameObject PlayerPlaceholder;


    public float speed = 4,JumpHeight = 1.2f,movSpeed = 3f,value = 6f,acce = 0.05f,maxSpeed = 15f;

    float gravity = 100 , playerSpeed;
    bool OnGround = false ;


    float distanceToGround;
    Vector3 Groundnormal;



    private Rigidbody rb;
    [HideInInspector]public bool canMove;

    private void Awake()
    {
        if (playerMov != null) Destroy(playerMov);
        else playerMov = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerSpeed = movSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        //if(joystick.Vertical >= .5f)
        //{
        //    //float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //    transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //}
        //if (joystick.Vertical <= -.5f)
        //{
        //    //float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //    transform.Translate(-Vector3.forward * speed * Time.deltaTime);


        //}

        if (canMove)
        {
            
            if (playerSpeed < maxSpeed)
            {
                playerSpeed += Time.deltaTime * acce;

            }

            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);



            //Local Rotation


#if UNITY_EDITOR

            if (Input.GetKey(KeyCode.E)) transform.Rotate(0, 200 * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.Q)) transform.Rotate(0, -200 * Time.deltaTime, 0);

#endif

#if UNITY_ANDROID

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x > Screen.width / 2) transform.Rotate(0, 200 * Time.deltaTime, 0);
                else transform.Rotate(0, -200 * Time.deltaTime, 0);
            }

#endif
        }



        //Jump

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.AddForce(transform.up * 40000 * JumpHeight * Time.deltaTime);

        //}



        //GroundControl

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {

            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 0.2f)
            {
                OnGround = true;
            }
            else
            {
                OnGround = false;
            }


        }


        //GRAVITY and ROTATION

        //Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        //if (OnGround == false)
        //{
        //    rb.AddForce(gravDirection * -gravity);

        //}

        ////

        //Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        //transform.rotation = toRotation;

        

    }


    //CHANGE PLANET

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.transform != Planet.transform)
    //    {

    //        Planet = collision.transform.gameObject;

    //        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

    //        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
    //        transform.rotation = toRotation;

    //        rb.velocity = Vector3.zero;
    //        rb.AddForce(gravDirection * gravity);


    //        PlayerPlaceholder.GetComponent<TutorialPlayerPlaceholder>().NewPlanet(Planet);

    //    }

    //}


    public void Reset()
    {
        playerSpeed = movSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "FireBall")
        {
            canMove = false;
            GameManager.gameManager.StopGame();
        }
        else if (collision.collider.gameObject.tag == "Crystal")
        {
            collision.collider.gameObject.SetActive(false);
            GameManager.gameManager.IncrementScore();
        }
    }
}
         