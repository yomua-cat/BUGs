# 玩法模块

## 实现的接口

- `IJudgementSystem` — 见 `src/core/Interfaces/IJudgementSystem.cs`
- `IScoreManager` — 见 `src/core/Interfaces/IScoreManager.cs`

## 依赖的接口

- `IAudioEngine` — 获取音频时间
- `IInputSource` — 接收输入事件
- `IChartReader` — 加载谱面
- `IUIManager` — 显示判定/分数

## 职责

核心游戏逻辑：判定、计分、连击。

## 判定系统

```
输入事件 → 匹配最近的 Note → 计算时间偏差 → 判定等级
```

### 判定等级

| 等级 | 时间窗口 | 分数倍率 |
|------|----------|----------|
| Perfect | ±25ms | 1.0 |
| Great | ±50ms | 0.8 |
| Good | ±100ms | 0.5 |
| Miss | >100ms | 0.0 |

> ⚠️ 时间窗口待实际测试后调整。

## 计分系统

```
// TODO: 定义计分公式
```

## 使用示例

```csharp
var judgement = new JudgementSystem(audio, ui);
judgement.LoadChart(chart);
judgement.OnJudgement += (e) => scoreManager.ProcessJudgement(e);

input.OnInput += (e) => judgement.ProcessInput(e);

// 游戏循环
void Update() {
    judgement.Update(audio.GetPosition(musicHandle));
}
```

## 文件结构

```
gameplay/
├── JudgementSystem.cs   # IJudgementSystem 实现
├── ScoreManager.cs      # IScoreManager 实现
└── README.md            # 本文件
```