// IScoreManager.cs
// 计分管理器接口 — 模块契约
// 实现方：src/gameplay/ScoreManager.cs
// 调用方：ui（显示分数）、gameplay（连击计算）

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// 计分管理器接口。
    /// 接收判定事件，计算分数和连击。
    /// </summary>
    public interface IScoreManager
    {
        /// <summary>处理判定事件，更新分数</summary>
        void ProcessJudgement(JudgementEvent judgement);

        /// <summary>获取当前分数</summary>
        long GetScore();

        /// <summary>获取当前连击数</summary>
        int GetCombo();

        /// <summary>获取最大连击数</summary>
        int GetMaxCombo();

        /// <summary>获取准确率（0.0 ~ 1.0）</summary>
        float GetAccuracy();

        /// <summary>获取评级（S/A/B/C/D）</summary>
        string GetGrade();

        /// <summary>获取完整统计</summary>
        ScoreStats GetStats();

        /// <summary>重置计分</summary>
        void Reset();
    }

    /// <summary>
    /// 计分统计。
    /// </summary>
    public struct ScoreStats
    {
        public long Score;
        public int Combo;
        public int MaxCombo;
        public float Accuracy;
        public string Grade;
        public JudgementStats Judgements;
    }
}