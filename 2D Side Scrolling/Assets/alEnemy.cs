using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
    public float distance = 3f;
    Rigidbody2D rb;
    Physics2D physics2D;
    public GameObject enemyBullet;
    public int number, hp;
    public GameObject explode; // ตอนตาย

    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        number = Random.Range(1, 250);
        if (number == 5)
        {
            Instantiate(enemyBullet, transform.position, transform.rotation);
        }
        transform.LookAt(target.position);
        if (target.position.x < transform.position.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        // transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f), Space.Self);
        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        }
        if (hp <= 0)
        {
            StartCoroutine(secondDeath(0.2f));
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "weaponA")
        {
            hp -= 3;
        }
    }
    IEnumerator secondDeath(float sec)
    {
        yield return new WaitForSeconds(sec);
        Instantiate(explode, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
