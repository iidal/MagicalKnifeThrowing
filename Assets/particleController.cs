using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour
{
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    [SerializeField]
    float lifeTime = 2f;

    [SerializeField]
    bool makeColorLighter = true;


 
    void Update()
    {
        if(lifeTime > 0){
            lifeTime -= Time.deltaTime;
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void AdjustColors(Color mainColor){
        if(makeColorLighter){
            float i = 0;
            foreach (ParticleSystem ps in particles)
            {
                var main = ps.main;
                float stupidMultiplier = (i/100)*10;
                Color tempColor = new Color(mainColor.r + stupidMultiplier, mainColor.g+ stupidMultiplier, mainColor.b+ stupidMultiplier, mainColor.a);
                main.startColor = tempColor;
                i++;
            }

        }
    }
}
