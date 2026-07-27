# 游戏玩法

> ⚠️ 详细规格待设计。以下为框架。

## 核心概念

### 判定系统

输入和判定**完全解耦**。任何有效输入可触发任何判定。

```
输入事件 → 输入抽象层 → 判定系统 → 结果（Perfect/Great/Good/Miss）
```

### 判定点

判定不再是固定判定线，而是**判定点**：

- **位置**：归一化坐标系 `[-1, 1] × [-1, 1]` 中的 `(x, y)`
- **深度**：`z ∈ [0, 1]`（0=最近/前景，1=最远/背景），用于渲染排序与景深
- **角度**：`angle ∈ [0, 360)` 度，判定区朝向（影响视觉反馈方向）
- **半径**：`radius` 判定触发范围（默认 0.12，可配置）
- **模式**：`mode ∈ {judge, perform, both}`
  - `judge`：仅参与判定，不渲染
  - `perform`：仅作为演出锚点（镜头/特效跟随），不判定
  - `both`：同时参与判定与演出

判定点可在谱面中**动态移动/旋转/缩放**（通过 Fx 控制），允许作者：
- 让判定点跟随轨道轨迹移动
- 多判定点协同（如双轨、分屏）
- 纯演出用判定点（不产生判定，仅驱动镜头/特效）

### 时间窗口

实现见 `src/gameplay/README.md` 与 `src/core/Interfaces/IJudgementSystem.cs`。

| 等级 | 时间窗口 | 分数 |
|------|----------|------|
| Perfect | ±25ms | 1000 |
| Great | ±50ms | 800 |
| Good | ±100ms | 500 |
| Miss | >100ms | 0（断连击） |

> ⚠️ 时间窗口与权重为初值，待实际测试后调整。

### 计分系统

准确率 = 命中数 / 总数。评级：S ≥95% / A ≥90% / B ≥80% / C ≥70% / D 其余。
实现见 `src/gameplay/ScoreManager.cs`。

## 输入模型

### 支持设备

- 键盘（默认）
- 触屏
- 手柄
- MIDI（未来）
- 太鼓（未来）
- 无障碍设备（未来）

### 键位布局

允许任意自定义键位布局。默认布局待设计。

## 谱面格式

使用自定义 BUGs Chart Format **v0.2**。详见 `src/chart/FORMAT.md`。

核心变更：
- Note 坐标从 `ch` (lane index) 改为 `x, y` (归一化坐标)
- 新增 `z` (深度)、`angle` (朝向)
- 新增 `judge_point` 字段引用判定点配置
- 新增 `fx` 字段定义触发器（模块名 + 参数）

## Fx 触发器系统

**设计原则**：谱面只声明"触发什么"，游戏读取并调用对应模块，**不支持运行时自定义代码**（MOD 阶段再开放）。

### Fx 结构

```json
{
  "fx": {
    "module": "camera_push",
    "params": { "zoom": 1.2, "duration": 0.5 }
  }
}
```

| 字段 | 类型 | 说明 |
|------|------|------|
| `module` | string | 内置模块标识符（见下表） |
| `params` | object | 模块参数，键值对，类型由模块定义 |

### 内置模块表（最小集）

| module | 用途 | params 示例 |
|--------|------|-------------|
| `camera_push` | 镜头推拉/位移 | `{ "zoom": 1.2, "duration": 0.5, "ease": "outQuad" }` |
| `camera_shake` | 镜头震动 | `{ "intensity": 0.15, "duration": 0.3, "frequency": 25 }` |
| `bg_shift` | 背景色相/亮度偏移 | `{ "hue": 45, "saturation": 1.2, "transition": 0.7 }` |
| `judge_point_move` | 判定点位移/旋转 | `{ "x": 0.3, "y": -0.2, "angle": 45, "duration": 0.4 }` |
| `judge_point_scale` | 判定点缩放 | `{ "scale": 1.5, "duration": 0.3 }` |
| `particle_burst` | 粒子爆发 | `{ "preset": "spark", "count": 30, "color": "#5ec97a" }` |
| `time_scale` | 全局时间缩放 | `{ "scale": 0.5, "duration": 1.0, "ease": "inOutQuad" }` |
| `screen_flash` | 全屏单帧闪光 | `{ "color": "#ffffff", "intensity": 0.3, "frames": 1 }` |

> **约束**：默认判定光效**禁止**使用全屏效果（`screen_flash` 等）。连击 Miss 时全屏闪光会遮挡视野。附加判定效果（如 Perfect 触发粒子、镜头推拉）必须通过 Fx 显式声明，由模块系统统一调度。

## 判定视觉规格

### 核心原则

1. **仅外圆，无填充** —— 所有判定光效为描边圆环/弧线，透明内部
2. **默认判定无全屏效果** —— Perfect/Great/Good/Miss 基础反馈仅在判定点半径范围内
3. **附加效果走 Fx** —— 任何超出判定点范围的视觉反馈（粒子、镜头、背景变色、屏幕闪光）必须在谱面中显式声明 `fx`

### 判定光效规格

| 判定 | 视觉描述 | 颜色 Token | 持续 | 备注 |
|------|----------|------------|------|------|
| **Perfect** | 白色核心闪烁 1 帧 → 绿色外圆扩散 → 细粒子向外飞散 | `--accent` / `--accent-glow` | 300ms | 核心闪烁仅 1 帧 |
| **Great** | 绿色外圆扩散（无白核心） | `--accent` | 250ms | |
| **Good** | 蓝色外圆收缩淡出 | `--info` | 200ms | |
| **Miss** | 红色冲击波向内塌陷 → 单帧红色边框闪烁 | `--error` / `--error-glow` | 400ms | 边框闪烁而非全屏填充 |

> 所有光效在判定点局部坐标系下播放，随判定点移动/旋转。

## HUD 极简规格

| 组件 | 视觉 | Token | 触发 |
|------|------|-------|------|
| **连击计数** | 屏幕右侧竖排，数字逐级放大→缩小，断连击时红色碎裂 | `--text-combo: 64px`, `--font-display`, `--grade-*` | 每次判定更新 |
| **暂停菜单** | ESC/双击/手柄 Start → 半透明遮罩 + 居中面板（继续/重开/设置/退出） | `overlay.css`, `motion/slide.css` | 暂停时 `Time.timeScale = 0` |

> **无分数、无准确率、无进度条** —— 全部进结算页。

## 游戏模式

```
自由模式 / 挑战模式 / 练习模式 —— 详细规格待设计
```

---

## 关联文档

- 谱面格式：`src/chart/FORMAT.md`
- 视觉规格：`docs/GAMEPLAY_VISUAL_SPEC.md`
- 判定接口：`src/core/Interfaces/IJudgementSystem.cs`
- Fx 总线接口：`src/core/Interfaces/IFxBus.cs`（待创建）
- 设计系统：`design/system/`