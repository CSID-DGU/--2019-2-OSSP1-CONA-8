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
        };
    }

    public void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0.0f, -followTarget.rotation.eulerAngles.y, 0.0f);
        
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

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
        );//플레이어가 화면 가장자리를 벗어나지 않도록한다.
    }
    
}
