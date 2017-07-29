using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunk : MonoBehaviour
{

    public GameObject tilePrefab;
    public GameObject borderPrefab;

    private Vector2 _center;
    public Vector2 center
    {
        get
        {
            return _center;
        }
        set
        {
            _center = value;
            transform.position = center * size - Vector2.one * size / 2f;
        }
    }

    [SerializeField]
    private int _size;
    public int size { get { return _size; } }

    private GameManager gameManager;
    private System.Random random;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        random = new System.Random(gameManager.seed);
        Generate();
    }

    void Update()
    {

    }

    void Generate()
    {
        GameManager.TileType[,] map = new GameManager.TileType[size, size];
        Debug.Log(size);

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                int randInt = random.Next(0, 100);

                int i = 0;
                for (int sum = gameManager.tilesData[0].probability; sum <= 100; i++, sum += gameManager.tilesData[i].probability)
                {
                    if (sum > randInt)
                        break;
                }

                Sprite sprite = gameManager.tilesData[i].sprite;
                GameObject newTile = Instantiate(tilePrefab, transform);
                newTile.transform.localPosition = new Vector2(x, y);
                newTile.GetComponent<SpriteRenderer>().sprite = sprite;
                map[x, y] = gameManager.tilesData[i].type;
            }
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                // Instantiate border to the right
                Sprite rightBorderSprite = (x < size - 1) ? gameManager.GetBorder(map[x, y], map[x + 1, y]) : null;
                if (rightBorderSprite != null)
                {
                    InstantiateBorder(x, y, rightBorderSprite, new Vector2(0.5f, 0), 0);
                }

                // Instantiate border to the left
                Sprite leftBorderSprite = (x > 0) ? gameManager.GetBorder(map[x, y], map[x - 1, y]) : null;
                if (leftBorderSprite != null)
                {
                    InstantiateBorder(x, y, leftBorderSprite, new Vector2(-0.5f, 0), 180);
                }

                // Instantiate border to the top
                Sprite topBorderSprite = (y < size - 1) ? gameManager.GetBorder(map[x, y], map[x, y + 1]) : null;
                if (topBorderSprite != null)
                {
                    InstantiateBorder(x, y, topBorderSprite, new Vector2(0, 0.5f), 90);
                }

                // Instantiate border to the bottom
                Sprite bottomBorderSprite = (y > 0) ? gameManager.GetBorder(map[x, y], map[x, y - 1]) : null;
                if (bottomBorderSprite != null)
                {
                    InstantiateBorder(x, y, bottomBorderSprite, new Vector2(0, -0.5f), -90);
                }
            }
        }
    }

    private void InstantiateBorder(int x, int y, Sprite rightBorderSprite, Vector2 offset, int zRotatation)
    {
        GameObject border = Instantiate(borderPrefab, transform);
        border.GetComponent<SpriteRenderer>().sprite = rightBorderSprite;
        border.transform.localPosition = new Vector2(x, y) + offset;
        border.transform.Rotate(0, 0, zRotatation);
    }
}
