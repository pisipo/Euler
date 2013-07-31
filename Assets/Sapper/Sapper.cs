using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Sapper : MonoBehaviour {

	public UITable fieldTable;
	public Cell cellPref;
	public Transform explosion;
	public Camera camera;
	public Cell[,] field; 
	public void SetMines( /*Cell[,] field*/)
	{
		field = new Cell[16,16];
		fieldTable.columns=field.GetLength(0);
		for(int i=0;i<field.GetLength(0);i++)
		{
			for(int j=0;j<field.GetLength(1);j++)
			{
				field[i,j]=Instantiate(cellPref) as Cell;
				field[i,j].transform.parent=fieldTable.transform;
				field[i,j].transform.localScale=Vector3.one;
				
				if(UnityEngine.Random.Range(0,100)>90){
					field[i,j].IsMined=true;
					//field[i,j].back.color=Color.gray;
				}
				else
					field[i,j].IsMined=false;
			}
//			cell.IsMined=UnityEngine.Random.Range(0,1);
		}
		fieldTable.Reposition();
	}
	
	public void SetMinesCount( /*Cell[,] field*/)
	{
		for(int i=0;i<field.GetLength(0);i++)
		{
			for(int j=0;j<field.GetLength(1);j++)
			{
			    try{if(field[i+1,j].IsMined==true)field[i,j].NearMinesCount++;}catch{}
			    try{if(field[i-1,j].IsMined==true)field[i,j].NearMinesCount++;}catch{}
			    try{if(field[i,j+1].IsMined==true)field[i,j].NearMinesCount++;}catch{}
			    try{if(field[i,j-1].IsMined==true)field[i,j].NearMinesCount++;}catch{}
			    try{if(field[i+1,j+1].IsMined==true)field[i,j].NearMinesCount++;}catch{}
			    try{if(field[i-1,j+1].IsMined==true)field[i,j].NearMinesCount++;}catch{}
			    try{if(field[i-1,j-1].IsMined==true)field[i,j].NearMinesCount++;}catch{}
			    try{if(field[i+1,j-1].IsMined==true)field[i,j].NearMinesCount++;}catch{}
				field[i,j].label.text=field[i,j].NearMinesCount.ToString();
			}
		}
	}
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			
			Ray ray =camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,1000))
			{
				
				Cell cell=hit.transform.GetComponent<Cell>();
				if(cell.IsChecked==false){
					cell.back.color=Color.yellow;
					if(cell.IsMined==true){ 
						cell.back.color=Color.red;
					}	
					else {
						cell.back.color=Color.yellow;
						cell.label.gameObject.SetActive(true);
						cell.IsClosed=false;
						if(cell.label.text=="0"){
							
							SearchForFreeNearCells(cell);
						}
					}
				}
			}
		}
		if(Input.GetMouseButtonDown(1))
		{
			Ray ray =camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,1000))
			{
				
				Cell cell=hit.transform.GetComponent<Cell>();
				cell.IsChecked=!cell.IsChecked;
				if(cell.IsChecked)
					cell.back.color=Color.red;
				else
					cell.back.color=Color.blue;
			}
		}
	}
	public void SearchForFreeNearCells(Cell cell)
	{
		var index=CoordinatesOf(cell);
		for(int i=index[0]-1;i<index[0]+2;i++){
			for(int j=index[1]-1;j<index[1]+2;j++){
				try{field[i,j].label.gameObject.SetActive(true);
					field[i,j].back.color=Color.yellow;
				    if(field[i,j].label.text=="0" && field[i,j].IsClosed==true ){
						field[i,j].IsClosed=false;
						SearchForFreeNearCells(field[i,j]);
					}
				}
				catch{}
			}
		}	
		//print(index[0]);
		//print(index[1]);
		
	}
	void Start()
	{
		//field = new Cell[16,16];
		SetMines(/*field*/);
		SetMinesCount(/*field*/);
		print(field.Length);
	}
	
	
	public  int[] CoordinatesOf (Cell value)
	{
	    int w = field.GetLength(0); // width
	    int h = field.GetLength(1); // height
	
	    for (int x = 0; x < w; ++x)
	    {
	        for (int y = 0; y < h; ++y)
	        {
	            if (field[x, y].Equals(value))
	                return new int[2] {x,y};
	        }
	    }
	
	    return new int[2]{-1,-1};
	}
}
