// IAudioEngine.cs
// 音频引擎接口 — 模块契约
// 实现方：src/audio/AudioEngine.cs
// 调用方：gameplay（判定需要音频时钟）、ui（音频可视化）

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// 音频引擎接口。
    /// 封装 miniaudio，提供低延迟音频播放和时钟查询。
    /// </summary>
    public interface IAudioEngine
    {
        /// <summary>加载音频文件</summary>
        /// <param name="path">音频文件路径</param>
        /// <returns>音频句柄，用于后续操作</returns>
        int Load(string path);

        /// <summary>卸载音频</summary>
        void Unload(int handle);

        /// <summary>播放音频</summary>
        /// <param name="handle">音频句柄</param>
        /// <param name="loop">是否循环</param>
        void Play(int handle, bool loop = false);

        /// <summary>暂停音频</summary>
        void Pause(int handle);

        /// <summary>停止音频</summary>
        void Stop(int handle);

        /// <summary>获取当前播放位置（秒）</summary>
        /// <remarks>
        /// 这是音游判定的时间基准。
        /// 返回硬件时钟同步的精确位置，包含延迟补偿。
        /// </remarks>
        double GetPosition(int handle);

        /// <summary>获取音频总时长（秒）</summary>
        double GetDuration(int handle);

        /// <summary>设置音量（0.0 ~ 1.0）</summary>
        void SetVolume(int handle, float volume);

        /// <summary>获取输出延迟（秒）</summary>
        /// <remarks>用于延迟校准。不同平台/设备返回值不同。</remarks>
        double GetOutputLatency();

        /// <summary>设置全局延迟补偿（秒）</summary>
        void SetLatencyCompensation(double seconds);
    }
}