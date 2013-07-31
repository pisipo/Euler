using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
        public bool IsMined;
	    public bool IsChecked=false;
	    public bool IsClosed=true;
		public int NearMinesCount=0;
	    public UILabel label;
	    public UISprite back;
	
	
void OnMouseClick()
	{
		print("yay");
	}
}