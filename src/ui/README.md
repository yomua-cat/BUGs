# UI 模块

## 实现的接口

`IUIManager` — 见 `src/core/Interfaces/IUIManager.cs`

## 依赖的接口

无（顶层模块，被其他模块调用）

## 职责

管理所有游戏界面的显示、切换、动画。

## 公开方法

| 方法 | 说明 |
|------|------|
| `ShowScreen(id)` | 显示指定界面 |
| `HideCurrentScreen()` | 隐藏当前界面 |
| `ShowJudgement(result)` | 显示判定 |
| `ShowCombo(combo)` | 显示连击 |
| `ShowScore(score)` | 显示分数 |
| `UpdateProgress(progress)` | 更新进度条 |
| `ShowPauseMenu()` | 暂停菜单 |
| `ShowResultScreen(stats)` | 结算界面 |
| `ShowLoading(message)` | 加载界面 |
| `HideLoading()` | 隐藏加载 |

## 界面列表

```
// TODO: 定义所有界面 ID
```

## 设计约束

- 遵循 BUGs 设计系统（见 docs/DESIGN.md）
- Microsoft Fluent 间距
- 大留白、矩形布局
- 终端排版
- 每个视觉元素必须证明其存在价值

## 文件结构

```
ui/
├── UIManager.cs         # IUIManager 实现
├── Screens/             # 各界面实现
│   ├── MainMenu.cs
│   ├── GameplayHUD.cs
│   ├── ResultScreen.cs
│   └── SettingsScreen.cs
└── README.md            # 本文件
```