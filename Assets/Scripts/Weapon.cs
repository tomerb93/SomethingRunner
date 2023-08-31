using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] ParticleSystem muzzleFlash;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            FPCamera.GetComponent<Zoom>().currentZoom = 1f;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            FPCamera.GetComponent<Zoom>().currentZoom = 0f;
        }
    }

    private void Shoot()
    {
        StartCoroutine(StartShootingVisual());
        PlayMuzzleFlash();
        ProcessRayCast();
    }

    private IEnumerator StartShootingVisual()
    {
        transform.Translate(Vector3.back * 0.01f);
        Debug.Log("Before");
        yield return new WaitForSeconds(0.2f);
        Debug.Log("After");
        transform.Translate(Vector3.forward * 0.01f);
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        bool hitFound = Physics.Raycast(FPCamera.transform.position,
            FPCamera.transform.forward,
            out hit, range);
        if (!hitFound) return;

        // TODO: add some hit effect for visual players
        EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

        if (target == null) return;

        target.TakeDamage(damage);
    }
}
