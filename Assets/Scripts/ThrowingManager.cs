using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingManager : MonoBehaviour
{

    public OrbManager orbManager;
    public PlayerManager playerManager;
    public GameObject knife;
    public Transform knifeSpawn;

    public delegate void ThrowAndDelete(SpellOrbController o);


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
