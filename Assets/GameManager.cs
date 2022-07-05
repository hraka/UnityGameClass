using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public static GameManager manager;
    public bool whiteTurn;
    public Transform layout;
    public Dictionary<Vector2Int, Tile> dictionary = new Dictionary<Vector2Int, Tile>();
    // Start is called before the first frame update
    void Start()
    {
        manager = this;
        whiteTurn = true;
        
        for(int i = 0; i < 8; i++)
        {
            for(int j =0; j < 8; j++)
            {
                var newTile = Instantiate(Resources.Load<Tile>("Tile"), layout);
                newTile.gameObject.name = "Tile(" + i + "," + j + ")";
                newTile.location = new Vector2Int(i, j);
                dictionary.Add(new Vector2Int(i, j), newTile);

                if((i + j)  % 2 == 0)
                {
                    newTile.GetComponent<Image>().color = Color.black;
                }
                
            }
        }



        var pawnTile = dictionary[new Vector2Int(0, 1)];
        var piece = Instantiate(Resources.Load<Piece>("PawnWhite"), pawnTile.transform);
        piece.tile = pawnTile;

        pawnTile = dictionary[new Vector2Int(0, 6)];
        piece = Instantiate(Resources.Load<Piece>("PawnBlack"), pawnTile.transform);
        piece.tile = pawnTile;
        pawnTile.myPiece = piece;

        pawnTile = dictionary[new Vector2Int(0, 0)];
        piece = Instantiate(Resources.Load<Piece>("RookWhite"), pawnTile.transform);
        piece.tile = pawnTile;
        pawnTile.myPiece = piece;

        pawnTile = dictionary[new Vector2Int(7, 7)];
        piece = Instantiate(Resources.Load<Piece>("RookBlack"), pawnTile.transform);
        piece.tile = pawnTile;
        pawnTile.myPiece = piece;

        pawnTile = dictionary[new Vector2Int(2, 0)];
        piece = Instantiate(Resources.Load<Piece>("BishopWhite"), pawnTile.transform);
        piece.tile = pawnTile;
        pawnTile.myPiece = piece;

        pawnTile = dictionary[new Vector2Int(5, 7)];
        piece = Instantiate(Resources.Load<Piece>("BishopBlack"), pawnTile.transform);
        piece.tile = pawnTile;
        pawnTile.myPiece = piece;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
