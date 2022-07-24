using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBossWalls : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject wall, cage;

    [SerializeField] float speed;
    [SerializeField] float maxTime;
     public bool wallDown;

    float time;
    EnemyHealth enemyHealth;
    void Start()
    {
        enemyHealth = GameObject.Find("death").GetComponent<EnemyHealth>();
        time = maxTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (time <= 0 || !wallDown)
            return;

        time -= Time.deltaTime;

        wall.transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    void Update()
    {
        if (enemyHealth.isDead)
            cage.GetComponent<Rigidbody2D>().gravityScale = 1;
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            wallDown = true;
    }
}
