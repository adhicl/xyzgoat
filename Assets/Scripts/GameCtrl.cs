using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameCtrl : MonoBehaviour
{
    [SerializeField] private ParticleSystem sparkParticle;
    [SerializeField] Material sparkMaterial;

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
}