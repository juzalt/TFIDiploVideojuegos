using UnityEngine;

public class Block : MonoBehaviour
{
    public int Value;
    [SerializeField] private SpriteRenderer _renderer;
    public Block MergingBlock;
    public bool Merging;
    public Vector2 Pos => transform.position;
    public Node Node;
    public void Init(BlockType type)
    {
        Value = type.Value;
        _renderer.color = type.Color;
    }

    public void SetBlock(Node node)
    {
        if (Node != null) Node.OccupiedBlock = null;
        Node = node;
        Node.OccupiedBlock = this;
    }

    public void MergeBlock(Block blockToMergeWith)
    {
        // Decide cuál es el bloque con el que estamos mergeando
        MergingBlock = blockToMergeWith;

        // Libera el nodo actual
        Node.OccupiedBlock = null;

        /* 
         * Como se revisa bloque por bloque si se puede mergear o no, 
         * tenemos que configurar para que el bloque original no mergee 
         * mas de una vez, para el caso donde hay mas de un merge posible.         * 
         */
        blockToMergeWith.Merging = true;
        // blockToMergeWith deberia ser el punto de partida para la animacion
    }

    public bool CanMerge(int value) => value == Value && !Merging && MergingBlock == null;
}
