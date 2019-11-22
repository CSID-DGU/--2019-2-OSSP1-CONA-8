using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
    public Transform followTarget; //imagetarget을 따라가기 위해
    public float positionFactor = 7f;

    public float speed;
    public float tilt;
    public Done_Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public Renderer renderer3D;

    private float nextFire;

    void Awake()
    {
        renderer3D = GetComponent<Renderer>();

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x);
    }

    public void Update()
    {
        if (renderer3D.enabled && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //GetComponent<Rigidbody>().velocity = movement * speed;

        //GetComponent<Rigidbody>().position = new Vector3
        //(
        //    Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
        //    0.0f,
        //    Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        //);

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    public void FixedUpdate()
    {
        //transform.rotation = followTarget.rotation; //imagetarget의 회전을 따라간다.
       
        transform.rotation = Quaternion.Euler(0.0f, -followTarget.rotation.eulerAngles.y, 0.0f);


        //TODO 
        // player boundary 수정
        if (transform.position.x <= boundary.xMax && transform.position.x >= boundary.xMin)
        {
            //위치 이동은 좌우로만 따라가도록 한다.
            transform.position = new Vector3(
                followTarget.position.x * positionFactor,
                transform.position.y,
                transform.position.z
            );
        }
        else if (transform.position.x < boundary.xMin)
        {
            transform.position = new Vector3(
            boundary.xMin,
            transform.position.y,
            transform.position.z
            );
        }
        else if (transform.position.x > boundary.xMax)
        {
            transform.position = new Vector3(
            boundary.xMax,
            transform.position.y,
            transform.position.z
            );
        }
    }

    //void FixedUpdate ()
    //{
    //	float moveHorizontal = Input.GetAxis ("Horizontal");
    //	float moveVertical = Input.GetAxis ("Vertical");

    //	Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
    //	GetComponent<Rigidbody>().velocity = movement * speed;

    //	GetComponent<Rigidbody>().position = new Vector3
    //	(
    //		Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
    //		0.0f, 
    //		Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
    //	);

    //	GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    //}
}
