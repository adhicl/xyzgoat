using System;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    private bool _active = false;
    private InputSystem_Actions inputSystemActions;

    private void Start()
    {
        inputSystemActions = new InputSystem_Actions();
    }

    public void StartControl()
    {
        //Debug.Log("StartControl ");
        
        _active = true;
        inputSystemActions.Player.Enable();
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
        }
        else
        {
            this.transform.Rotate(Vector3.up, 10f * Time.deltaTime);
        }
    }
}
