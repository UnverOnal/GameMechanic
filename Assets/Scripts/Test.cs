using EzySlice;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject previousObject;
    public GameObject objectToCut;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.S)) return;

        var surplus = new MeshCutter().Slice(previousObject, objectToCut);
        surplus.AddComponent<Rigidbody>().AddForce(Vector3.up * -5f, ForceMode.Impulse);
    }
}