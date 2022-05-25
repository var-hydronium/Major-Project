using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemymovement : MonoBehaviour
{
    public static Enemymovement instance;
    public Transform target;
    public int damage = 2;
    public float speed = 5f;

    public GameObject Bulletprefab;
    private GameObject bullet;
    
    public float obstacleRange = 3.0f;
    Rigidbody rb;
    NavMeshAgent agent;

    public Animator _animator;
    public bool _getHit;
    public bool _die;
    Vector3 pos;

    private void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        instance = this;
        rb = GetComponent<Rigidbody>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        
            // pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        if (!_getHit && !_die)
        {
             pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            _animator.SetBool("Walk Forward", true);
        }

        if (_getHit)
        {
            _animator.SetBool("Walk Forward", false);
            _animator.SetTrigger("Take Damage");
            Invoke( "sethitOff",.5f);
        }
        if (_die)
        {
            _animator.SetBool("Walk Forward", false);
            _animator.SetTrigger("Die");
        }
            rb.MovePosition(pos);
            transform.LookAt(target);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if(Physics.SphereCast(ray,3.75f,out hit))
            {
                GameObject hitobject = hit.transform.gameObject;
                PlayerInfo playerinfo = hit.transform.GetComponent<PlayerInfo>();
                if (hitobject.GetComponent<PlayerInfo>())
                {
                    if(bullet == null )
                    {
                    Debug.Log("shoot");
                        //playerinfo.hurt(damage);
                        bullet = Instantiate(Bulletprefab) as GameObject;
                        bullet.transform.position = transform.TransformPoint(Vector3.forward * 5f);
                        bullet.transform.rotation = transform.rotation;
                    }
                }
               
            }

        
    }
    void OnDrawGizmosSelected()
    {
       // Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, lookradius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<PlayerInfo>().hurt(damage);
        }
    }
    void sethitOff()
    {
        _getHit = false;
        _animator.ResetTrigger("Take Damage");
        
    }
}
