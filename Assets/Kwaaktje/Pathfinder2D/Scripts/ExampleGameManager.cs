using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KwaaktjePathfinder2D
{
    public class ExampleGameManager : MonoBehaviour
    {
        [Header("Parameters")]
        public float playerSpeed = 3.0f;
        public float difficultTerrainWeight = 100.0f;

        [Header("Other")]
        public new Camera camera;
        public Vector2Int startPlayerPosition;
        public Tilemap mazeTileMap;
        public Tilemap pathTilemap;
        public TileBase pathTile;
        public TileBase targetTile;

        public GameObject player;

        public TextMeshProUGUI distanceText;
        public TextMeshProUGUI weightedDistanceText;

        private List<Vector2Int> path;
        private Vector2Int currentTagetCell;
        private bool isDestinationReached = true;
        private Pathfinder2D pathfinder;

        private Dictionary<Vector2Int, float> GetWeightedTilemap()
        {
            Dictionary<Vector2Int, float> result = new Dictionary<Vector2Int, float>();
            for (int x = mazeTileMap.origin.x; x < mazeTileMap.origin.x + mazeTileMap.size.x; x++)
            {
                for (int y = mazeTileMap.origin.y; y < mazeTileMap.origin.y + mazeTileMap.size.y; y++)
                {
                    TileBase tile = mazeTileMap.GetTile(new Vector3Int(x, y, 0));
                    if (tile)
                    {
                        if ((tile.name == "Floor") | (tile.name == "Floor_HEX"))
                        {
                            result.Add(new Vector2Int(x, y), 1.0f);
                        }
                        else if ((tile.name == "Floor2") | (tile.name == "Floor2_HEX"))
                        {
                            result.Add(new Vector2Int(x, y), difficultTerrainWeight);
                        }
                    }
                }
            }
            return result;
        }

        private void ShowPath()
        {
            foreach (var cell in path)
            {
                pathTilemap.SetTile((Vector3Int)cell, pathTile);
            }
            if (path.Count > 0)
            {
                pathTilemap.SetTile((Vector3Int)path[0], targetTile);
            }
        }

        private void UpdateDistance(float newDistance)
        {
            distanceText.text = newDistance.ToString();
        }

        private void UpdateWeightedDistance(float newDistance)
        {
            weightedDistanceText.text = newDistance.ToString();
        }


        // Start is called before the first frame update
        void OnMouseDown()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int start = (Vector2Int)mazeTileMap.WorldToCell(player.gameObject.transform.position);
            Vector2Int end = (Vector2Int)mazeTileMap.WorldToCell(mousePosition);
            path = pathfinder.FindPath(start, end).Path;
            isDestinationReached = false;
            NextPathPoint();
        }

        private void NextPathPoint()
        {
            if (path.Count > 0)
            {
                currentTagetCell = path[path.Count - 1];
                path.RemoveAt(path.Count - 1);
            }
            else
            {
                isDestinationReached = true;
            }
        }
    }
}
