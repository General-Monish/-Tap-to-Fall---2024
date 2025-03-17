using UnityEngine;

public class BossIntroUI : MonoBehaviour
{
    public static BossIntroUI instance;
    public GameObject bossIntroPanel;

    private void Awake()
    {
        instance = this;
    }


    public void ShowBossIntro()
    {
        bossIntroPanel.SetActive(true);
    }

    public void HideBossIntro()
    {
        bossIntroPanel.SetActive(false);
        Smanager.Instance.OnBossIntroComplete(); // Now it spawns the boss
    }
}
