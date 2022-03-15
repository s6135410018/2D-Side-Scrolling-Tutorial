using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostEnemy : MonoBehaviour
{
    public Transform target, player;
    public GameObject explode, enemyBullet, gun;
    public float followTime;
    public int flip = 0;
    void Update()
    {
        transform.LookAt(target.position);
        if (player.position.x > transform.position.x)
        {
            flip = 180;
        }
        else
        {
            flip = 0;
        }
        transform.Translate(Vector2.right * 1f * Time.deltaTime);
        transform.eulerAngles = new Vector2(0, flip);
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "follow")
        {
            Instantiate(enemyBullet, gun.transform.position, gun.transform.rotation);
            target.transform.position = new Vector3(Random.Range(-3f,6f), -0.06f, 1f);
        }
        if (other.gameObject.tag == "weaponA")
        {
            StartCoroutine(secondDeath(0.2f));
        }
    }
     IEnumerator secondDeath(float sec)
    {
        yield return new WaitForSeconds(sec);
        Instantiate(explode, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
