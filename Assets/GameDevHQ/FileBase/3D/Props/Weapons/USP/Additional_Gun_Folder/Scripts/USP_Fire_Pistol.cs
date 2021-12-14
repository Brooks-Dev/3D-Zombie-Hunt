using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USP_Fire_Pistol : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smoke;

    [SerializeField]
    private ParticleSystem _bulletCasing;

    [SerializeField]
    private ParticleSystem _muzzleFlashSide;

    [SerializeField]
    private ParticleSystem _Muzzle_Flash_Front;

    private Animator _anim;

    [SerializeField]
    private AudioClip _gunShotAudioClip;

    [SerializeField]
    private AudioSource _audioSource;

    private int _ammo = 15;

    public bool FullAuto;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _ammo > 0)
        {
            Debug.Log("Shoot gun");
            if (FullAuto == false)
            {
                _anim.SetTrigger("Fire");
            }

            if (FullAuto == true)
            {
                _anim.SetBool("Automatic_Fire", true);
            }
        }
 
        /*if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (FullAuto == true)
            {
                _anim.SetBool("Automatic_Fire", false);
            }

            if (FullAuto == false)
            {
                _anim.SetBool("Fire", false);
            }
        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            _anim.SetTrigger("Reload");
            _ammo = 15;
        }
    }

    public void FireGunParticles()
    {
        Debug.Log("Fired gun particles");
        _ammo--;
        _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        GunFireAudio();
    }

    public void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }
}
