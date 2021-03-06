using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //Change this values to create new types of guns.
    [Header("Gun Settings")]
    [SerializeField] float startingAmmo;
    [SerializeField] float Ammo;
    [SerializeField] float Magazines;
    [SerializeField] float ReloadTime;
    [SerializeField] float BulletSpeed;
    [SerializeField] float FireRate;

    //Reloading and fire rate
    private bool FireSwitch;
    private bool isReloading;
    private float timeToFire = 0;
    private float initialFireRate;

    [Header("Gun Setup")]
    //Select the tip of the gun from where the bullets will exit the gun.
    [SerializeField] Transform gunTip;

    //Select the ammunition you want your weapon tu use.
    [SerializeField] GameObject AmmoType;

    //Animator for the gun shoot mechanic.
    [SerializeField] Animator MusketAnimator;

    [Header("Gun FX")]
    //Weapon shoot SFX
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] float ShootSFXVolume = 1f;

    //Weapon shoot muzzle flash
    [SerializeField] ParticleSystem muzzleFlash;

    void Start()
    {
        modeChecker();
        audioSource = GetComponent<AudioSource>();
        initialFireRate = FireRate;
        startingAmmo = Ammo;
        isReloading = false;
    }

    void Update()
    {
        fireSelected();
        ShootWeapon();
        Reload();
    }

    void ShootWeapon()
    {
        //Single shot firing logic
        if (FireRate == 0)
        {
            if (Input.GetButtonDown("Fire1") && Ammo >= 1)
            {
                Debug.Log("Firing single shot!");
                Shoot();
            }
        }
        //Automatic firing logic
        else if (FireRate != 0)
        {
            if (Input.GetButton("Fire1") && Ammo >= 1 && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / FireRate;
                Debug.Log("Firing automatic!");
                Shoot();
            }
        }

        //Ammo checker
        if ((Input.GetButtonDown("Fire1") && Ammo <= 1))
        {
            Debug.Log("No ammo left!");
        }
    }

    //Main Shoot mechanic && projectile effects
    void Shoot()
    {
        MusketAnimator.SetTrigger("Shoot");
        GameObject Projectile = Instantiate(AmmoType, gunTip.position, gunTip.rotation);
        Rigidbody rb = Projectile.GetComponent<Rigidbody>();
        rb.AddForce(gunTip.forward * BulletSpeed, ForceMode.Impulse);
        audioSource.PlayOneShot(ShootSFX, ShootSFXVolume);
        Ammo -= 1f;
        muzzleFlash.Play(true);
    }

    //Switcher for different fire modes, semi-auto and full auto.
    void fireSelected()
    {
        if (Input.GetKeyDown(KeyCode.F) && FireSwitch == false)
        {
            FireSwitch = true;
            FireRate = initialFireRate;
            Debug.Log("Switching to automatic!");
        }
        else if (Input.GetKeyDown(KeyCode.F) && FireSwitch == true)
        {
            FireSwitch = false;
            FireRate = 0f;
            Debug.Log("Switching to single shot!");
        }
    }

    //Runs at the beggining of the scene and updates the weapon firing system to match the selected one.
    void modeChecker()
    {
        if (FireRate != 0)
        {
            FireSwitch = true;
        }
        if (FireRate == 0)
        {
            FireSwitch = false;
        }
    }

    //Reload mechanic
    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && Magazines >= 1)
        {
            Debug.Log("Reloading...");
            StartCoroutine(Reloading());
        }
        else if ((Input.GetKeyDown(KeyCode.R) && Magazines <= 1))
        {
            Debug.Log("No mags left!");
        }
    }

    //Reload delay
    public IEnumerator Reloading()
    {
        Debug.Log("Starting Reload...");
        isReloading = true;
        yield return new WaitForSeconds(ReloadTime);
        Debug.Log("Reloading complete!");
        Ammo = startingAmmo;
        isReloading = false;
        Magazines -= 1f;
    }
}