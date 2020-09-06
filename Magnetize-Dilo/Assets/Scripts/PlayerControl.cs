using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    private GameObject closestTower;
    private GameObject hookedTower;
    private bool isPulled = false;

    public UI ui;

    public UnityEvent onCrash;

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        rb.velocity = -transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            onCrash.Invoke();
            //this.gameObject.SetActive(false);
            ui.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Goal")
        {
            ui.Finish();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Tower")
        {
            closestTower = collision.gameObject;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPulled)
            return;

        if (collision.gameObject.tag == "Tower")
        {
            closestTower = null;
            hookedTower = null;

            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void Pull()
    {
        if (!isPulled)
        {
            if(closestTower != null && hookedTower == null)
            {
                hookedTower = closestTower;
            }

            if (hookedTower)
            {
                float distance = Vector2.Distance(hookedTower.transform.position,transform.position);

                Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
                float newPullForce = Mathf.Clamp(pullForce / distance, 30, 100);
                rb.AddForce(pullDirection * newPullForce);

                rb.angularVelocity = -rotateSpeed / distance;
                isPulled = true;
            }
        }
    }

    public void NotPulled()
    {
        rb.angularVelocity = 0;
        isPulled = false;
        hookedTower = null;
    }

    public void ResetLocation()
    {

    }
}
