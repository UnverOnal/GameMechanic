using EzySlice;
using UnityEngine;

public class MeshCutter
{
    public GameObject Slice(GameObject cutter, GameObject objectToCut)
    {
        var material = objectToCut.GetComponent<MeshRenderer>().material;
        var positionToSliceOn = GetPositionToSliceOn(cutter, objectToCut);
        var slicedHull = objectToCut.Slice(positionToSliceOn, objectToCut.transform.right, material);

        var rightObject = slicedHull.CreateUpperHull(objectToCut, material);
        var leftObject = slicedHull.CreateLowerHull(objectToCut, material);
        Object.Destroy(objectToCut);

        var surplus = objectToCut.transform.position.x > cutter.transform.position.x
            ? rightObject
            : leftObject;

        return surplus;
    }

    private Vector3 GetPositionToSliceOn(GameObject cutter, GameObject objectToCut)
    {
        var direction = objectToCut.transform.position.x > cutter.transform.position.x ? 1f : -1f;
        var position = cutter.transform.position;
        position += Vector3.right
                    * (cutter.GetComponent<MeshFilter>().mesh.bounds.extents.x *
                       cutter.transform.localScale.x * direction);
        position.z = objectToCut.transform.position.z;

        return position;
    }
}