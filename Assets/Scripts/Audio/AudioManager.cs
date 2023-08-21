using System;
using GameResource;
using Platform;
using UnityEngine;
using VContainer;

namespace Audio
{
    public class AudioManager
    {
        private readonly AudioSource _audioSource;

        private float _pitch;

        [Inject]
        public AudioManager(SceneResources sceneResources, PlatformManager platformManager)
        {
            _audioSource = sceneResources.audioHolder.GetComponent<AudioSource>();
            _pitch = _audioSource.pitch;

            platformManager.OnPerfectTap += PlayTapSound;
        }

        private void PlayTapSound()
        {
            _audioSource.Play();
            
            _pitch *= 1.2f;
            _pitch = Mathf.Min(_pitch, 3f);
            _audioSource.pitch = _pitch;
        }
    }
}