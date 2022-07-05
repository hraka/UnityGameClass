using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public Tile tile;
    public bool isWhite;
    public enum PieceType {Pawn, Rook, Bishop, Knight} //반드시 이 중 하나를 선택해야 함
    public PieceType type;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        GetComponent<Canvas>().overrideSorting = true;
        GetComponent<Canvas>().sortingOrder = 2;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        GetComponent<Canvas>().sortingOrder = 1;
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }
}
