using UnityEngine;
using System.Collections;

public class CellClassic
{
    public int Row { get; set; }

    public int Column { get; set; }
    public bool IsMined;
    public bool IsChecked = false;
    public bool IsClosed = true;
    public int NearMinesCount;
   
    public CellClassic(int row, int col)
    {
        Row = row;
        Column = col;
    }

   /* private void OnMouseClick()
    {
        print("yay");
    }*/
}