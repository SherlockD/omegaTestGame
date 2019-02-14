using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    
    [SerializeField] private Fader _loadScreen;
    [SerializeField] private GameObject _uiBackGround;
    
    public void MainMenuButtonClick()
    {
        _gameObject.SetActive(false);
        _loadScreen.gameObject.SetActive(true);
        _uiBackGround.SetActive(true);
        SceneManager.Instance.LoadScene("MainMenu",_loadScreen,null);
    }
}
