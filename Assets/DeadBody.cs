using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = (10f * new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f))).normalized + -(Vector2)transform.right * 10f;
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(0f,180f);
    }
    public void SpawnBodyPart(GameObject bodyPart)
    {
        Instantiate(bodyPart, transform.position, transform.rotation);
    }   
}
