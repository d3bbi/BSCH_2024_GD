using System.Collections;
using UnityEngine;

public class CowTriggered : MonoBehaviour
{
    public GameObject heart;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Vector2 position = transform.position;
            position.x -= 0.41f;
            position.y += 1.13f;
            Instantiate(heart, position, Quaternion.identity);
        }
    }

}
