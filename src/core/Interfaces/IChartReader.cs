// IChartReader.cs
// 谱面读取器接口 — 模块契约
// 实现方：src/chart/ChartReader.cs
// 调用方：gameplay（加载谱面）、ui（谱面选择界面）

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// 谱面读取器接口。
    /// 解析 BUGs Chart Format 文件，生成 ChartData。
    /// </summary>
    public interface IChartReader
    {
        /// <summary>从文件读取谱面</summary>
        /// <param name="path">谱面文件路径</param>
        /// <returns>解析后的谱面数据</returns>
        ChartData ReadFromFile(string path);

        /// <summary>从字符串读取谱面</summary>
        /// <param name="content">谱面文本内容</param>
        ChartData ReadFromString(string content);

        /// <summary>验证谱面格式</summary>
        /// <param name="path">谱面文件路径</param>
        /// <returns>验证结果（是否有效 + 错误列表）</returns>
        ChartValidationResult Validate(string path);

        /// <summary>获取谱面元数据（不完整解析，仅标题/作者/难度等）</summary>
        ChartMetadata GetMetadata(string path);
    }

    /// <summary>
    /// 谱面验证结果。
    /// </summary>
    public struct ChartValidationResult
    {
        public bool IsValid;
        public string[] Errors;
    }

    /// <summary>
    /// 谱面元数据。
    /// </summary>
    public struct ChartMetadata
    {
        public string Title;
        public string Artist;
        public string Charter;
        public string Difficulty;
        public int Level;
        public double BPM;
        public double Duration;
    }
}