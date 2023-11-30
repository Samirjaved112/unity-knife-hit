using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ThrowKnife : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;
    private bool isActive = true;
    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Profiler.BeginSample("My Sample",this);
        if (PlayerPrefs.GetInt("StartGame") == 1)
        {
            //Debug.Log(PlayerPrefs.GetInt("StartGame"));
            if (Input.GetMouseButtonDown(0) && isActive)
            {
                rb.AddForce(throwForce, ForceMode2D.Impulse);
                rb.gravityScale = 1;
                GameUi.Instance.DecrementKnifeCount();
            }
        }
        Profiler.EndSample();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
            return;
         isActive = false;
        if (collision.collider.tag == "Log")
        {
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);
            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);
            GameController.Instance.OnKnifeHit(); 
        }
        else if (collision.collider.tag == "Knife")
        {
            rb.velocity = new Vector2(rb.velocity.x, -1);
            GameController.Instance.StartGameOverSequence(false);
        }
    }
}
