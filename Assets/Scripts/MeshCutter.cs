using EzySlice;
using UnityEngine;

public class MeshCutter
{
    //Returns extra parts and outs platform left
    public GameObject[] Slice(GameObject cutter, GameObject objectToCut, out GameObject body)
    {
        var material = objectToCut.GetComponent<MeshRenderer>().material;
        var positionsToSliceOn = GetPositionsToSliceOn(cutter, objectToCut);
        body = objectToCut;
        
        //Slice right part if it exceeds the border 
        var rightHull = body.Slice(positionsToSliceOn[0], body.transform.right, material);
        var rightObject = rightHull?.CreateUpperHull(body, material);
        var rightBody = rightHull?.CreateLowerHull(body, material);

        body = rightBody ? rightBody : body;
        
        //Slice left part if it exceeds the border 
        var leftHull = body.Slice(positionsToSliceOn[1], body.transform.right, material);
        var leftObject = leftHull?.CreateLowerHull(body, material);
        var leftBody = leftHull?.CreateUpperHull(body, material);

        body = leftBody ? leftBody : body;
        
        if(body == leftBody)
            Object.Destroy(rightBody);
        objectToCut.SetActive(false);

        return new []{rightObject, leftObject};
    }

    private Vector3[] GetPositionsToSliceOn(GameObject cutter, GameObject objectToCut)
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