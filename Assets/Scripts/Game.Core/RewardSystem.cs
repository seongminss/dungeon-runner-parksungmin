using System.Net.Cache;

namespace Game.Core
{
    /// 던전 러너 — 스테이지 보상 정산 (레거시)
    /// 이 클래스에는 9주차에서 배운 코드 스멜이 의도적으로 심어져 있다.
    /// 10주차: 이 파일을 "고치기 전에" 현재 동작을 테스트로 고정한다(특성화 테스트).
    public class RewardSystem
    {
        public string lastGrade;    // 최근 등급
        public int lastReward;      // 최근 보상액

        // clearTime: 클리어 시간(초), coin: 코인 수, bossKillCount: 보스 처치 수, diff: 난이도(1~3)
        public int ApplyStageReward(int clearTime, int coin, int bossKillCount, bool isEvent, int diff) {
            // --- 등급 판정 ---
            lastGrade = JudgeGrade(clearTime);

            // --- 보상 계산 ---
            lastReward = CalculrateReward(coin, bossKillCount, diff, isEvent, lastGrade);
            
            return lastReward;
        }

        public int CalculrateReward(int coin, int bossKillCount, int diff, bool isEvent, string grade) {
            int reward = coin * 10 + bossKillCount * 50;
            if (diff == 2) reward = (int)(reward * 1.2f);
            if (diff == 3) reward = (int)(reward * 1.5f);
            if (isEvent) reward = (int)(reward * 1.5f);
            if (grade == "S") reward += 100;
            return reward;
        }

        public string JudgeGrade(int clearTime) {
            string grade = "";
            if (clearTime < 60) grade = "S";
            else if (clearTime < 90) grade = "A";
            else if (clearTime < 120) grade = "B";
            else grade = "C";
            return grade;
        }

    }
}
