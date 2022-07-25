using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IDropHandler
{
    public Vector2Int location;
    public Piece myPiece;

    public void OnDrop(PointerEventData eventData)
    {
        var piece = eventData.pointerDrag.GetComponent<Piece>();

        if (piece.isWhite != GameManager.manager.whiteTurn)
        {
            Debug.Log("���ʸ� ��Ű����/ �����: " + GameManager.manager.whiteTurn);
            return;
        }
        if (piece.type == Piece.PieceType.Pawn)
        {
            if (piece.isWhite == true && (location == piece.tile.location + Vector2Int.up + Vector2Int.left ||
                location == piece.tile.location + Vector2Int.up + Vector2Int.right))
            {
                if (myPiece != null)
                    SetPiece(piece);
                else
                    return;
            }
            if ((piece.isWhite == false && (location == piece.tile.location + Vector2Int.down + Vector2Int.left ||
                location == piece.tile.location + Vector2Int.down + Vector2Int.right)))
            {
                if (myPiece != null)
                    SetPiece(piece);
                else
                    return;
            }
            if (piece.pawnMoved == false)
            {
                if (piece.isWhite == true && (location == piece.tile.location + Vector2Int.up + Vector2Int.up))
                    SetPawnMove(piece);

                else if (piece.isWhite == false && (location == piece.tile.location + Vector2Int.down + Vector2Int.down))
                    SetPawnMove(piece);
            }
            if (piece.isWhite == true && (location == piece.tile.location + Vector2Int.up))
                SetPiece(piece);
            else if (piece.isWhite == false && (location == piece.tile.location + Vector2Int.down))
                SetPiece(piece);
        }
        else if (piece.type == Piece.PieceType.Rook && (location.x == piece.tile.location.x || (location.y == piece.tile.location.y)))
            SetPiece(piece);
        else if (piece.type == Piece.PieceType.Bishop && ((piece.tile.location.x - location.x) == (piece.tile.location.y - location.y) ||
                    location.x - piece.tile.location.x == piece.tile.location.y - location.y))
            SetPiece(piece);
        else if (piece.type == Piece.PieceType.Knight &&
            ((piece.tile.location.x - location.x) * (piece.tile.location.y - location.y) == 2 ||
            (piece.tile.location.x - location.x) * (piece.tile.location.y - location.y) == -2))
            SetPiece(piece);
        else if (piece.type == Piece.PieceType.Queen &&
            ((location.x == piece.tile.location.x || (location.y == piece.tile.location.y)) ||
            ((piece.tile.location.x - location.x) == (piece.tile.location.y - location.y) ||
                    location.x - piece.tile.location.x == piece.tile.location.y - location.y)))
            SetPiece(piece);
        else if (piece.type == Piece.PieceType.King &&
            (piece.tile.location.x - location.x) * (piece.tile.location.y - location.y) == 1 ||
            (piece.tile.location.x - location.x) * (piece.tile.location.y - location.y) == -1 ||
            location == piece.tile.location + Vector2Int.up ||
            location == piece.tile.location + Vector2Int.down ||
            location == piece.tile.location + Vector2Int.left ||
            location == piece.tile.location + Vector2Int.right)
            SetPiece(piece);
        else
            Debug.Log("�߸��� �̵� �õ�");

        /*if (piece.isWhite == GameManager.manager.whiteTurn) //���ʸ� ��Ŵ
        {
            if(location != piece.tile.location) 
            {
                if (piece.type == Piece.PieceType.Pawn)
                {
                    if (piece.isWhite == true && (location == piece.tile.location + Vector2Int.up))
                    {
                        SetPiece(piece);
                    }
                    else if (piece.isWhite == false && (location == piece.tile.location + Vector2Int.down))
                    {
                        SetPiece(piece);
                    }

                }
                else if (piece.type == Piece.PieceType.Rook && (location.x == piece.tile.location.x || (location.y == piece.tile.location.y)))
                {
                    SetPiece(piece);
                }
                else if (piece.type == Piece.PieceType.Bishop && ((piece.tile.location.x - location.x) == (piece.tile.location.y - location.y) || 
                    location.x - piece.tile.location.x == piece.tile.location.y - location.y))
                {
                    SetPiece(piece);
                }
                else
                {
                    Debug.Log("������ �̵�");
                }

            }
        }
        else
        {
            Debug.Log("���ʸ� ��Ű����/ �����: " + GameManager.manager.whiteTurn);
        }
*/


    }

    

    void SetPiece(Piece piece)
    {
        //���ʸ� ��Ű�� ���� ���
        

        //�⹰�� �ִ� ���
        if(myPiece != null)
        {
            if (myPiece.isWhite != piece.isWhite)
            {
                Destroy(myPiece.gameObject);
                Debug.Log("�⹰ �Ա�");
            }
               
            else
            {
                Debug.Log("������ �⹰�� �ִ� �ڸ��� �̵� �Ұ�");
                return;
            }
                
        }
        //�������� �̵�
        myPiece = piece;
        piece.tile.myPiece = null;
        piece.tile = this;
        piece.gameObject.transform.SetParent(this.transform);
        Debug.Log("���� �̵�");
        GameManager.manager.whiteTurn = !GameManager.manager.whiteTurn;

        piece.pawnMoved = true;
    }
    void SetPawnMove(Piece piece)
    {
        if (myPiece != null)
        {
            return;

        }
        myPiece = piece;
        piece.tile.myPiece = null;
        piece.tile = this;
        piece.gameObject.transform.SetParent(this.transform);
        Debug.Log("���� �̵�");
        GameManager.manager.whiteTurn = !GameManager.manager.whiteTurn;

        piece.pawnMoved = true;
    }
}



