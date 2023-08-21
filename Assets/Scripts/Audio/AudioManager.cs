using System;
using GameResource;
using Platform;
using UnityEngine;
using VContainer;

namespace Audio
{
    public class AudioManager : IDisposable
    {
        private readonly AudioSource _audioSource;

        private float _pitch;
        private readonly PlatformManager _platformManager;

        [Inject]
        public AudioManager(SceneResources sceneResources, PlatformManager platformManager)
        {
            _audioSource = sceneResources.audioHolder.GetComponent<AudioSource>();
            _pitch = _audioSource.pitch;

            _platformManager = platformManager;
            _platformManager.OnPerfectTap += PlayTapSound;
            _platformManager.OnNormalTap += ResetPitch;
        }

        private void PlayTapSound()
        {
            _audioSource.Play();
            
            _pitch *= 1.2f;
            _pitch = Mathf.Min(_pitch, 3f);
            _audioSource.pitch = _pitch;
        }

        private void ResetPitch()
        {
            _pitch = 1f;
            _audioSource.pitch = _pitch;
        }

        public void Dispose()
        {
            _platformManager.OnPerfectTap -= PlayTapSound;
            _platformManager.OnNormalTap -= ResetPitch;
        }
    }
}