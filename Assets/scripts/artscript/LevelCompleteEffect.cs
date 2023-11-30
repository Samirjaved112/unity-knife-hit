using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteEffect : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D[] childRigidbodies;
    public float forceMagnitude;
    private void Start()
    {
        forceMagnitude = Random.Range(15f, 25f);
        StartCoroutine(PlayEffect());
    }

    public IEnumerator PlayEffect()
    {
        Vector2[] forceDirections = new Vector2[]
       {
            Vector2.left,
            new Vector2(-1,1),
            Vector2.up,
            new Vector2(1,-1),
            new Vector2(1, 1).normalized,  
           Vector2.right,
            Vector2.up,
            Vector2.down
       };
        for (int i = 0; i < childRigidbodies.Length; i++)
        {
            childRigidbodies[i].AddForce(forceDirections[i] * forceMagnitude, ForceMode2D.Impulse);   
        }
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < childRigidbodies.Length; i++)
        {
            childRigidbodies[i].gravityScale = 30f;
        }
    }
}
