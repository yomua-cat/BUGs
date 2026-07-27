// IFxBus.cs
// Fx 触发器总线接口 — 模块契约
// 实现方：src/gameplay/FxBus.cs
// 调用方：judgement system（判定时触发）、chart events（时间点触发）

using System;
using System.Collections.Generic;

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// Fx 触发事件数据。
    /// </summary>
    public struct FxEvent
    {
        /// <summary>模块标识符（如 "camera_push", "particle_burst"）</summary>
        public string ModuleId;

        /// <summary>模块参数（键值对，类型由模块定义）</summary>
        public Dictionary<string, object> Params;

        /// <summary>触发时刻（音频时钟，秒）</summary>
        public double Timestamp;

        /// <summary>触发位置（归一化坐标，可为空）</summary>
        public Vector2? Origin;

        /// <summary>触发深度（可为空）</summary>
        public float? Depth;

        /// <summary>关联判定点（可为空）</summary>
        public JudgePointData? JudgePoint;

        /// <summary>关联 Note（可为空）</summary>
        public NoteData? Note;
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
    /// Note 数据（运行时）。
    /// </summary>
    public struct NoteData
    {
        public double Time;        // 判定时刻（秒）
        public Vector2 Position;   // 判定时刻位置
        public float Depth;        // z
        public int Type;           // 0=Tap, 1=Hold, 2=Slide, 3=FxTrigger
        public double Duration;    // Hold/Slide 持续时间
        public string JudgePointId; // 关联判定点
        public FxTrigger? Fx;      // Fx 触发器（可为空）
    }

    /// <summary>
    /// Fx 触发器定义（谱面中的 fx 字段）。
    /// </summary>
    public struct FxTrigger
    {
        public string Module;
        public Dictionary<string, object> Params;
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
    /// Fx 模块接口。
    /// 每个内置模块实现此接口并注册到 FxModuleRegistry。
    /// </summary>
    public interface IFxModule
    {
        /// <summary>模块标识符（唯一，如 "camera_push"）</summary>
        string ModuleId { get; }

        /// <summary>参数 JSON Schema（用于编辑器校验、运行时校验）</summary>
        string ParamSchemaJson { get; }

        /// <summary>执行模块逻辑</summary>
        /// <param name="evt">触发事件（含参数、上下文）</param>
        void Execute(FxEvent evt);
    }

    /// <summary>
    /// Fx 触发器总线接口。
    /// 负责模块注册、参数校验、触发分发。
    /// </summary>
    public interface IFxBus
    {
        /// <summary>Fx 触发回调（供调试/日志/回放订阅）</summary>
        event Action<FxEvent> OnFxTriggered;

        /// <summary>注册内置模块（启动时调用）</summary>
        void RegisterModule(IFxModule module);

        /// <summary>触发 Fx（由判定系统、事件系统调用）</summary>
        /// <param name="moduleId">模块标识符</param>
        /// <param name="params">参数字典</param>
        /// <param name="context">执行上下文（时间、位置、关联对象）</param>
        void Trigger(string moduleId, Dictionary<string, object> @params, FxContext context = default);

        /// <summary>获取已注册模块列表（供编辑器/调试）</summary>
        IReadOnlyList<string> GetRegisteredModules();
    }

    /// <summary>
    /// Fx 执行上下文。
    /// </summary>
    public struct FxContext
    {
        public double Timestamp;           // 音频时钟
        public Vector2? Origin;            // 触发位置
        public float? Depth;               // 触发深度
        public JudgePointData? JudgePoint; // 关联判定点
        public NoteData? Note;             // 关联 Note
    }
}