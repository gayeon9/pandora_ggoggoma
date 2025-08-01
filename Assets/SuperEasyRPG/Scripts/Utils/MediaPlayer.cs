using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace SuperEasyRPG
{
    class AudioTrack
    {
        public AudioSource audioSource;
        public float originalVolume;

        public AudioTrack(AudioSource audioSource, float originalVolume)
        {
            this.audioSource = audioSource;
            this.originalVolume = originalVolume;
        }
    }

    public class MediaPlayer : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject videoSkipPanel;
        public RawImage videoPanel;
        public RenderTexture videoRenderTexture;
        public Image backgroundImage;

        VideoPlayer videoPlayer;
        Action<VideoClip> onVideoEnd;

        List<AudioTrack> audioTracks = new();
        List<Action<AudioClip>> audioEndHandlers = new();

        private void Awake()
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = videoRenderTexture;
            videoPlayer.playOnAwake = false;
        }

        void Update()
        {
            for (int i = 0; i < audioTracks.Count; i++)
            {
                AudioTrack track = audioTracks[i]; 
                if (!track.audioSource.isPlaying && track.audioSource.time > 0)
                {
                    OnAudioEnd(i);
                }
            }

            if (videoPlayer != null && videoPlayer.isPlaying && Input.GetKeyDown(KeyCode.Space))
            {
                SkipVideo(); 
            }
        }

        void OnDestroy()
        {
            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached -= OnVideoEnd;
            }
        }

        public void PlayVideo(VideoClip clip, Action<VideoClip> onVideoEnd = null)
        {
            FireAndForget(PlayVideoAsync(clip, onVideoEnd));
        }

        private async Task PlayVideoAsync(VideoClip clip, Action<VideoClip> onVideoEnd)
        {
            if (videoPlayer.isPlaying)
            {
                Debug.LogWarning("Another video is already playing. Stopping it and playing the new video instead.");
                videoPlayer.Stop();
            }

            videoPlayer.clip = clip;
            this.onVideoEnd = onVideoEnd;

            Debug.Log($"Play video clip: {clip.name}");

            SetPlayerCanMove(false);
            canvas.SetActive(true);

            // background dims and audio mutes
            await Task.WhenAll(
                InterpolateOverTime((alpha) => backgroundImage.color = new(0, 0, 0, alpha), 0, 1, 200),
                InterpolateOverTime(SetAudioTrackMasterVolume, 1, 0, 200)
            );

            videoPanel.color = new Color(1, 1, 1, 0);
            videoPlayer.Play();
            ShowVideoSkipText();

            // video fades in
            await InterpolateOverTime((alpha) => videoPanel.color = new Color(1, 1, 1, alpha), 0, 1, 200);
        }

        private void OnVideoEnd(VideoPlayer player)
        {
            FireAndForget(OnVideoEndAsync());
        }

        private async Task OnVideoEndAsync()
        {
            VideoClip clip = videoPlayer.clip;
            videoPlayer.clip = null;
            HideVideoSkipText();
            await Task.Delay(100); // WaitForSeconds(0.1f)

            // video fades out
            await InterpolateOverTime((alpha) => videoPanel.color = new Color(1, 1, 1, alpha), 1, 0, 200);

            // background and audio returns to normal
            await Task.WhenAll(
                InterpolateOverTime((alpha) => backgroundImage.color = new(0, 0, 0, alpha), 1, 0, 200),
                InterpolateOverTime(SetAudioTrackMasterVolume, 0, 1, 200)
            );

            canvas.SetActive(false);
            videoRenderTexture.Release();
            SetPlayerCanMove(true);

            if (onVideoEnd != null)
            {
                Action<VideoClip> handler = onVideoEnd;
                onVideoEnd = null;
                handler.Invoke(clip);
            }

            Debug.Log($"Video ended: {clip}");
        }

        private async Task InterpolateOverTime(Action<float> set, float from, float to, int durationMillis)
        {
            float interpolator = 0;
            int intervalMillis = durationMillis / 10;
            while (interpolator < 1f)
            {
                interpolator += 0.1f;
                float value = from + (to - from) * interpolator;
                set.Invoke(value);

                await Task.Delay(intervalMillis);
            }
        }

        private void SetPlayerCanMove(bool canMove)
        {
            Player player = FindObjectOfType<Player>();
            if (player == null)
                return;

            player.canMove = canMove;
        }

        private void ShowVideoSkipText()
        {
            videoSkipPanel.SetActive(true);
        }

        private void HideVideoSkipText()
        {
            videoSkipPanel.SetActive(false);
        }

        private void SkipVideo()
        {
            OnVideoEnd(videoPlayer);
        }

        public void PlayAudio(AudioClip clip, bool loop, float volume, Action<AudioClip> onAudioEnd = null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.loop = loop;

            AudioTrack track = new(audioSource, volume);
            audioTracks.Add(track);
            audioEndHandlers.Add(onAudioEnd);

            audioSource.volume = volume;
            audioSource.Play();
            Debug.Log($"Play audio clip: {clip.name}");
        }

        public void StopAudio(float fadeOutDelaySeconds, Action onAudioStop = null)
        {
            FireAndForget(StopAudioAsync(fadeOutDelaySeconds, onAudioStop)); 
        }

        private async Task StopAudioAsync(float fadeOutDelaySeconds, Action onAudioStop = null)
        {
            await InterpolateOverTime(SetAudioTrackMasterVolume, 1, 0, (int)(fadeOutDelaySeconds * 1000));
            foreach (AudioTrack track in audioTracks)
            {
                Destroy(track.audioSource);
            }
            audioTracks.Clear();
            audioEndHandlers.Clear();
            if (onAudioStop != null)
            {
                onAudioStop.Invoke();
            }
        }

        private void SetAudioTrackMasterVolume(float volume)
        {
            foreach (AudioTrack track in audioTracks)
            {
                track.audioSource.volume = track.originalVolume * volume;
            }
        }

        private void OnAudioEnd(int index)
        {
            AudioTrack track = audioTracks[index];
            Action<AudioClip> handler = audioEndHandlers[index];
            AudioClip clip = track.audioSource.clip;
            Debug.Log($"End of audio clip {clip}");
            Destroy(track.audioSource);
            audioTracks.RemoveAt(index);
            audioEndHandlers.RemoveAt(index);
            if (handler != null)
            {
                handler.Invoke(clip);
            }
        }

        private static void FireAndForget(Task task)
        {
            task.ContinueWith(t =>
            {
                Debug.LogError(t.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}

