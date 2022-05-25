using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public AudioClip audio;
    public AudioSource source;
    public float firerate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleflash;
    public GameObject impacteffect;
    public float impactforce = 80f;
    private float nexttimetofire = 0f;
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nexttimetofire)
        {
            nexttimetofire = Time.time + 1f / firerate;
            PostProcessingEffects._instance.Fire();
            Shoot();
            source.PlayOneShot(audio);
        }
    }

    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactforce);
            }

            GameObject ImpactGo = Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGo, 2f);
        }
    }
}
