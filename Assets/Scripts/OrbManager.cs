using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{

    public GameObject[] orbPrefabs;

    public List<SpellOrbController> orbs = new List<SpellOrbController>();

    //route points
    float point1min = -1.25f;
    float point1max = 1.25f;
    float middlePointMin = -2.5f;
    float middlePointMax = 2.5f;

    [SerializeField]
    GameObject playerPoint;

    void Start()
    {

    }


    public void StartCreatingOrbs(){
        StartCoroutine("Create");
    }

    IEnumerator Create()
    {
        while (GameManager.instance.isGameOver == false)
        {
            CreateOrb();
            yield return new WaitForSeconds(3f);
        }
    }

    void CreateOrb()
    {
        int rand = Random.Range(0, orbPrefabs.Length);
        GameObject go = Instantiate(orbPrefabs[rand], transform.position, Quaternion.identity);
        go.GetComponent<SpellOrbController>().route = CreateRoutePoints();
        orbs.Add(go.GetComponent<SpellOrbController>());

    }



    GameObject[] CreateRoutePoints()
    {

        GameObject[] points = new GameObject[4];

        //start point
        GameObject tempPoint1 = new GameObject("point1");
        tempPoint1.transform.position = new Vector2(Random.Range(point1min, point1max), 2.6f);
        points[0] = tempPoint1;
        //2nd point
        GameObject tempPoint2 = new GameObject("point2");
        tempPoint2.transform.position = new Vector2(Random.Range(middlePointMin, middlePointMax), 0.45f);
        points[1] = tempPoint2;
        //3rd point
        GameObject tempPoint3 = new GameObject("point3");
        tempPoint3.transform.position = new Vector2(Random.Range(middlePointMin, middlePointMax), -1.3f);
        points[2] = tempPoint3;
        //last point
        GameObject tempPoint4 = new GameObject("point4");
        tempPoint4.transform.position = playerPoint.transform.position;
        points[3] = tempPoint4;

        return points;

    }
    public void DestroyOrbs(){
        foreach (SpellOrbController soc in orbs)
        {
            soc.DestroyOrb();
        }
    }
}
