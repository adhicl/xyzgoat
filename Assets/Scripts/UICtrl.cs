using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    #region singleton
    public static UICtrl Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] TextMeshProUGUI tDateTime;

    private Common.UIType _currentUIType;
    
    private void Start()
    {
        _currentUIType = Common.UIType.Game;
        ChangeUI();
    }

    private void Update()
    {
        if (_currentUIType == Common.UIType.Game) return;

        Vector3 cPos = tDateTime.rectTransform.position;
        cPos.x -= Time.deltaTime * 50f;
        if (cPos.x <= -450f)
        {
            cPos.x = 450f;
        }
        tDateTime.rectTransform.position = cPos;
    }

    private void ChangeUI()
    {
        switch (_currentUIType)
        {
            case Common.UIType.Game:
                gameUI.SetActive(true);
                settingUI.SetActive(false);
                break;
            case Common.UIType.Setting:
                gameUI.SetActive(false);
                settingUI.SetActive(true);
                
                
                break;
        }
    }

    public void SwitchGame()
    {
        _currentUIType = Common.UIType.Game;
        ChangeUI();
    }

    public void SwitchSetting()
    {
        _currentUIType = Common.UIType.Setting;
        ChangeUI();
    }

    public void OpenNativePage()
    {
        
    }
}
