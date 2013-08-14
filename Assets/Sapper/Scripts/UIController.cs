using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField]private GameObject _difficultChooseMenu;
    [SerializeField] private GameObject _easyButton;
    [SerializeField]
    private GameObject _mediumButton;
    [SerializeField]
    private GameObject _hardButton;
    [SerializeField]
    private GameObject _resetButton;

    [SerializeField] private tk2dTextMesh _minesLeft;
    [SerializeField] private tk2dTextMesh _deminedMines;
    [SerializeField]
    private tk2dTextMesh _openedCells;
    void Start()
    {
        print("UICONTROLLERSTART");
        _difficultChooseMenu.SetActive(true);
        GestureController.OnGUITap += GUITapHandler;

    }
    public void Easy()
    {
        SapperClassic.Instance.StarGame(5,5,8);
        _difficultChooseMenu.SetActive(false);
    }
    public void Medium()
    {
        SapperClassic.Instance.StarGame(15, 15, 15);
        _difficultChooseMenu.SetActive(false);
    }
    public void Hard()
    {
        SapperClassic.Instance.StarGame(20, 20, 20);
        _difficultChooseMenu.SetActive(false);
    }

    public void Reset()
    {
        //_difficulChooseMenu.SetActive(true);
        Application.LoadLevel(Application.loadedLevel);
    }

    void GUITapHandler(GameObject sender)
    {
        if (sender==_easyButton)Easy();
        if (sender == _mediumButton) Medium();
        if (sender == _hardButton) Hard();
        if (sender == _resetButton) Reset();
    }

    void OnDisable()
    {
        GestureController.OnGUITap -= GUITapHandler;
    }
}
