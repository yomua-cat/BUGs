// IUIManager.cs
// UI 管理器接口 — 模块契约
// 实现方：src/ui/UIManager.cs
// 调用方：gameplay（显示判定/分数）、平台启动入口

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// UI 管理器接口。
    /// 管理所有游戏界面的显示和切换。
    /// </summary>
    public interface IUIManager
    {
        /// <summary>显示指定界面</summary>
        /// <param name="screenId">界面标识</param>
        void ShowScreen(string screenId);

        /// <summary>隐藏当前界面</summary>
        void HideCurrentScreen();

        /// <summary>更新判定显示</summary>
        void ShowJudgement(Judgement result);

        /// <summary>更新连击显示</summary>
        void ShowCombo(int combo);

        /// <summary>更新分数显示</summary>
        void ShowScore(long score);

        /// <summary>更新进度条</summary>
        /// <param name="progress">进度（0.0 ~ 1.0）</param>
        void UpdateProgress(float progress);

        /// <summary>显示暂停菜单</summary>
        void ShowPauseMenu();

        /// <summary>显示结算界面</summary>
        void ShowResultScreen(ScoreStats stats);

        /// <summary>显示加载界面</summary>
        void ShowLoading(string message);

        /// <summary>隐藏加载界面</summary>
        void HideLoading();
    }
}