// IUIManager.cs
// UI 管理器接口 — 模块契约
// 实现方：src/ui/UIManager.cs
// 调用方：gameplay（显示判定/连击/结算）、平台启动入口

using System;

namespace BUGs.Core.Interfaces
{
    /// <summary>
    /// UI 管理器接口。
    /// 管理游戏过程中的最小 HUD：连击、判定光效触发、暂停、结算。
    /// </summary>
    public interface IUIManager
    {
        /// <summary>触发判定光效（由 JudgementSystem.OnJudgement 回调驱动）</summary>
        /// <param name="evt">判定事件（含结果、位置、Fx 信息）</param>
        void TriggerJudgementEffect(JudgementEvent evt);

        /// <summary>更新连击显示</summary>
        /// <param name="combo">当前连击数</param>
        /// <param name="isMilestone">是否为里程碑（50/100/200/500/1000...）</param>
        void UpdateCombo(int combo, bool isMilestone = false);

        /// <summary>连击断开动画（红色碎裂）</summary>
        void BreakCombo();

        /// <summary>显示暂停菜单</summary>
        void ShowPauseMenu();

        /// <summary>隐藏暂停菜单</summary>
        void HidePauseMenu();

        /// <summary>显示结算界面</summary>
        /// <param name="stats">完整统计（分数、准确率、判定分布、最大连击、评级）</param>
        void ShowResultScreen(ScoreStats stats);

        /// <summary>显示加载界面</summary>
        void ShowLoading(string message);

        /// <summary>隐藏加载界面</summary>
        void HideLoading();
    }
}