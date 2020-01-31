using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{

    public static OrbManager instance;

    public GameObject[] orbPrefabs;

    public List<SpellOrbController> orbs = new List<SpellOrbController>();

    float defaultSpawnSpeed = 1.5f;
    float spawnSpeed;

    //route points
    //each orb has own column
    float middlePointMin;
    float middlePointMax;
    Vector2[] orbColumns = new Vector2[4];  // using vector2 array to save the x coordinate bounds of orb columns


    [SerializeField]
    Transform[] startPositions;
    [SerializeField]
    GameObject playerPoint;

    //orb speed
    float defaultOrbSpeed = 0.5f;
    float orbSpeed;

    //burst
    float burstDelay;
    float burstDelayMin = 10f;
    float burstDelayMax = 20f;
    float burstDuration = 1f;
    public bool bursting = false;

    //effects for orb spawning
    [SerializeField]
    ParticleSystem[] spawningEffects = new ParticleSystem[2];


    public Camera cam;

    bool testBool = false;

    [SerializeField] Animator enemyAnim;

    void Start()
    {

        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }


        spawnSpeed = defaultSpawnSpeed;
        orbSpeed = defaultOrbSpeed;

        GetRouteArea();     //get size of the screen and divide it to four columns
    }


    void Update(){
        // if(Input.GetKeyUp(KeyCode.Space)){
        //     Debug.Log("sss");
        //     if(testBool){
        //         enemyAnim.SetBool("bursting",false);
        //         testBool = false;
        //     }
        //     else{
        //         enemyAnim.SetBool("bursting", true);
        //         testBool = true;
        //     }
        // }
    }


    public void StartCreatingOrbs()
    {
        StartCoroutine("Create");
    }

    IEnumerator Create()
    {

        StartCoroutine("AdjustSpawnSpeed");


        while (GameManager.instance.isGameOver == false)
        {
            CreateOrb();

            yield return new WaitForSeconds(spawnSpeed);



        }

    }

    void CreateOrb()
    {
        int rand = Random.Range(0, orbPrefabs.Length);
        //int posRand = Random.Range(0, 2); //which startposition to use
        GameObject go = Instantiate(orbPrefabs[rand], startPositions[rand].position, Quaternion.identity);

        SpellOrbController tempContr = go.GetComponent<SpellOrbController>();
        tempContr.route = CreateRoutePoints(rand);
        tempContr.speed = orbSpeed;
        orbs.Add(tempContr);

        int tempRand = 0;   //two particle effects for each side, which in turn have 2 spawn positions so first 2 have particle 0 and next 2 particle 1
        if (rand > 1)
        {
            tempRand = 1;
        }
        spawningEffects[tempRand].GetComponent<particleController>().ChangeColor(tempContr.mainColor);
        spawningEffects[tempRand].Play();


        //AdjustSpawnSpeed();

        if (orbSpeed < 3f)
        {
            float increase = ((float)GameManager.instance.level * 4) / 100;
            orbSpeed = defaultOrbSpeed + increase;
        }

    }

    IEnumerator AdjustSpawnSpeed()
    {
        burstDelay = Random.Range(burstDelayMin, burstDelayMax);


        while (GameManager.instance.isGameOver == false)
        {
            burstDelay -= Time.deltaTime;
            if (burstDelay < 0 && !bursting)
            {
                Debug.Log("BURST LEVEL IS OVER 9000");
                enemyAnim.SetBool("bursting", true);
                yield return new WaitForSeconds(1.5f);
                bursting = true;
                spawnSpeed = 0.3f;
            }
            if (bursting)
            {
                burstDuration -= Time.deltaTime;
                if (burstDuration < 0)
                {
                    bursting = false;
                    burstDelay = Random.Range(burstDelayMin, burstDelayMax);
                    burstDuration = 2f;
                    enemyAnim.SetBool("bursting", false);
                }
            }




            if (!bursting)
            {

                //update spawnspeed
                if (spawnSpeed > 0.05f)
                {
                    float decrease = ((float)GameManager.instance.level * 8) / 100;
                    spawnSpeed = defaultSpawnSpeed - decrease;
                }


            }
            yield return new WaitForEndOfFrame();
        }
        enemyAnim.SetBool("bursting", false);
        burstDelay = Random.Range(burstDelayMin, burstDelayMax);
        bursting = false;
    }





    public void DestroyOrbs()
    {
       
        foreach (SpellOrbController soc in orbs)
        {

            soc.DestroyOrb();


        }
        
        orbs = new List<SpellOrbController>();
    }



    GameObject[] CreateRoutePoints(int index)
    {

        GameObject[] points = new GameObject[4];

        //start point
        GameObject tempPoint1 = new GameObject("point1");
        tempPoint1.transform.position = startPositions[index].position;
        //tempPoint1.transform.position = new Vector2(Random.Range(point1min, point1max), 2.6f);
        points[0] = tempPoint1;
        //2nd point
        GameObject tempPoint2 = new GameObject("point2");
        tempPoint2.transform.position = new Vector2(Random.Range(orbColumns[index].x, orbColumns[index].y), 0.45f);
        points[1] = tempPoint2;
        //3rd point
        GameObject tempPoint3 = new GameObject("point3");
        tempPoint3.transform.position = new Vector2(Random.Range(orbColumns[index].x, orbColumns[index].y), -1.3f);
        points[2] = tempPoint3;
        //last point
        GameObject tempPoint4 = new GameObject("point4");
        tempPoint4.transform.position = playerPoint.transform.position;
        points[3] = tempPoint4;


        return points;

    }
    void GetRouteArea()
    {
        Vector3 p = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        middlePointMax = p.x;
        p = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        middlePointMin = p.x;

        float columnWidth;
        float xArea;
        xArea = middlePointMax - middlePointMin;
        columnWidth = xArea / 4;
        //1st column
        orbColumns[0] = new Vector2(middlePointMin, middlePointMin + columnWidth);
        //2nd column
        orbColumns[1] = new Vector2(middlePointMin + columnWidth, middlePointMin + columnWidth * 2);
        //3rd column
        orbColumns[2] = new Vector2(middlePointMin + columnWidth * 2, middlePointMin + columnWidth * 3);
        //4th column
        orbColumns[3] = new Vector2(middlePointMin + columnWidth * 3, middlePointMax);

    }

}
