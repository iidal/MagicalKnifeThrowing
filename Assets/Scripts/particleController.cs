using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour
{
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    [SerializeField]
    float lifeTime = 2f;
    [SerializeField]
    bool destroyAfterLifeTime = true;
    [SerializeField]
    bool adjustOnStart = false;

    [SerializeField]
    bool makeColorLighter = true;
    [SerializeField]
    bool randomizeColor = false;
    public List<Color> colorsToRandomize = new List<Color>();



    void Start()
    {
        if (adjustOnStart)
        {
            AdjustColors(Color.white);  //just a placeholder color
        }
    }

    void Update()
    {
        if (destroyAfterLifeTime)
        {
            if (lifeTime > 0)
            {
                lifeTime -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void AdjustColors(Color mainColor)
    {
        if (makeColorLighter)
        {
            float i = 0;
            foreach (ParticleSystem ps in particles)
            {
                var main = ps.main;
                float stupidMultiplier = (i / 100) * 10;
                Color tempColor = new Color(mainColor.r + stupidMultiplier, mainColor.g + stupidMultiplier, mainColor.b + stupidMultiplier, mainColor.a);
                main.startColor = tempColor;
                i++;
            }

        }
        else if (randomizeColor)
        {
            StartCoroutine("LerpColor");
        }
    }
    IEnumerator LerpColor()
    {
        Color tempColor = colorsToRandomize[0];
        Color currentColor = tempColor;
        Color nextColor = colorsToRandomize[1];
        int currentIndex = 0;
        float t = 0f;
        while (true)
        {
            tempColor = Color.Lerp(currentColor, nextColor, t);
            t += 0.5f * Time.deltaTime;
            if (t > 1.0f)
            {

                    currentColor = nextColor;
                    if (currentIndex >= colorsToRandomize.Count-1)
                    {
                        currentIndex = 0;
                        nextColor = colorsToRandomize[0];
                    }
                    else
                    {
                        currentIndex++;
                        nextColor = colorsToRandomize[currentIndex];
                    }
                t = 0.0f;          
            }

            foreach (ParticleSystem ps in particles)
            {
                var main = ps.main;
                Color temp = new Color(tempColor.r, tempColor.g, tempColor.b, main.startColor.color.a);
                main.startColor = temp;
            }
            yield return null;

        }

    }
}
