using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;

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

    [DllImport("__Internal")] private  static extern void NativeIOSCode_openNativePage();

    
    private void Start()
    {
        _currentUIType = Common.UIType.Game;
        ChangeUI();
    }

    private void Update()
    {
        if (_currentUIType == Common.UIType.Game) return;

        DateTime cTime = DateTime.Now;

        Vector3 cPos = tDateTime.rectTransform.anchoredPosition;
        cPos.x -= Time.deltaTime * 50f;
        if (cPos.x <= -440f)
        {
            cPos.x = 440f;
        }

        tDateTime.text = "Current Date: "+cTime.ToString("dddd, dd MMMM yyyy")+",\nCurrent Time: "+cTime.ToString("HH:mm");
        tDateTime.rectTransform.anchoredPosition = cPos;
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
        #if UNITY_IOS && !UNITY_EDITOR
        NativeIOSCode_openNativePage();
        #endif
    }
}
