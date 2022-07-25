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


        TextAsset position = Resources.Load<TextAsset>("Position");
        var lines = position.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            var line0 = lines[i].Split(',');

            var pawnTile = dictionary[new Vector2Int(int.Parse(line0[3]), int.Parse(line0[4]))];
            var piece = Instantiate(Resources.Load<Piece>(line0[0]), pawnTile.transform);
            piece.tile = pawnTile;
            pawnTile.myPiece = piece;

            if(int.Parse(line0[2]) == 0)
            {
                Debug.Log("∞À¡§");
                piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                piece.isWhite = false;
                piece.GetComponent<Image>().color = new Color(1, 0, 0);

                    
            } else
            {
                piece.isWhite = true;
                piece.GetComponent<Image>().color = new Color(1, 1, 1);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
