using System;
using System.Linq;
using Cinemachine;
using EzySlice;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject previousObject;
    public GameObject objectToCut;

    public CinemachineFreeLook cam;

    private void Update()
    {
        cam.m_XAxis.Value += Time.deltaTime * 50f;
        
        if (!Input.GetKeyDown(KeyCode.S)) return;

        var surplus = new MeshCutter().Slice(previousObject, objectToCut, out var body);
        surplus[0]?.AddComponent<Rigidbody>().AddForce(Vector3.up * -5f, ForceMode.Impulse);
        surplus[1]?.AddComponent<Rigidbody>().AddForce(Vector3.up * -5f, ForceMode.Impulse);
    }
}