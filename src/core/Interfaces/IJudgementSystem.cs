// IJudgementSystem.cs
// 判定系统接口 — 模块契约
// 实现方：src/gameplay/JudgementSystem.cs
// 调用方：ui（显示判定结果）、gameplay/ScoreManager（计分）

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// 判定结果枚举。
    /// </summary>
    public enum Judgement
    {
        Perfect,
        Great,
        Good,
        Miss
    }

    /// <summary>
    /// 判定事件数据。
    /// </summary>
    public struct JudgementEvent
    {
        /// <summary>判定结果</summary>
        public Judgement Result;

        /// <summary>时间偏差（秒，正=晚了，负=早了）</summary>
        public double TimeDeviation;

        /// <summary>对应的 Note 索引</summary>
        public int NoteIndex;

        /// <summary>判定时间戳</summary>
        public double Timestamp;
    }

    /// <summary>
    /// 判定系统接口。
    /// 接收输入事件和谱面数据，产生判定结果。
    /// 输入和判定完全解耦：任何有效输入可触发任何判定。
    /// </summary>
    public interface IJudgementSystem
    {
        /// <summary>判定事件回调</summary>
        event System.Action<JudgementEvent> OnJudgement;

        /// <summary>加载谱面数据</summary>
        /// <param name="chart">谱面数据</param>
        void LoadChart(ChartData chart);

        /// <summary>处理输入事件</summary>
        /// <param name="input">输入事件</param>
        void ProcessInput(InputEvent input);

        /// <summary>每帧更新（用于自动 Miss 检测）</summary>
        /// <param name="currentTime">当前音频时间（秒）</param>
        void Update(double currentTime);

        /// <summary>获取判定统计</summary>
        JudgementStats GetStats();

        /// <summary>重置判定状态</summary>
        void Reset();
    }

    /// <summary>
    /// 判定统计。
    /// </summary>
    public struct JudgementStats
    {
        public int PerfectCount;
        public int GreatCount;
        public int GoodCount;
        public int MissCount;
        public int MaxCombo;
        public int CurrentCombo;
    }

    /// <summary>
    /// 谱面数据（简化，完整定义见 src/chart/FORMAT.md）。
    /// </summary>
    public struct ChartData
    {
        public NoteData[] Notes;
        public double BPM;
        public double Offset;
    }

    /// <summary>
    /// 单个 Note 数据。
    /// </summary>
    public struct NoteData
    {
        /// <summary>Note 时间（秒）</summary>
        public double Time;

        /// <summary>Note 通道/轨道</summary>
        public int Channel;

        /// <summary>Note 类型（Tap, Hold, Slide 等）</summary>
        public int Type;

        /// <summary>持续时间（仅 Hold/Slide，秒）</summary>
        public double Duration;
    }
}