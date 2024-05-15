using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBullet : MonoBehaviour
{
    private float lifeTime = 3f;

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
        if(collision.gameObject.TryGetComponent<Zombie>(out Zombie enemy))
        {
            enemy.TakeDamage(20);
        }
    }
}
