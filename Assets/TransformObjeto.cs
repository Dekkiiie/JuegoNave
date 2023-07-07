using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TransformObjeto : ScriptableObject
{
    public List<Vector3> pos;
    public List<Quaternion> rot;
}
