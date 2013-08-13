using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SapperClassic : MonoBehaviour
{
    private static SapperClassic _instance;
   
    public static SapperClassic Instance
    {
        get
        {
            return _instance;
        }
    }
    private FieldClassic field;
    public bool IsPaused
    {
        get;
        set;
    }		
	void Start()
	{
	    _instance = this;
	    GestureController.OnCellShortTap += CellShortTapHandler;
        GestureController.OnCellLongTap += CellLongTapHandler;
        
	}

    void OnTap(TapGestureRecognizer gesture)
    {
        Debug.Log("Tap");
    }

    void CellShortTapHandler(CellClassic cell)
    {
        if (cell.IsFlag)
            return;
        else if (cell.IsClose)
            field.OpenCell(cell);
    }

    void CellLongTapHandler(CellClassic cell)
    {
        Handheld.Vibrate();
        cell.IsFlag = !cell.IsFlag;
    }

    public void GameOver()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void StarGame(int rowCount,int colCount,int minesPersentage)
    {

        field = new FieldClassic(rowCount, colCount);
        field.SetMines(minesPersentage);
        field.SetNearMinesCount();  
    }

    void OnDisable()
    {
        print("ONSAPPERDISABLE");
        _instance = null;
        GestureController.OnCellShortTap -= CellShortTapHandler;
        GestureController.OnCellLongTap -= CellLongTapHandler;
    }
}
