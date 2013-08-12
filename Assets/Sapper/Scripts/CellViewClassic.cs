using UnityEngine;
using System.Collections;

public class CellViewClassic : MonoBehaviour
{
    public tk2dSprite flag;
    public tk2dSprite question;
    public tk2dSprite closed;
    public tk2dSprite open;
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
        flag.gameObject.SetActive(Model.IsFlag);
        if (Model.IsClose == false) {
            closed.gameObject.SetActive(false);//StartCoroutine("OpenCellCoroutine");
            open.gameObject.SetActive(true);
            }
        if(Model.IsMine)flag.gameObject.SetActive(true);


    }

}
  
