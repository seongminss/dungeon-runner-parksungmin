using UnityEngine;
using Game.Core;

namespace Game.Unity
{
    /// 데모 씬용 실행기 — 빈 씬의 아무 오브젝트에 부착하면 동작한다.
    /// (씬 파일은 저장소에 포함하지 않는다: 새 씬 > 빈 오브젝트 > 이 스크립트 부착 > 재생)
    /// Game.Core가 엔진 없이도, 엔진 안에서도 같은 결과를 낸다는 것을 보여 주는 최소 데모.
    public class DemoRunner : MonoBehaviour
    {
        int clearTime = 100, coin = 3, bossKillCount = 1;
        bool isEvent;
        string lastGrade = "";
        readonly RewardSystem rewardSystem = new RewardSystem();

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 340, 260), GUI.skin.box);
            GUILayout.Label("던전 러너 레거시 — 보상 정산 데모");
            GUILayout.Label($"클리어 시간 clearTime = {clearTime}s");
            clearTime = (int)GUILayout.HorizontalSlider(clearTime, 0, 180);
            GUILayout.Label($"코인 coin = {coin}");
            coin = (int)GUILayout.HorizontalSlider(coin, 0, 20);
            GUILayout.Label($"보스 bossKillCount = {bossKillCount}");
            bossKillCount = (int)GUILayout.HorizontalSlider(bossKillCount, 0, 3);
            isEvent = GUILayout.Toggle(isEvent, " 이벤트 기간");
            if (GUILayout.Button("정산 (ApplyStageReward)"))
            {
                int reward = rewardSystem.ApplyStageReward(clearTime, coin, bossKillCount, isEvent, diff: 1);
                lastGrade = rewardSystem.JudgeGrade(clearTime);
                Debug.Log($"보상 {reward}, 등급 {lastGrade}");
            }
            GUILayout.Label($"최근: {rewardSystem.lastReward} / 등급 {lastGrade}");
            GUILayout.EndArea();
        }
    }
}