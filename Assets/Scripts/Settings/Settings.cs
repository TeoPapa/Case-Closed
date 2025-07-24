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

    public GameObject TestingPanel;
    public TMP_Text Money;

    protected void Awake() {
        GameHandler.Load();
    }

    protected void Start() {
        SettingsPanel.SetActive(false);
        TestingPanel.SetActive(false);
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

    public void OpenTesting() {
        string CodeW = CodeField.text;

        if (CodeW.Equals(GameHandler.Code))
            Open();
    }

    void Open() {
        TestingPanel.SetActive(true);
        Money.text = GameHandler.Money.ToString();
    }

    public void AddM() {
        int mon = GameHandler.Money;
        mon += GameHandler.moneyValue;

        if(mon > 1000) mon = 1000;
        Money.text = mon.ToString();
        GameHandler.Money = mon;
    }

    public void DestroyThem() {
        Blockade[] b = FindObjectsByType<Blockade>(FindObjectsSortMode.None);

        foreach(Blockade bl in b) {
            foreach (Destroyable d in bl.dest)
                d.DestroyMe(false);
        }
    }

    public void ResetThem() {
        GameHandler.Money = 0;
        GameHandler.moneyValue = 5;

        GameHandler.hasPlayedBefore = false;

        GameHandler.PlayerPosition = new Vector3(-270.5f, -4.4f, 0);

        GameHandler.Case = null;

        GameHandler.LevelsPlayed = new List<Level>();

        GameHandler.Clear();

        GameHandler.DefaultScene = "MainMenu";

        GameHandler.MusicVolume = 1f;
        GameHandler.SfxVolume = 1f;

        Saver.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void SceneCh() {
        SceneManager.LoadScene("MainMenu");
    }

    
}

