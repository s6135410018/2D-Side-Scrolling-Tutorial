using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int healthBar = 100;
    public Text healthText;
    public GameObject hitArea;
    public Slider sliderHp;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator =  this.gameObject.GetComponent<Animator>();
        sliderHp.maxValue = healthBar;
        sliderHp.value = healthBar;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HEALTH: " + healthBar;
        if (healthBar <= 0)
        {
            healthBar = 0;
            animator.SetTrigger("Death");
        }
        sliderHp.value = healthBar;
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(10);
        }
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
    void TakeDamage(int damage)
    {
        healthBar -= damage;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "health")
        {
            healthBar += 50;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "deathzone")
        {
            healthBar = 0;
        }
        if (other.gameObject.tag == "enemy")
        {
            TakeDamage(10);
        }
    }
}
