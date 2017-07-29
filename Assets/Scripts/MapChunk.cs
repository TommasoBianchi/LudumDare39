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

        for (int x = 1; x < size - 1; x++)
        {
            for (int y = 1; y < size - 1; y++)
            {
                // Instantiate border to the right
                Sprite rightBorderSprite = gameManager.getBorder(map[x, y], map[x + 1, y]);
                if (rightBorderSprite != null)
                {
                    GameObject border = Instantiate(borderPrefab, transform);
                    border.GetComponent<SpriteRenderer>().sprite = rightBorderSprite;
                    border.transform.localPosition = new Vector2(x + 0.5f, y);
                    border.transform.Rotate(0, 0, 0);
                }

                // Instantiate border to the left
                Sprite leftBorderSprite = gameManager.getBorder(map[x, y], map[x - 1, y]);
                if (leftBorderSprite != null)
                {
                    GameObject border = Instantiate(borderPrefab, transform);
                    border.GetComponent<SpriteRenderer>().sprite = leftBorderSprite;
                    border.transform.localPosition = new Vector2(x - 0.5f, y);
                    border.transform.Rotate(0, 0, 180);
                }

                // Instantiate border to the top
                Sprite topBorderSprite = gameManager.getBorder(map[x, y], map[x, y + 1]);
                if (topBorderSprite != null)
                {
                    GameObject border = Instantiate(borderPrefab, transform);
                    border.GetComponent<SpriteRenderer>().sprite = topBorderSprite;
                    border.transform.localPosition = new Vector2(x, y + 0.5f);
                    border.transform.Rotate(0, 0, 90);
                }

                // Instantiate border to the bottom
                Sprite bottomBorderSprite = gameManager.getBorder(map[x, y], map[x, y - 1]);
                if (bottomBorderSprite != null)
                {
                    GameObject border = Instantiate(borderPrefab, transform);
                    border.GetComponent<SpriteRenderer>().sprite = bottomBorderSprite;
                    border.transform.localPosition = new Vector2(x, y - 0.5f);
                    border.transform.Rotate(0, 0, -90);
                }
            }
        }
    }
}
