using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private Animator _animator;
    private bool walking;
    private Vector3 playerToMouse; //almasena el eje xyz
    private int floorMask;
    private float camRayLength=100f;
    private float speed = 2f;
    private Rigidbody _rigidbody;
    private Vector3 move;
    public int lives = 10;
    public Text txtLives;
    private float timeLimit = 7f;
    

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Animation(h,v);
        MyRotation();
        Moving(h,v);
    }

    void Update()
    {
        if (lives <= 0)
        {
            if (timeLimit > 1)
            {
                timeLimit -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    void Animation(float h, float v)
    {
        walking = h != 0f || v != 0f;
        _animator.SetBool("isWalking",walking);
    }

    void Moving(float h, float v)
    {
     move.Set(h,0f,v);
     move = move.normalized * speed * Time.deltaTime;
     
     if (_rigidbody)_rigidbody.MovePosition(transform.position+move);

    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor"); 
        txtLives.text = "Lives: "+lives.ToString();
    }

    void MyRotation()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _rigidbody.MoveRotation(newRotation);
        }
    }
    

    public void Attack()
    {
        lives -=1;
        txtLives.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            _animator.SetTrigger("death");
            GetComponent<CapsuleCollider>().isTrigger = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
} 
