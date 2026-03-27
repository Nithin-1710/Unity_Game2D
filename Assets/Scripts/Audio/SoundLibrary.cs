using UnityEngine;
[System.Serializable]
public struct SoundEffects
{
    public string groupID;
    public AudioClip[] clips;
}
public class SoundLibrary : MonoBehaviour
{
    public SoundEffects[] soundEffects;
    public AudioClip GetClipFromName(string name)
    {
        foreach(var soundeffect in soundEffects)
        {
            if (soundeffect.groupID == name)
            {
                return soundeffect.clips[Random.Range(0,soundeffect.clips.Length)];
            }
        }
        return null;
    }
}
