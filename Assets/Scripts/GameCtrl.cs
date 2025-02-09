using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using System.Runtime.InteropServices;
using AOT;

public class GameCtrl : MonoBehaviour
{
    #region singleton
    public static GameCtrl Instance { get; private set; }
    
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

    [SerializeField] private ParticleSystem sparkParticle;
    [SerializeField] Material sparkMaterial;

    [DllImport("__Internal")] private  static extern void NativeIOSCode_setSparkDelegate(SparkHandler sparkHandler);

    private void Start() {
        #if UNITY_IOS && !UNITY_EDITOR
        Debug.Log("Try set spark delegate ");
        NativeIOSCode_setSparkDelegate(OnSparkHandlerCallback);
        #endif
    }

    public delegate void SparkHandler(float r, float g, float b);
    [MonoPInvokeCallback(typeof(SparkHandler))]
    private static void OnSparkHandlerCallback(float r, float g, float b){
        Instance.sparkMaterial.color = new Color(r, g, b, 1f);
        Instance.sparkParticle.Play();
    }

    /*
    private void UpdateMaterial()
    {
        sparkMaterial.color = Random.ColorHSV();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateMaterial();
            sparkParticle.Play();
        }
    }
    //*/
}