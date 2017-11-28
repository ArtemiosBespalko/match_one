using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public float spacing = 1f;

    private List<Piece> _pieces;

    void Start()
    {
        _pieces = new List<Piece>(width * height);

        var pieceParentGameObject = new GameObject("Pieces");
        var parent = pieceParentGameObject.transform;

        var positionOffset = new Vector2(-width * 0.5f * spacing + spacing * 0.5f, -height * 0.5f * spacing + spacing * 0.5f);
        parent.position = positionOffset;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var position = new Vector2(i * spacing, j * spacing);
				var prefab = Resources.Load<Piece>("Piece" + Random.Range(0, 6));
                var randomPiece = Instantiate<Piece>(prefab, position + positionOffset, Quaternion.identity, parent);
                randomPiece.Initialize(i, j);
                randomPiece.PieceClickedEvent += OnPieceClicked;

                _pieces.Add(randomPiece);
            }
        }
    }

    private void OnPieceClicked(Piece piece)
    {
        int column = piece.i;
        int row = piece.j;

        var topPieces = new List<Piece>(10);

        for (int i = 0; i < _pieces.Count; i++)
        {
            var localPiece = _pieces[i];
            if (localPiece.i == column && localPiece.j > row)
            {
                topPieces.Add(localPiece);
            }
        }

        foreach(var localPiece in topPieces)
        {
            var position = new Vector2(localPiece.i * spacing, (localPiece.j - 1) * spacing);
            localPiece.ChangeY(localPiece.j - 1, position);
        }

        _pieces.Remove(piece);
        piece.PieceClickedEvent -= OnPieceClicked;
        piece.transform.DOScale(3f, 0.5f);
        var spriteRenderer = piece.GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0f, 0.5f);
        spriteRenderer.sortingOrder = 2;
        Destroy(piece.gameObject, 0.5f);
    }
}
