using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bomb;
    // how long does it take to explode
    public float timeToExplode = 3f;
    // number of bombs the player has
    public int numberOfBombs = 1;
    // how many bombs are remaining
    private int bombsRemaining = 0;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionTime = 1f;
    public int explosionRadius = 1;

    [Header("Distructable")]
    public Tilemap distructibleTiles;
    public Distructable destructiblePrefab;


    // Start is called before the first frame update
    private void OnEnable()
    {
        bombsRemaining = numberOfBombs;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombsRemaining > 0)
        {
            StartCoroutine(PlaceBomb());
        }
    }

    // method to place the bomb
    private  IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;

        // create a bomb at the player's position
        GameObject bombInstance = Instantiate(bomb, position, Quaternion.identity);
        bombsRemaining--;

        // Wait for the bomb to explode
        yield return new WaitForSeconds(timeToExplode);

        position = bombInstance.transform.position;

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionTime);

        float sizeExplotion = 0.7f;

        Explode(position, new Vector2(0,sizeExplotion), explosionRadius);
        Explode(position, new Vector2(0, -sizeExplotion), explosionRadius);
        Explode(position, new Vector2(-sizeExplotion, 0), explosionRadius);
        Explode(position, new Vector2(sizeExplotion, 0), explosionRadius);

        bombsRemaining++;
        // remove the object
        Destroy(bombInstance);
    }

    // method to explode the bomb (recursive method)
    private void Explode(Vector2 position, Vector2 direction, int length)
    {   
        // if the length is less than or equal to 0, return
        if (length <= 0) {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionTime);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = distructibleTiles.WorldToCell(position);
        TileBase tile = distructibleTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            distructibleTiles.SetTile(cell, null);
        }
    }

    public void AddBombs(int bombsToAdd)
    {
        numberOfBombs += bombsToAdd;
        bombsRemaining += bombsToAdd;
    }

}
