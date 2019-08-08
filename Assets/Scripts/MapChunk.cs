using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunk : MonoBehaviour
{

    public GameObject sanctuaryTilePrefab;
    public GameObject tilePrefab;
    public GameObject borderPrefab;

    public float tileScale;

    private Vector2 _center;
    public Vector2 center
    {
        get
        {
            return _center;
        }
        private set
        {
            _center = value;
            transform.position = (center * size - Vector2.one * size / 2f) * tileScale;
        }
    }

    [SerializeField]
    private int _size;
    public int size { get { return _size; } }

    [SerializeField]
    private int _sanctuaries;
    public int sanctuaries { get { return _sanctuaries; } }

    private GameManager gameManager;
    private System.Random random;

    private GameManager.TileType[,] map;

    void Initialize()
    {
        gameManager = FindObjectOfType<GameManager>();
        random = new System.Random(gameManager.seed);
    }

    public void Generate(Vector2 center)
    {
        Initialize();
        this.center = center;
        map = new GameManager.TileType[size, size];

        HashSet<Vector2> sanctuaryPositions = GenerateSanctuaryPositions();

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (sanctuaryPositions.Contains(new Vector2(x, y))) continue;

                int randInt = random.Next(0, 100);

                int i = 0;
                for (int sum = gameManager.tilesData[0].probability; sum <= 100; i++, sum += gameManager.tilesData[i].probability)
                {
                    if (sum > randInt)
                        break;
                }

                Sprite sprite = gameManager.tilesData[i].sprite;
                GameObject newTile = Instantiate(tilePrefab, transform);
                newTile.transform.localPosition = new Vector2(x, y) * tileScale;
                newTile.GetComponent<SpriteRenderer>().sprite = sprite;
                map[x, y] = gameManager.tilesData[i].type;
            }
        }

        // Place the sanctuaries
        foreach (var pos in sanctuaryPositions)
        {   
            GameObject newTile = Instantiate(sanctuaryTilePrefab, transform);
            newTile.transform.localPosition = pos * tileScale;
            map[(int)pos.x, (int)pos.y] = GameManager.TileType.Sanctuary_Tile;
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

        MapChunk rightMapChunk = gameManager.GetChunkAt((int)center.x + 1, (int)center.y);
        if (rightMapChunk != null && rightMapChunk.map != null)
        {
            for (int y = 0; y < size; y++)
            {
                // Instantiate border to the right
                Sprite rightBorderSprite = gameManager.GetBorder(map[size - 1, y], rightMapChunk.map[0, y]);
                if (rightBorderSprite != null)
                {
                    InstantiateBorder(size - 1, y, rightBorderSprite, new Vector2(0.5f, 0), 0);
                }
                // Instantiate border to the right - inverted
                Sprite rightBorderSpriteInverted = gameManager.GetBorder(rightMapChunk.map[0, y], map[size - 1, y]);
                if (rightBorderSpriteInverted != null)
                {
                    InstantiateBorder(size - 1, y, rightBorderSpriteInverted, new Vector2(0.5f, 0), 180);
                }
            }
        }

        MapChunk leftMapChunk = gameManager.GetChunkAt((int)center.x - 1, (int)center.y);
        if (leftMapChunk != null && leftMapChunk.map != null)
        {
            for (int y = 0; y < size; y++)
            {
                // Instantiate border to the left
                Sprite leftBorderSprite = gameManager.GetBorder(map[0, y], leftMapChunk.map[size - 1, y]);
                if (leftBorderSprite != null)
                {
                    InstantiateBorder(0, y, leftBorderSprite, new Vector2(-0.5f, 0), 180);
                }
                // Instantiate border to the left - inverted
                Sprite leftBorderSpriteInverted = gameManager.GetBorder(leftMapChunk.map[size - 1, y], map[0, y]);
                if (leftBorderSpriteInverted != null)
                {
                    InstantiateBorder(0, y, leftBorderSpriteInverted, new Vector2(-0.5f, 0), 0);
                }
            }
        }

        MapChunk upMapChunk = gameManager.GetChunkAt((int)center.x, (int)center.y + 1);
        if (upMapChunk != null && upMapChunk.map != null)
        {
            for (int x = 0; x < size; x++)
            {
                // Instantiate border to the top
                Sprite topBorderSprite = gameManager.GetBorder(map[x, size - 1], upMapChunk.map[x, 0]);
                if (topBorderSprite != null)
                {
                    InstantiateBorder(x, size - 1, topBorderSprite, new Vector2(0, 0.5f), 90);
                }
                // Instantiate border to the top - inverted
                Sprite topBorderSpriteInverted = gameManager.GetBorder(upMapChunk.map[x, 0], map[x, size - 1]);
                if (topBorderSpriteInverted != null)
                {
                    InstantiateBorder(x, size - 1, topBorderSpriteInverted, new Vector2(0, 0.5f), -90);
                }
            }
        }

        MapChunk downMapChunk = gameManager.GetChunkAt((int)center.x, (int)center.y - 1);
        if (downMapChunk != null && downMapChunk.map != null)
        {
            for (int x = 0; x < size; x++)
            {
                // Instantiate border to the bottom
                Sprite bottomBorderSprite = gameManager.GetBorder(map[x, 0], downMapChunk.map[x, size - 1]);
                if (bottomBorderSprite != null)
                {
                    InstantiateBorder(x, 0, bottomBorderSprite, new Vector2(0, -0.5f), -90);
                }
                // Instantiate border to the bottom - inverted
                Sprite bottomBorderSpriteInverted = gameManager.GetBorder(downMapChunk.map[x, size - 1], map[x, 0]);
                if (bottomBorderSpriteInverted != null)
                {
                    InstantiateBorder(x, 0, bottomBorderSpriteInverted, new Vector2(0, -0.5f), 90);
                }
            }
        }
    }

    void InstantiateBorder(int x, int y, Sprite rightBorderSprite, Vector2 offset, int zRotatation)
    {
        GameObject border = Instantiate(borderPrefab, transform);
        border.GetComponent<SpriteRenderer>().sprite = rightBorderSprite;
        border.transform.localPosition = new Vector2(x, y) + offset;
        border.transform.Rotate(0, 0, zRotatation);
    }

    HashSet<Vector2> GenerateSanctuaryPositions()
    {
        HashSet<Vector2> positions = new HashSet<Vector2>();

        if (center == Vector2.zero)
            return positions;

        for (int i = 0; i < sanctuaries; i++)
        {
            Vector2 pos = new Vector2(random.Next(0, size), random.Next(0, size));

            if(positions.Contains(pos))
                i--;
            else
                positions.Add(pos);
        }

        return positions;
    }
}
