using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowingManager : MonoBehaviour
{

    public static ThrowingManager instance;
    public OrbManager orbManager;
    public PlayerManager playerManager;
    public GameObject knife;
    public Transform knifeSpawn;

    Animator playerAnim;

    public delegate void ThrowAndDelete(SpellOrbController o);  //destroying knife and orb when they meet

     #region tapped wrong icon 
    [Header ("tapped wrong orb")]
    [SerializeField]
    Image blockImage;
    [SerializeField]
    ParticleSystem fuckUpBurst;
    #endregion
    #region audio
    [Header ("Audio")]
    AudioSource orbExplAS;
    [SerializeField]
    AudioSource throwAS;
    [SerializeField]
    List<AudioClip> throwSounds = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> orbExplosionSounds = new List<AudioClip>();
    [SerializeField]
    AudioClip missedOrbClip;
    float throwAsOgVol;
    #endregion

    public TextMeshProUGUI testiteksti;

   

    void Start(){
        playerAnim = GetComponent<Animator>();
        orbExplAS = GetComponent<AudioSource>();
        throwAsOgVol = throwAS.volume;

        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }

    }

    public void TestiTektiVaihto(){
        testiteksti.text ="tuhoa se vitun orb";
    }


    public void ThrowKnife(string icon){
        StartCoroutine("Throw", icon);
    }




    IEnumerator Throw(string icon)
    {
        bool orbDestroyed = false; // change to true if a orb is destroyed. if false after the foreach loop, punish the player

        int i = 0;
        foreach (SpellOrbController soc in orbManager.orbs)
        {
            if (soc.icon == icon)
            {
                testiteksti.text = "oikea orbsi";


                ThrowAndDelete callback = DeleteOrbAndKinfe;
                GameObject k = Instantiate(knife, knifeSpawn.position, Quaternion.identity);
                k.GetComponent<KnifeController>().StartMove(soc.gameObject.transform, callback, soc);
                playerAnim.Play("ThrowKnife");
                throwAS.pitch = Random.Range(0.8f, 1.15f);
                //throwAS.PlayOneShot(throwSounds[Random.Range(0, throwSounds.Count)]);
                throwAS.PlayOneShot(throwSounds[0]);
                orbManager.orbs.RemoveAt(i);
                orbDestroyed = true;
                yield return null;
                break;

            }
            i++;
        }
       
        if(orbDestroyed == false){
            StartCoroutine("TappedWrong");
        }


    }

    IEnumerator TappedWrong(){
        blockImage.enabled = true;
        throwAS.volume = 0.5f;
        throwAS.PlayOneShot(missedOrbClip);
        fuckUpBurst.Play();
        yield return new WaitForSeconds(0.3f);
        blockImage.enabled = false;
        throwAS.volume = throwAsOgVol;
    }

    public void DeleteOrbAndKinfe(SpellOrbController spellOrb){

        testiteksti.text = "NONIIN TUHOTTU";


        spellOrb.DestroyOrb();
        playerManager.AddPoints();
        //orbExplAS.pitch = Random.Range(0.8f, 1f);
        orbExplAS.PlayOneShot(orbExplosionSounds[Random.Range(0, orbExplosionSounds.Count)]);
    }
}
