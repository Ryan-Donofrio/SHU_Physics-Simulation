using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float masterVolume = 1f;
    [SerializeField] AudioClip menuButtonSound;
    [SerializeField] GameObject soundInstance;

    private AudioSource audioSource;
    private List<GameObject> audioSourcePool = new List<GameObject>();
    private List<AudioSource> activeAudioPool = new List<AudioSource>();

    private static SoundManager soundManager;

    public static SoundManager GetSoundManager
    {
        get { return soundManager; }
    }

    private void Awake()
    {
        //Ensures only one instance of this script can exist at a time
        if (soundManager == null)
            soundManager = this;
        else
            Destroy(this.gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = masterVolume;

        for(int i = 0; i < 10; i++)
        {
            var newObj = Instantiate(soundInstance);
            audioSourcePool.Add(newObj);
            newObj.GetComponent<AudioSource>().volume = masterVolume;
            newObj.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (activeAudioPool.Count <= 0)
            return;

        //Replace this with an action
        foreach(AudioSource sound in activeAudioPool)
        {
            if (!sound.isPlaying)
            {
                sound.Stop();
                activeAudioPool.Remove(sound);
                sound.gameObject.SetActive(false);
            }
        }
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public void PressMenuButton()
    {
        PlaySound(menuButtonSound);
    }

    public void PlaySound(AudioClip newclip)
    {
        audioSource.clip = newclip;
        audioSource.Play();

        StartCoroutine(RemoveClip());
    }

    private IEnumerator RemoveClip()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.Stop();
        audioSource.clip = null;

        yield return null;
    }

    public void InstantiateSound(Vector3 newPosition, AudioClip newClip)
    {
        var audioSourcePrefab = FindInactiveAudioSource();
        audioSourcePrefab.SetActive(true);
        audioSourcePrefab.transform.position = newPosition;
        audioSourcePrefab.GetComponent<AudioSource>().clip = newClip;
        audioSourcePrefab.GetComponent<AudioSource>().volume = masterVolume;
        activeAudioPool.Add(audioSourcePrefab.GetComponent<AudioSource>());
        audioSourcePrefab.GetComponent<AudioSource>().Play();
    }

    public void InstantiateSound(Vector3 newPosition, AudioClip newClip, float newVolume)
    {
        var audioSourcePrefab = FindInactiveAudioSource();
        audioSourcePrefab.SetActive(true);
        audioSourcePrefab.transform.position = newPosition;
        audioSourcePrefab.GetComponent<AudioSource>().clip = newClip;
        audioSourcePrefab.GetComponent<AudioSource>().volume = newVolume;
        activeAudioPool.Add(audioSourcePrefab.GetComponent<AudioSource>());
        audioSourcePrefab.GetComponent<AudioSource>().Play();
    }

    private GameObject FindInactiveAudioSource()
    {
        foreach(GameObject obj in audioSourcePool)
        {
            if (!obj.activeSelf)
                return obj;
        }

        //This code only runs if all pooled AudioSources are active
        var newObj = Instantiate(soundInstance);
        audioSourcePool.Add(newObj);
        newObj.GetComponent<AudioSource>().volume = masterVolume;
        return newObj;
    }
}
