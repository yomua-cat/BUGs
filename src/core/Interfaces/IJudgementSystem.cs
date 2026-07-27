// IJudgementSystem.cs
// 判定系统接口 — 模块契约
// 实现方：src/gameplay/JudgementSystem.cs
// 调用方：ui（显示判定结果）、gameplay/ScoreManager（计分）、FxBus（触发 Fx）

using System;
using System.Collections.Generic;

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

        /// <summary>判定时间戳（音频时钟，秒）</summary>
        public double Timestamp;

        /// <summary>Note 判定时刻位置（归一化坐标）</summary>
        public Vector2 NotePosition;

        /// <summary>Note 深度</summary>
        public float NoteDepth;

        /// <summary>该 Note 携带的 Fx 模块标识符（可为空）</summary>
        public string FxModuleId;

        /// <summary>该 Note 携带的 Fx 参数（可为空）</summary>
        public Dictionary<string, object> FxParams;

        /// <summary>关联的判定点 ID</summary>
        public string JudgePointId;
    }

    /// <summary>
    /// 2D 向量（归一化坐标）。
    /// </summary>
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y) { X = x; Y = y; }
    }

    /// <summary>
    /// 判定点数据（运行时可变）。
    /// </summary>
    public struct JudgePointData
    {
        public string Id;
        public Vector2 Position;   // x, y ∈ [-1, 1]
        public float Depth;        // z ∈ [0, 1]
        public float Angle;        // degrees ∈ [0, 360)
        public float Radius;       // 判定半径
        public JudgePointMode Mode;
    }

    /// <summary>
    /// 判定点模式。
    /// </summary>
    public enum JudgePointMode
    {
        Judge,      // 仅参与判定
        Perform,    // 仅作为演出锚点
        Both        // 同时参与判定与演出
    }

    /// <summary>
    /// 判定系统接口。
    /// 接收输入事件和谱面数据，产生判定结果。
    /// 输入和判定完全解耦：任何有效输入可触发任何判定。
    /// </summary>
    public interface IJudgementSystem
    {
        /// <summary>判定事件回调</summary>
        event Action<JudgementEvent> OnJudgement;

        /// <summary>加载谱面数据</summary>
        /// <param name="chart">谱面数据（含 judge_points、notes、events）</param>
        void LoadChart(ChartData chart);

        /// <summary>处理输入事件</summary>
        /// <param name="input">输入事件</param>
        void ProcessInput(InputEvent input);

        /// <summary>每帧更新（用于自动 Miss 检测、判定点动态更新）</summary>
        /// <param name="currentTime">当前音频时间（秒）</param>
        void Update(double currentTime);

        /// <summary>获取判定统计</summary>
        JudgementStats GetStats();

        /// <summary>重置判定状态</summary>
        void Reset();

        /// <summary>获取当前活跃判定点（供 UI/演出系统查询）</summary>
        IReadOnlyList<JudgePointData> GetActiveJudgePoints();

        /// <summary>运行时更新判定点（由 Fx/Events 调用）</summary>
        void UpdateJudgePoint(string judgePointId, JudgePointData data);
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
    /// 谱面数据（完整 v0.2 结构）。
    /// </summary>
    public struct ChartData
    {
        public JudgePointData[] JudgePoints;
        public NoteData[] Notes;
        public ChartEvent[] Events;
        public double BPM;
        public double Offset;
    }

    /// <summary>
    /// 单个 Note 数据。
    /// </summary>
    public struct NoteData
    {
        /// <summary>Note 时间（秒，相对谱面开始，含 offset）</summary>
        public double Time;

        /// <summary>Note 判定时刻位置（归一化）</summary>
        public Vector2 Position;

        /// <summary>Note 深度</summary>
        public float Depth;

        /// <summary>Note 类型：0=Tap, 1=Hold, 2=Slide, 3=FxTrigger</summary>
        public int Type;

        /// <summary>持续时间（仅 Hold/Slide，秒）</summary>
        public double Duration;

        /// <summary>关联判定点 ID</summary>
        public string JudgePointId;

        /// <summary>Fx 触发器（可为空）</summary>
        public FxTrigger? Fx;
    }

    /// <summary>
    /// Fx 触发器定义。
    /// </summary>
    public struct FxTrigger
    {
        public string Module;
        public Dictionary<string, object> Params;
    }

    /// <summary>
    /// 谱面事件（动态变更：判定点移动、BPM 变化等）。
    /// </summary>
    public struct ChartEvent
    {
        public double Time;           // 触发时间（秒）
        public string Type;           // 事件类型
        public string Target;         // 目标 ID（judge_point id 等）
        public Dictionary<string, object> Params; // 事件参数
    }
}