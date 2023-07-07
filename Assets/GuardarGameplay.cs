using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarGameplay : MonoBehaviour
{
    public TransformObjeto tObjeto;
    public Transform tnave;
    public float frecuenciaSalvado;
    float f = 0;
    float t = 0;
    bool pararGrabar;
    private void Start()
    {
        f = 1/frecuenciaSalvado;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) pararGrabar = true;

        t+= Time.deltaTime;
        if(t > f && !pararGrabar)
        {
            tObjeto.pos.Add(tnave.position);
            tObjeto.rot.Add(tnave.rotation);
            t = 0;
        }
    }
}
