using System.Collections;
using UnityEngine;

public class CowTriggered : MonoBehaviour
{
    public GameObject heart;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            for (int i = 0; i < 10; i++)
            {
                StartCoroutine(InstantiateHeart(transform.position, transform.rotation));
            }
        }
    }

    IEnumerator InstantiateHeart(Vector2 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(0.1f);
        position.x += Random.Range(-1f, 14f);
        position.y += Random.Range(-1f, 14f);
        Instantiate(heart, position, rotation);
    }
}
