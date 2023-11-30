using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 hitPosition;
    [SerializeField]
    private Vector2 startPosition;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Knife")
        {
           
            if (gameObject.activeInHierarchy)
            {
            StartCoroutine(ChangePosition());

            }
        }

    }
    private IEnumerator ChangePosition()
    {
        transform.localPosition = hitPosition;
        yield return new WaitForSeconds(0.05f);
        transform.localPosition = startPosition;
    }


}

