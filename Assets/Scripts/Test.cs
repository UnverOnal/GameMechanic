using EzySlice;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject previousObject;
    public GameObject objectToCut;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.S)) return;

        var surplus = new MeshCutter().Slice(previousObject, objectToCut, out var body);
        body.transform.localScale *= 2f;
        surplus[0].AddComponent<Rigidbody>().AddForce(Vector3.up * -5f, ForceMode.Impulse);
        surplus[1].AddComponent<Rigidbody>().AddForce(Vector3.up * -5f, ForceMode.Impulse);
    }
}