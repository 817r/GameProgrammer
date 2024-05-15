using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float lifeTime = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
        //Debug.Log(lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<playerStats>(out playerStats health))
        {
            health.TakeDamage(20);

            Destroy(gameObject);
        }
    }
}
