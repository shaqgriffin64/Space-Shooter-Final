using UnityEngine;
using System.Collections;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public AudioClip Fire;
    public AudioClip Teleport;
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float teleportRate;


    private AudioSource audioSource;
    private Rigidbody rb;
    private float nextFire;
    private float nextTeleport;

    //end of testing variables

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        audioSource = GetComponent<AudioSource>();
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            audioSource.volume = 1f;
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.clip = Fire;
            audioSource.loop = false;
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time > nextTeleport)
            {
                audioSource.volume = 0.15f;
                nextTeleport = Time.time + teleportRate;
                Vector3.Lerp(transform.position, transform.position += new Vector3(-2, 0, 0), speed);
                audioSource.clip = Teleport;
                audioSource.loop = false;

                audioSource.Play();
            }
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextTeleport)
            {
                audioSource.volume = 0.15f;
                nextTeleport = Time.time + teleportRate;
                transform.position += new Vector3(2, 0, 0);
                audioSource.clip = Teleport;
                audioSource.loop = false;

                audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.H))
            {
                fireRate = 0.4f;
                teleportRate = 4;
                speed = 8;
            }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        //controls the player's tilt in game, set as a public value
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        }
    }

}