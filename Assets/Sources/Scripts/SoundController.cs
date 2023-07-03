using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSource;
    [SerializeField] private AudioSource _magazineSource;
    [SerializeField] private AudioSource _loadSource;
    [SerializeField] private AudioSource _shotWithoutSource;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Item _item;
    [SerializeField] private GameObject MuzzleFlash;
    

    private void Start() 
    {
       MuzzleFlash.SetActive(false);  
    }
    public void ShotSoundPlay()
    {
        _shotSource.Play();
    }
    public void ShotSoundStop()
    {
       _shotSource.Stop();
    }
     public void LoadSoundPlay()
    {
        _loadSource.Play();
    }

    public void MagazineSoundPlay()
    {
        _magazineSource.Play();
    }

    public void ShotWithout()
    {
       _shotWithoutSource.Play();
    }

    public void BulletFly()
    {   
       _bulletPool.Create(_item.BulletSpawn.position,Vector3.right,
                         _item.WeaponInfoo.BulletSprite);
       
    } 

    public void MuzzlePlay()
    {
       StartCoroutine(MuzlleFlashShow());
    }

     private IEnumerator MuzlleFlashShow()
    {  
       MuzzleFlash.SetActive(true);
       yield return new WaitForSeconds(0.08f);
       MuzzleFlash.SetActive(false);     
    } 
    


}
