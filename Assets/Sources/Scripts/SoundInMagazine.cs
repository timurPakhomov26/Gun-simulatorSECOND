using UnityEngine;

public class SoundInMagazine : MonoBehaviour
{
    [SerializeField] private SoundController _soundController;


    public void NewMagazinePlay()
    {

    }

    public void MagazineSoundPlay()
    {
       _soundController.MagazineSoundPlay();
    }
}
