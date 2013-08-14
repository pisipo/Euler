using UnityEngine;
using System.Collections;

public class CellViewClassic : MonoBehaviour
{
    public tk2dSprite flag;
    public tk2dSprite question;
    public tk2dSprite close;
    public tk2dSprite open;
    public tk2dSprite mine;
    public tk2dTextMesh NearMinesCountText;
    private CellClassic _model;

    public CellClassic Model
    {
        get
        {
            return _model;
        }
        set
        {
            _model = value;
            _model.UpdateViewEvent += OnUpdateView;
        }
    }

    private void OnUpdateView()
    {
        //DEBUG
        //if (Model.IsMine) Model.IsFlag = true;
      




        flag.gameObject.SetActive(Model.IsFlag);
        if (Model.IsClose == false) {
            close.gameObject.SetActive(false);
            open.gameObject.SetActive(true);
            NearMinesCountText.text = Model.NearMinesCount.ToString();
            NearMinesCountText.Commit();
            NearMinesCountText.gameObject.SetActive(true);
        }
        if (Model.IsDemine)
        {
            close.gameObject.SetActive(false);
            flag.gameObject.SetActive(false);
            open.gameObject.SetActive(true);
            NearMinesCountText.gameObject.SetActive(false);
            mine.gameObject.SetActive(true);
        }


    }

}
  
