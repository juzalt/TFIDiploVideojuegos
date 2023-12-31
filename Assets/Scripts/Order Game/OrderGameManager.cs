using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _width = 4;
    [SerializeField] private int _height = 4;
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private SpriteRenderer _boardPrefab;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] Camera camera; // Esto no estaba en el tuto pero hacer Camera.main.transform devolvia un error (no encontraba a Camera)
    [SerializeField] private List<BlockType> _types;
    [SerializeField] private float _travelTime = 0.2f;
    [SerializeField] private int _winCondition = 2048; // Podemos cambiar este numero a cualquier multiplo de 2 segun que tan largo queremos que sea el juego.
    [SerializeField] GameObject endGamePanel;
    [SerializeField] TextMeshProUGUI endGameText;
    [SerializeField] Sprite imgForValue2;
    [SerializeField] Sprite imgForValue4;
    [SerializeField] Sprite imgForValue8;
    [SerializeField] Sprite imgForValue16;
    [SerializeField] Sprite imgForValue32;
    [SerializeField] Sprite imgForValue64;

    private GameState _state;
    private int _round;

    private List<Node> _nodes;
    private List<Block> _blocks;

    private SpriteRenderer _spriteRenderer;

    private BlockType GetBlockTypeByValue(int value) => _types.First(t=> t.Value == value);

    void Start()
    {
        ChangeState(GameState.GenerateLevel);
    }

    private void Update()
    {
        if (_state != GameState.WaitingInput) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) Shift(Vector2.left);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) Shift(Vector2.up);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) Shift(Vector2.right);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) Shift(Vector2.down);

        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    private void ChangeState(GameState newState)
    {
        _state = newState;

        switch (newState)
        {
            case GameState.GenerateLevel:
                GenerateGrid();
                break;
            case GameState.SpawningBlocks:
                SpawnBlocks(_round++ == 0 ? 2 : 1);
                break;
            case GameState.WaitingInput:
                break;
            case GameState.Moving:
                break;
            case GameState.Win:
                EndGame(true);
                break;
            case GameState.Lose:
                EndGame(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    void EndGame(bool win)
    {
        endGamePanel.SetActive(true);
        if (win)
        {
            AudioManager.Instance.PlaySound(AudioManager.Sound.WinMiniGame);
            endGameText.text = "�Acomodaste todo! \r \n �Quieres repetirlo?";
            PlayerPrefs.SetInt("OrderGameFinished", 1);
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioManager.Sound.LoseMiniGame);
            endGameText.text = "�Quieres volver a intentar?";
        }
    }

    void GenerateGrid()
    {
        _round = 0;
        _nodes = new List<Node>();
        _blocks = new List<Block>();
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

        ChangeState(GameState.SpawningBlocks);
    }

    void SpawnBlocks(int amount)
    {
        var freeNodes = _nodes.Where(n=>n.OccupiedBlock == null).OrderBy(b=> Random.value).ToList();

        foreach (var node in freeNodes.Take(amount)) {
            SpawnBlock(node, Random.value > 0.8f ? 4 : 2);
        }

        if (freeNodes.Count() == 1) {
            // Perdiste maestre. Toc� de ac�.
            ChangeState(GameState.Lose);
            return;
        }

        ChangeState(_blocks.Any( b => b.Value == _winCondition) ? GameState.Win : GameState.WaitingInput);
    }

    void SpawnBlock(Node node, int value)
    {
        var block = Instantiate(_blockPrefab, node.Pos, Quaternion.identity);
        block.Init(GetBlockTypeByValue(value));
        block.SetBlock(node);
        _spriteRenderer = block.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;

        block.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
        switch (value)
        {
            case 2:
                _spriteRenderer.sprite = imgForValue2;
                break;
            case 4:
                _spriteRenderer.sprite = imgForValue4;
                break;
            case 8:
                _spriteRenderer.sprite = imgForValue8;
                break;
            case 16:
                _spriteRenderer.sprite = imgForValue16;
                break;
            case 32:
                _spriteRenderer.sprite = imgForValue32;
                break;
            case 64:
                _spriteRenderer.sprite = imgForValue64;
                break;
            default:
                throw new ArgumentOutOfRangeException("error", value, null);
        }
        _blocks.Add(block);
    }

    void Shift(Vector2 dir)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.MoveClothOG);
        ChangeState(GameState.Moving);

        var orderedBlocks = _blocks.OrderBy(b => b.Pos.x).ThenBy(b => b.Pos.y).ToList();

        if (dir == Vector2.right || dir == Vector2.up) orderedBlocks.Reverse();

        foreach (var block in orderedBlocks)
        {
            var next = block.Node;
            do
            {
                block.SetBlock(next);

                var possibleNode = GetNodeAtPosition(next.Pos + dir);
                if (possibleNode != null)
                {
                    // Entra ac� cuando hay un nodo en la direcci�n a la que quiere ir

                    // Si puede mergear, mergea.
                    if (possibleNode.OccupiedBlock !=  null && possibleNode.OccupiedBlock.CanMerge(block.Value))
                    {
                        block.MergeBlock(possibleNode.OccupiedBlock);
                        AudioManager.Instance.PlaySound(AudioManager.Sound.GainWisdomSG);
                    } 
                    // Si no es posible, el bloque se mueve de nodo
                    else if (possibleNode.OccupiedBlock == null) next = possibleNode;
                }

            } while (next != block.Node);
        }

        var sequence = DOTween.Sequence();

        foreach (var block in orderedBlocks)
        {
            var movePoint = block.MergingBlock != null ? block.MergingBlock.Node.Pos : block.Node.Pos;

            sequence.Insert(0, block.transform.DOMove(movePoint, _travelTime));
        }

        sequence.OnComplete(() =>
        {
            foreach (var block in orderedBlocks.Where(b => b.MergingBlock != null ))
            {
                MergeBlocks(block.MergingBlock, block);
            }

            ChangeState(GameState.SpawningBlocks);
        });
    }

    void MergeBlocks(Block baseBlock, Block mergingBlock)
    {
        SpawnBlock(baseBlock.Node, baseBlock.Value * 2);

        RemoveBlock(baseBlock);
        RemoveBlock(mergingBlock);
    }

    void RemoveBlock(Block block)
    {
        _blocks.Remove(block);
        Destroy(block.gameObject);
    }

    Node GetNodeAtPosition(Vector2 pos)
    {
        return _nodes.FirstOrDefault(n => n.Pos == pos);
    }

    public void ContinueStudying()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickYes);
    }

    public void GoBackToMainScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIClickNo);
        AudioManager.Instance.ChangeMusic();
    }
}

[Serializable]
public struct BlockType
{
    public int Value;
    public Color Color;
}

public enum GameState 
{
    GenerateLevel,
    SpawningBlocks,
    WaitingInput,
    Moving,
    Win,
    Lose
}