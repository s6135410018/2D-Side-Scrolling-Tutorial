using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 1f;
    public float jumpSpeed = 9f;
    public float maxSpeed = 10f;
    public float jumpPower = 20f;
    public bool grounded;
    public float jumpRate = 1f;
    public float nextJumpPress = 0.0f;
    public float fireRate = 0.2f;
    public float nextFireRate = 0.0f;
    private Rigidbody2D rb;
    private Physics2D physics2D;    
    private Animator animator;
    private int healthBar = 100;
    public GameObject hitArea;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator =  this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded", true);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (Input.GetAxis("Horizontal") > 0.1f)
        {
           transform.Translate(Vector2.right * speed * Time.deltaTime);
           transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetButtonDown("Jump") && Time.time > nextJumpPress)
        {
            animator.SetBool("Jump", true);
            nextJumpPress = Time.time + jumpRate;
            rb.AddForce(jumpSpeed * (Vector2.up * jumpPower));
            print(jumpSpeed * (Vector2.up * jumpPower));
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        if (Input.GetKey(KeyCode.P) && Time.time > nextFireRate)
        {
            animator.SetBool("Attack", true);
            nextFireRate = Time.time + fireRate;
            Attack();
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
   public void Attack()
    {
        StartCoroutine(DelaySlash());
    }
    IEnumerator DelaySlash()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(hitArea,rb.position + Vector2.up * -0.3f, transform.rotation);

    }
}
