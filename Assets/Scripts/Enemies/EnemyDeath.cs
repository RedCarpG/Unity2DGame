using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private float deathTime = 1f;
    [SerializeField] private GameObject deathEffect;

    public void Death()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        FallDeath();
    }
    public void FallDeath()
    {
        this.GetComponent<Animator>().SetBool("isDead", true);
        foreach (var aCollider in this.GetComponents<Collider2D>())
        {
            aCollider.enabled = false;
        }
        Destroy(this.gameObject, deathTime);
    }

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        GameObject deathEffectClone = Instantiate(deathEffect, this.transform.position, Quaternion.identity);
        Destroy(deathEffectClone, 1);
    }

}
