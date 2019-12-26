using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellOrbController : MonoBehaviour
{

    public string icon = "swirl"; // get this when instatiated

    public GameObject[] route;
    private bool followCoroutineEnded = true;
    private float tParam;
    float speed = 0.5f;
    private Vector2 newPos;


    void Start()
    {
        
    }

     void Update(){

         if(followCoroutineEnded == true){
             StartCoroutine("FollowRoute");
         }

     }
    IEnumerator FollowRoute(){

        followCoroutineEnded = false;

        Vector2 p0 = route[0].transform.position;
        Vector2 p1 = route[1].transform.position;
        Vector2 p2 = route[2].transform.position;
        Vector2 p3 = route[3].transform.position;

        while(tParam <1){
            tParam += Time.deltaTime* speed;

            newPos = Mathf.Pow(1- tParam, 3) * p0 + 3 * Mathf.Pow(1- tParam, 2) * tParam* p1 +
                3* (1-tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            transform.position = newPos;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        followCoroutineEnded = true;

        //https://youtu.be/11ofnLOE8pw?t=409
       
    }


    public void DestroyOrb(){
        foreach(GameObject go in route){
            Destroy(go);
            Destroy(this.gameObject);
        }
    }
}
