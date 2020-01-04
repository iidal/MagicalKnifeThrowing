using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingManager : MonoBehaviour
{

    public OrbManager orbManager;
    public PlayerManager playerManager;
    public GameObject knife;
    public Transform knifeSpawn;

    Animator playerAnim;

    public delegate void ThrowAndDelete(SpellOrbController o);  //destroying knife and orb when they meet

    AudioSource throwAS;
    [SerializeField]
    List<AudioClip> throwSounds = new List<AudioClip>();

    void Start(){
        playerAnim = GetComponent<Animator>();
        throwAS = GetComponent<AudioSource>();
    }

    public void ThrowKnife(string icon){
        StartCoroutine("Throw", icon);
    }
    IEnumerator Throw(string icon)
    {
        int i = 0;
        foreach (SpellOrbController soc in orbManager.orbs)
        {
            if (soc.icon == icon)
            {
                
                ThrowAndDelete callback = DeleteOrbAndKinfe;
                GameObject k = Instantiate(knife, knifeSpawn.position, Quaternion.identity);
                k.GetComponent<KnifeController>().StartMove(soc.gameObject.transform, callback, soc);
                playerAnim.Play("ThrowKnife");
                throwAS.pitch = Random.Range(0.8f, 1.15f);
                throwAS.PlayOneShot(throwSounds[Random.Range(0, throwSounds.Count)]);
                orbManager.orbs.RemoveAt(i);
                yield return null;
                break;

            }
            i++;
        }


    }

    public void DeleteOrbAndKinfe(SpellOrbController spellOrb){
        spellOrb.DestroyOrb();
        playerManager.AddPoints();
    }
}
