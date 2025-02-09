using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class CubeCtrl : MonoBehaviour
{
    private bool _active = false;
    private InputSystem_Actions inputSystemActions;

    [DllImport("__Internal")] private  static extern void NativeIOSCode_objectRotated(float x, float y, float z);

    [DllImport("__Internal")] private  static extern void NativeIOSCode_setupEmitter();

    private void Start()
    {
        inputSystemActions = new InputSystem_Actions();
    }

    public void StartControl()
    {
        //Debug.Log("StartControl ");
        
        _active = true;
        inputSystemActions.Player.Enable();

        #if UNITY_IOS && !UNITY_EDITOR
        NativeIOSCode_setupEmitter();
        #endif
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_active) return;

        Vector2 input = inputSystemActions.Player.Look.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            Vector3 cEuler = this.transform.eulerAngles;
            cEuler.y -= input.x * Time.deltaTime * 50f;
            cEuler.z += input.y * Time.deltaTime * 50f;
            this.transform.eulerAngles = cEuler;

            #if UNITY_IOS && !UNITY_EDITOR
            NativeIOSCode_objectRotated(cEuler.x, cEuler.y, cEuler.z);
            #endif
        }
        else
        {
            this.transform.Rotate(Vector3.up, 10f * Time.deltaTime);
        }
    }
}
