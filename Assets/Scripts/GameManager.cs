using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private TileData[] _tilesData;
    public TileData[] tilesData { get { return _tilesData; } }

    [SerializeField]
    private TileBorderData[] tileBorderData;

    public MapChunk mapChunkPrefab;

    public Transform player;

    public string randomSeed;
    public int seed { get; private set; }

    private Dictionary<Vector2, MapChunk> chunks = new Dictionary<Vector2, MapChunk>();
    private HashSet<MapChunk> activeChunks = new HashSet<MapChunk>();
    private MapChunk currentChunk;

    void Start()
    {
        GenerateMap();
        currentChunk = chunks[new Vector2(0, 0)];
    }

    void Update()
    {
        if (currentChunk == null)
            return;

        Vector3 centerOffset = Vector3.one * mapChunkPrefab.size / 2f;
        if (Vector3.SqrMagnitude(currentChunk.transform.position + centerOffset - player.position) > currentChunk.size * currentChunk.size / 2)
            UpdateMap();
    }

    void GenerateMap()
    {
        seed = randomSeed.GetHashCode();

        GenerateChunksAroundPosition(Vector2.zero);
    }

    void GenerateChunk(int x, int y)
    {
        Vector2 center = new Vector2(x, y);

        if (chunks.ContainsKey(center))
            chunks[center].gameObject.SetActive(true);
        else
        {
            MapChunk newChunk = Instantiate(mapChunkPrefab, transform);
            newChunk.name = "Chunk " + center;
            chunks.Add(center, newChunk);
            newChunk.Generate(center);
        }

        activeChunks.Add(chunks[center]);
    }

    void GenerateChunksAroundPosition(Vector2 position)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                GenerateChunk((int)position.x + x, (int)position.y + y);
            }
        }
    }

    void UpdateMap()
    {
        Vector2 newCurrentChunkPos = new Vector2(
            Mathf.RoundToInt(player.position.x / mapChunkPrefab.size),
            Mathf.RoundToInt(player.position.y / mapChunkPrefab.size)
        );

        currentChunk = chunks[newCurrentChunkPos];
        GenerateChunksAroundPosition(newCurrentChunkPos);

        foreach (var chunk in activeChunks.ToArray())
        {
            Vector3 centerOffset = Vector3.one * mapChunkPrefab.size / 2f;
            if (Vector3.SqrMagnitude(chunk.transform.position + centerOffset - player.position) > 4 * chunk.size * chunk.size)
            {
                chunk.gameObject.SetActive(false);
                activeChunks.Remove(chunk);
            }
        }
    }

    void OnValidate()
    {
        if (borders != null)
            foreach (var border in tileBorderData)
            {
                if (!tilesData.Any(td => td.type == border.sideOne) || !tilesData.Any(td => td.type == border.sideTwo))
                {
                    Debug.LogError("You need to assign the right names to tileBorderData in GameManager!");
                }
            }

        if (tilesData.Select(td => td.probability).Sum() != 100)
        {
            Debug.LogError("You need to assign the right probabilities for tiles in GameManager!");
        }
    }


    Dictionary<TileType, Dictionary<TileType, Sprite>> borders;

    public Sprite GetBorder(TileType oneSide, TileType otherSide)
    {
        if (borders == null)
        {
            borders = new Dictionary<TileType, Dictionary<TileType, Sprite>>();
            foreach (var border in tileBorderData)
            {
                if (!borders.ContainsKey(border.sideOne))
                    borders.Add(border.sideOne, new Dictionary<TileType, Sprite>());

                borders[border.sideOne].Add(border.sideTwo, border.sprite);
            }
        }

        if (borders.ContainsKey(oneSide) && borders[oneSide].ContainsKey(otherSide))
            return borders[oneSide][otherSide];

        return null;
    }

    public MapChunk GetChunkAt(int x, int y)
    {
        Vector2 position = new Vector2(x, y);

        if (chunks.ContainsKey(position))
            return chunks[position];
        else
            return null;
    }

    [System.Serializable]
    public struct TileData
    {
        [SerializeField]
        private TileType _type;
        public TileType type { get { return _type; } }

        [SerializeField]
        private Sprite _sprite;
        public Sprite sprite { get { return _sprite; } }

        [Range(0, 100)]
        [SerializeField]
        private int _probability;
        public int probability { get { return _probability; } }
    }

    [System.Serializable]
    public struct TileBorderData
    {
        [SerializeField]
        private TileType _sideOne;
        public TileType sideOne { get { return _sideOne; } }

        [SerializeField]
        private TileType _sideTwo;
        public TileType sideTwo { get { return _sideTwo; } }

        [SerializeField]
        private Sprite _sprite;
        public Sprite sprite { get { return _sprite; } }
    }

    public enum TileType
    {
        Rock_1,
        Rock_2
    }
}
