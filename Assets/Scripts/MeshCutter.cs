using EzySlice;
using UnityEngine;

public class MeshCutter
{
    //Returns extra parts and outs platform left
    public GameObject[] Slice(GameObject cutter, GameObject objectToCut, out GameObject body)
    {
        var material = objectToCut.GetComponent<MeshRenderer>().material;
        var positionsToSliceOn = GetPositionToSliceOn(cutter, objectToCut);
        
        var rightHull = objectToCut.Slice(positionsToSliceOn[0], objectToCut.transform.right, material);
        var rightObject = rightHull.CreateUpperHull(objectToCut, material);
        var rightBody = rightHull.CreateLowerHull(objectToCut, material);
        
        var leftHull = rightBody.Slice(positionsToSliceOn[1], rightBody.transform.right, material);
        var leftObject = leftHull.CreateLowerHull(rightBody, material);
        var leftBody = leftHull.CreateUpperHull(rightBody, material);
        
        Object.Destroy(rightBody);
        objectToCut.SetActive(false);

        body = leftBody;
        return new []{rightObject, leftObject};
    }

    private Vector3[] GetPositionToSliceOn(GameObject cutter, GameObject objectToCut)
    {
        var cutterPosition = cutter.transform.position;
        var positions = new[]
        {
            cutterPosition + Vector3.right
            * (cutter.GetComponent<MeshFilter>().mesh.bounds.extents.x *
               cutter.transform.localScale.x),
            cutterPosition + Vector3.right
            * (cutter.GetComponent<MeshFilter>().mesh.bounds.extents.x *
               cutter.transform.localScale.x * -1f)
        };

        positions[0].z = positions[1].z = objectToCut.transform.position.z;

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = positions[0];

        return positions;
    }
}