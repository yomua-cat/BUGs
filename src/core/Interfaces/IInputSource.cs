// IInputSource.cs
// 输入源接口 — 模块契约
// 实现方：src/input/InputSource.cs
// 调用方：gameplay（判定系统接收输入事件）

using System;

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// 输入事件数据。
    /// 与具体设备无关的抽象输入表示。
    /// </summary>
    public struct InputEvent
    {
        /// <summary>输入类型</summary>
        public InputType Type;

        /// <summary>输入通道/键位标识</summary>
        /// <remarks>键盘为 KeyCode，触屏为触摸点 ID，手柄为按钮索引</remarks>
        public int Channel;

        /// <summary>输入值（0.0 ~ 1.0，按键为 0/1，触屏为压力）</summary>
        public float Value;

        /// <summary>事件时间戳（秒，与音频时钟同步）</summary>
        public double Timestamp;
    }

    /// <summary>
    /// 输入类型枚举。
    /// </summary>
    public enum InputType
    {
        KeyDown,
        KeyUp,
        TouchDown,
        TouchUp,
        TouchMove,
        GamepadButton,
        GamepadAxis
    }

    /// <summary>
    /// 输入源接口。
    /// 将各种输入设备抽象为统一的 InputEvent 流。
    /// </summary>
    public interface IInputSource
    {
        /// <summary>输入事件回调</summary>
        event Action<InputEvent> OnInput;

        /// <summary>启用输入源</summary>
        void Enable();

        /// <summary>禁用输入源</summary>
        void Disable();

        /// <summary>加载键位配置</summary>
        /// <param name="configPath">键位配置文件路径</param>
        void LoadKeyConfig(string configPath);

        /// <summary>获取当前键位映射</summary>
        /// <returns>通道 → 动作名称的映射</returns>
        System.Collections.Generic.Dictionary<int, string> GetKeyMapping();
    }
}