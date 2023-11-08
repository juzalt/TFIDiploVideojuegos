using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Value;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private TextMeshPro _text;
    public Block MergingBlock;
    public bool Merging;
    public Vector2 Pos => transform.position;
    public Node Node;
    public void Init(BlockType type)
    {
        Value = type.Value;
        _renderer.color = type.Color;
        _text.text = type.Value.ToString();
    }

    public void SetBlock(Node node)
    {
        if (Node != null) Node.OccupiedBlock = null;
        Node = node;
        Node.OccupiedBlock = this;
    }

    public void MergeBlock(Block blockToMergeWith)
    {
        // Decide cu[al es el bloque con el que estamos mergeando
        MergingBlock = blockToMergeWith;

        // Libera el nodo actual
        Node.OccupiedBlock = null;

        /* 
         * Como se revisa bloque por bloque si se puede mergear o no, 
         * tenemos que configurar para que el bloque original no mergee 
         * mas de una vez, para el caso donde hay mas de un merge posible.         * 
         */
        blockToMergeWith.Merging = true;
    }

    public bool CanMerge(int value) => value == Value && !Merging && MergingBlock == null;
}
