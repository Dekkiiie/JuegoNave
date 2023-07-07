using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirGameplay : MonoBehaviour
{
    
    public TransformObjeto tObjeto;
    public Transform tNave;

    public float frecuencia;

    float f;
    float t;

    private void Start()
    {
        f = 1/frecuencia;

        StartCoroutine(Reproducir());
    }

    IEnumerator Reproducir()
    {
        float tiempo = 0;
        int totalIndex = tObjeto.pos.Count;


        for (int i = 0; i < totalIndex; i++)
        {
            tiempo = 0;
            while (tiempo < f)
            {
                tiempo += Time.deltaTime;
                float perc = tiempo / f;

                if(i < totalIndex -2)
                {
                    tNave.position = Vector3.Lerp(tObjeto.pos[i], tObjeto.pos[i + 1], perc);
                    tNave.rotation = Quaternion.Lerp(tObjeto.rot[i], tObjeto.rot[i + 1], perc);
                }

                yield return null;

            }
        }

    }

}
