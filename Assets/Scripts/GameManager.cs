using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _width = 4;
    [SerializeField] private int _height = 4;
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private SpriteRenderer _boardPrefab;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] Camera camera; // Esto no estaba en el tuto pero hacer Camera.main.transform devolvia un error (no encontraba a Camera)
    [SerializeField] private List<BlockType> _types;

    private List<Node> _nodes;
    private List<Block> _blocks;

    private BlockType GetBlockTypeByValue(int value) => _types.First(t=> t.Value == value);

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _nodes = new List<Node>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var node = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity);
                _nodes.Add(node);
            }
        }

        var center = new Vector2((float) _width / 2 - 0.5f, (float) _height / 2 - 0.5f);

        var board = Instantiate(_boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(_width, _height);

        camera.transform.position = new Vector3(center.x, center.y, -10);

        SpawnBlocks(2);
    }

    void SpawnBlocks(int amount)
    {
        var freeNodes = _nodes.Where(n=>n.OccupiedBlock == null).OrderBy(b=> Random.value).ToList();

        foreach (var node in freeNodes.Take(amount)) {
            var block = Instantiate(_blockPrefab, node.Pos, Quaternion.identity);
            block.Init(GetBlockTypeByValue(Random.value > 0.8f ? 4 : 2));
        }

        if(freeNodes.Count() == 1) {
            // Perdiste maestre. Tocá de acá.
            return;
        }
    }
}

[Serializable]
public struct BlockType
{
    public int Value;
    public Color Color;
}