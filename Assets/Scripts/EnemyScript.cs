using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
   public float speed;
   public bool moveRight;

    // Update is called once per frame
    void Update()
    {
        if(moveRight)
        {
            transform.Translate(2 * Time.deltaTime *speed , 0, 0);
            transform.localScale = new Vector2 (.35f,.35f);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime *speed , 0, 0);
            transform.localScale = new Vector2 (-.35f,.35f);
        }
    }
    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("Turn"))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }
    }
}
