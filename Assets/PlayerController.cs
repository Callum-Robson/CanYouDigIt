using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Tilemap destructibleTiles;
    public TilemapCollider2D tilemapCollider;
    public Grid grid;

    public float x = 0f;
    public float y = 0f;

    public DestructibleTile tunnelLeftTile;
    public DestructibleTile tunnelRightTile;
    public DestructibleTile tunnelTopLeftTile;
    public DestructibleTile tunnelTopRighTile;
    public DestructibleTile tunnelBottomLeftTile;
    public DestructibleTile tunnelBottomRightTile;
    public DestructibleTile tunnelTopTile;
    public DestructibleTile tunnelBottomTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool canMove = false;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        float xDirection = x;
        float yDirection = y;

        if (y < 0)
            yDirection = -1.25f;
        else if (y > 0)
            yDirection = 1.25f;

        Vector3Int drilledTileCell = grid.WorldToCell(transform.position + new Vector3(xDirection, yDirection, 0));

        if (destructibleTiles.GetTile(drilledTileCell) is DestructibleTile)
        {
            DestructibleTile drilledTile = destructibleTiles.GetTile(drilledTileCell) as DestructibleTile;
            drilledTile.health -= 0.1f;
            Debug.Log("Tile health = " + drilledTile.health);
            if (drilledTile.health < 0)
            {
                Debug.Log("Destroyed tile");
                destructibleTiles.SetTile(drilledTileCell, null);
                if (destructibleTiles.GetTile(drilledTileCell + new Vector3Int(-1, 1, 0)) == null)
                {
                    destructibleTiles.SetTile(new Vector3Int(-1, 0, 0) + drilledTileCell, tunnelTopLeftTile);
                }
                else
                {
                    destructibleTiles.SetTile(new Vector3Int(-1, 0, 0) + drilledTileCell, tunnelLeftTile);
                }
                if (destructibleTiles.GetTile(drilledTileCell + new Vector3Int(1, 1, 0)) == null)
                {
                    destructibleTiles.SetTile(new Vector3Int(1, 0, 0) + drilledTileCell, tunnelTopRighTile);
                }
                else
                {
                    destructibleTiles.SetTile(new Vector3Int(1, 0, 0) + drilledTileCell, tunnelRightTile);
                }


                canMove = true;
            }
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            if (x != 0)
            {
                transform.position += new Vector3(x, 0, 0) * Time.deltaTime;

            }
            else if (y != 0)
            {
                transform.position += new Vector3(0, y, 0) * Time.deltaTime;
            }
        }
    }

}
