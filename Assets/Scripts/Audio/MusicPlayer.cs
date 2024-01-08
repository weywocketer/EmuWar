using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FubarOps.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] List<AudioClip> songs = new();
        [SerializeField] AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            StartCoroutine(PlaySong());
        }

        IEnumerator PlaySong()
        {
            AudioClip chosenSong = songs[UnityEngine.Random.Range(0, songs.Count)];

            while (true)
            {
                if (UnityEngine.Random.Range(1, 11) <= 7)
                {
                    chosenSong = songs[UnityEngine.Random.Range(0, songs.Count)];
                }

                audioSource.PlayOneShot(chosenSong);
                yield return new WaitForSeconds(chosenSong.length);
            }
        }
    }
}
