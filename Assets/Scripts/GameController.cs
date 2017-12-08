using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public float spacing = 1f;
    private Transform _parent;
    private Vector2 _positionOffset;

    private List<Piece> _pieces;

    void Start()
    {
        _pieces = new List<Piece>(width * height);

        var pieceParentGameObject = new GameObject("Pieces");
        _parent = pieceParentGameObject.transform;

        _positionOffset = new Vector2(-width * 0.5f * spacing + spacing * 0.5f, -height * 0.5f * spacing + spacing * 0.5f);
        _parent.position = _positionOffset;

        for (int i = 0; i < width; i++) //проход по горизонтали
        {
            for (int j = 0; j < height; j++)  //проход по вертикали
            {
                CreatePiece(i,j); //генерация фишки
            }
        }
    }

    private void CreatePiece(int i, int j) //рандом для фишки
    {
        var position = new Vector2(i * spacing, j * spacing);
        var prefab = Resources.Load<Piece>("Piece" + Random.Range(0, 6));
        var randomPiece = Instantiate<Piece>(prefab, position + _positionOffset, Quaternion.identity, _parent);
        randomPiece.Initialize(i, j);
        randomPiece.PieceClickedEvent += OnPieceClicked;

        _pieces.Add(randomPiece); //создание фишки
    }

    private void OnPieceClicked(Piece piece) //по клику
    {
        int column = piece.i;
        int row = piece.j;

        var topPieces = new List<Piece>(10);

        CreatePiece(column, height); //создаем фишку в колонке на высоте height

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
