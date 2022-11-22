using UnityEngine;

public class AudioEffects : MonoBehaviour
{
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip dragSound;
        [SerializeField] private AudioClip clickSound;
        [SerializeField] private AudioClip lockSound;
        [SerializeField] private AudioClip pickSound;
        [SerializeField] private AudioClip errorSound;
        [SerializeField] private AudioClip winSound;


        public void PlayDragSound() => source.PlayOneShot(dragSound);

        public void PlayClickSound() => source.PlayOneShot(clickSound);
        public void PlayPickSound() => source.PlayOneShot(pickSound);
        public void PlayLockSound() => source.PlayOneShot(lockSound);
        public void PlayErrorSound() => source.PlayOneShot(errorSound);
        public void PlayWinSound() => source.PlayOneShot(winSound);

}