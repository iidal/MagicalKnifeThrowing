using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public Transform target;
    float speed = 10f;

    public void StartMove(Transform tar, ThrowingManager.ThrowAndDelete callback, SpellOrbController orb)
    {
        target = tar;
        StartCoroutine(MoveTowardsOrb(callback, orb));
    }

    IEnumerator MoveTowardsOrb(ThrowingManager.ThrowAndDelete callback, SpellOrbController orb)
    {
        while (true)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            yield return new WaitForSeconds(0);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) <= 0.3f)
            {
                callback(orb);
                break;
            }
        }
        Destroy(this.gameObject);
    }
}
