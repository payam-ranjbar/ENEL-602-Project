using UnityEngine;

public class AudioEffects : MonoBehaviour
{
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip dragSound;
        [SerializeField] private AudioClip clickSound;
        [SerializeField] private AudioClip lockSound;
        [SerializeField] private AudioClip pickSound;


        public void PlayDragSound() => source.PlayOneShot(dragSound);

        public void PlayClickSound() => source.PlayOneShot(dragSound);
        public void PlayPickSound() => source.PlayOneShot(pickSound);
        public void PlayLockSound() => source.PlayOneShot(lockSound);

}