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
            Debug.Log("차례를 지키세요/ 흰색턴: " + GameManager.manager.whiteTurn);
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
            Debug.Log("잘못된 이동 시도");

        /*if (piece.isWhite == GameManager.manager.whiteTurn) //차례를 지킴
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
                    Debug.Log("비정상 이동");
                }

            }
        }
        else
        {
            Debug.Log("차례를 지키세요/ 흰색턴: " + GameManager.manager.whiteTurn);
        }
*/


    }

    

    void SetPiece(Piece piece)
    {
        //차례를 지키지 않은 경우
        

        //기물이 있는 경우
        if(myPiece != null)
        {
            if (myPiece.isWhite != piece.isWhite)
            {
                Destroy(myPiece.gameObject);
                Debug.Log("기물 먹기");
            }
               
            else
            {
                Debug.Log("같은팀 기물이 있는 자리로 이동 불가");
                return;
            }
                
        }
        //정상적인 이동
        myPiece = piece;
        piece.tile.myPiece = null;
        piece.tile = this;
        piece.gameObject.transform.SetParent(this.transform);
        Debug.Log("정상 이동");
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
        Debug.Log("정상 이동");
        GameManager.manager.whiteTurn = !GameManager.manager.whiteTurn;

        piece.pawnMoved = true;
    }
}



