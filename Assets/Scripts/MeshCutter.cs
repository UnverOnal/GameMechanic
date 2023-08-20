using EzySlice;
using UnityEngine;

public class MeshCutter
{
    //Returns extra parts and outs platform left
    public GameObject[] Slice(GameObject cutter, GameObject objectToCut, out GameObject body)
    {
        var material = objectToCut.GetComponent<MeshRenderer>().material;
        var positionsToSliceOn = GetPositionToSliceOn(cutter, objectToCut);
        body = objectToCut;
        
        //Slice right part if exceeds the border 
        var rightHull = body.Slice(positionsToSliceOn[0], body.transform.right, material);
        var rightObject = rightHull?.CreateUpperHull(body, material);
        var rightBody = rightHull?.CreateLowerHull(body, material);

        body = rightBody ? rightBody : objectToCut;
        
        //Slice left part if exceeds the border 
        var leftHull = body.Slice(positionsToSliceOn[1], body.transform.right, material);
        var leftObject = leftHull?.CreateLowerHull(body, material);
        body = leftHull?.CreateUpperHull(body, material);
        
        if(body)
            Object.Destroy(rightBody);
        objectToCut.SetActive(false);

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

        return positions;
    }
}