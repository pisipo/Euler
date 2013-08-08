using System.Collections;
using System;
using UnityEngine;

public class FieldClassic
{   
    private CellClassic[,] _field;

    public FieldClassic(int rowCount,int colCount)
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                _field[i,j]=new CellClassic(i,j);
                CreateCellGameObject(_field[i,j]);
            }
        }
    }
    public void SetMines()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                if (UnityEngine.Random.Range(0, 100) > 90)
                {
                    _field[i, j].IsMined = true;
                }
                else
                    _field[i, j].IsMined = false;
            }
        }
    }
    public void SetNearMinesCount()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                try { if (_field[i + 1, j].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i, j + 1].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i, j - 1].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i + 1, j + 1].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j + 1].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j - 1].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i + 1, j - 1].IsMined == true)_field[i, j].NearMinesCount++; }
                catch { }
            }
        }
    }
    public void SearchForFreeNearCells(CellClassic cell)
    {
        for (int i = cell.Row - 1; i < cell.Row + 2; i++)
        {
            for (int j = cell.Column - 1; j < cell.Column + 2; j++)
            {
                try
                {
                    if (_field[i, j].NearMinesCount == 0 && _field[i, j].IsClosed == true)
                    {
                        _field[i, j].IsClosed = false;
                        SearchForFreeNearCells(_field[i, j]);
                    }
                }
                catch { }
            }
        }
    }
    void CreateCellGameObject(CellClassic cell)
    {
       var cellGO= GameObject.Instantiate(Resources.Load("prefabs/game/classic"))as GameObject;
       cellGO.GetComponent<CellViewClassic>().Model = cell;
       cellGO.GetComponent<tk2dButton>().ButtonPressedEvent += SapperClassic.OnCellClick;
    }
}
