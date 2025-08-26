using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public TMP_Text MusicVolume;
    public TMP_Text SfxVolume;

    public TMP_InputField CodeField;

    public GameObject SettingsPanel;

    protected void Awake() {
        GameHandler.Load();
        Input.multiTouchEnabled = false;
    }

    protected void Start() {
        SettingsPanel.SetActive(false);
    }

    public void OpenSettings() {
        MusicVolume.text = (GameHandler.MusicVolume * 100).ToString();
        SfxVolume.text = (GameHandler.SfxVolume * 100).ToString();
        SettingsPanel.SetActive(true);
    }

    public void CloseSettings() {
        GameHandler.Save(true);
        SettingsPanel.SetActive(false);
    }
    public void ChangeSfxVolume(UnityEngine.UI.Slider sl) {
        float vlm = sl.value;
        float value = ((int) vlm) / 100f;

        SfxVolume.text = ((int)vlm).ToString();
        AudioSource[] Sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        GameHandler.SfxVolume = value;

        foreach (AudioSource s in Sources) {
            s.volume = value;
        }

        MusicChange(GameHandler.MusicVolume*100);
    }

    void MusicChange(float vlm) {
        float value = ((int)vlm) / 100f;

        if (GameHandler.MusicVolume != value) MusicVolume.text = ((int)vlm).ToString();
        GameHandler.MusicVolume = value;
        FindFirstObjectByType<AudioManager>().setMusicVolume();
    }

    public void ChangeMusicVolume(UnityEngine.UI.Slider sl) {
        MusicChange(sl.value);
    }

   

    public void SceneCh(string s) {
        SceneManager.LoadScene(s);
    }

    
}

