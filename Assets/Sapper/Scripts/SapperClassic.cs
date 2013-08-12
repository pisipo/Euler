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
        GestureController.OnCellShortTap += CellLongTapHandler;
        field=new FieldClassic(10,10);
        field.SetMines();
        field.SetNearMinesCount();
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
        cell.IsFlag = !cell.IsFlag;
    }

    public void GameOver()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
