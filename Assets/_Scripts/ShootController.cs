using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ShootController : MonoBehaviour
{
public int damage =20;
public float TimeBullet=0.15f;
public float range=100f;
public Player player;

private float timer;

private Ray shootRay;
private RaycastHit shootHit;
private int shootableMask;
private LineRenderer gunLine;
private Light gunLight;
private float effectGun = 0.2f;
private AudioSource audio;

private void Update()
{
    timer += Time.deltaTime;
    if (Input.GetButton("Fire1") && timer >= TimeBullet)
    {
        Shoot();
    }

    if (timer>=TimeBullet*effectGun)
    {
        DisableEffects();
    }
}

// Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        shootableMask = LayerMask.GetMask("Shooteable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();

    }

    void Shoot()
    {
        audio.Play();
        timer = 0f;
        gunLight.enabled = true;
        gunLine.enabled = true;
        gunLine.SetPosition(0,transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            Enemy enemy = shootHit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takeDamage();
            }
            gunLine.SetPosition(1,shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1,shootRay.origin+shootRay.direction*range);
        }


    }

    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
