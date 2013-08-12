using System.Collections;
using System;
using UnityEngine;

public class FieldClassic
{   
    private CellClassic[,] _field;
    private FieldClassic _instance;














    public FieldClassic(int rowCount,int colCount)
    {
        if(_field==null)
            _field=new CellClassic[rowCount,colCount];
        else
        {
            return;
        }
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
                    _field[i, j].IsMine = true;
                }
                else
                    _field[i, j].IsMine = false;
            }
        }
    }
    public void SetNearMinesCount()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                try { if (_field[i + 1, j].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i, j + 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i, j - 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i + 1, j + 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j + 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j - 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i + 1, j - 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
            }
        }
    }
    public void SearchForFreeNearCells(CellClassic cell)
    {
        //int delay=1;
        for (int i = cell.Row - 1; i < cell.Row + 2; i++)
        {
            for (int j = cell.Column - 1; j < cell.Column + 2; j++)
            {
                try
                {
                    if (_field[i, j].NearMinesCount == 0 && _field[i, j].IsClose == true)
                    {
                        _field[i, j].IsClose = false;
                        //_field[i, j].DelayBeforeOpen = delay;
                        SearchForFreeNearCells(_field[i, j]);
                    }
                }
                catch { }
            }
        }
    }

    public void OpenCell(CellClassic cell)
    {
        cell.IsClose = false;
        if (cell.IsMine)
        {
            SapperClassic.Instance.GameOver();
        }
        else if (cell.NearMinesCount == 0)
        {
            cell.IsClose = false;
            SearchForFreeNearCells(cell);
        }
        else
        {
            cell.IsClose = false;
        }
    }
    void CreateCellGameObject(CellClassic cell)
    {
       var cellGO= GameObject.Instantiate(Resources.Load("prefabs/game/classic/cell")) as GameObject;
       cellGO.GetComponent<CellViewClassic>().Model = cell;
        cellGO.name = cell.Row.ToString() + "_" + cell.Column.ToString() + "_" + "cell";
       cellGO.transform.parent = GameObject.Find("Field").transform;
        var cellSprite = cellGO.GetComponent<CellViewClassic>().closed;
       cellGO.transform.localPosition = new Vector3(cell.Row * cellSprite.renderer.bounds.size.x, cell.Column * cellSprite.renderer.bounds.size.y, 0);
    }
}
