using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FubarOps.Audio
{
    /// <summary>
    /// Responsible for playing unit-related general sounds (footsteps, etc.).
    /// </summary>
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource footstepsAudioSource;
        [SerializeField] List<AudioClip> footstepClips;
        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        IEnumerator Start()
        {
            while (true)
            {
                if (rb.velocity.x != 0 || rb.velocity.y != 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, footstepClips.Count);
                    footstepsAudioSource.PlayOneShot(footstepClips[randomIndex]);
                }

                yield return new WaitForSeconds(0.6f);
            }

        }
    }
}