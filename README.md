# BUGs

> 一款跨平台节奏游戏。
> 目前在 **Unreal Engine 5.6.1** 核心原型验证阶段。

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Engine: UE5.6](https://img.shields.io/badge/Engine-Unreal%205.6.1-blue.svg)](https://www.unrealengine.com/)
[![Language: C++20](https://img.shields.io/badge/Language-C%2B%2B20-orange.svg)](https://isocpp.org/)

## 这是什么

BUGs 是一款节奏游戏——跟着音乐节拍操作，获得分数。

- **跨平台**：iOS / Android / macOS / Windows / Linux
- **输入自由**：键盘、触屏、手柄、MIDI、太鼓、无障碍设备
- **开源**：游戏代码 MIT 许可，引擎 UE EULA

> ⚠️ 项目处于 **UE5 核心原型验证阶段**（Quartz 时钟、判定系统、Fx 总线），暂不可游玩。

## 项目位置

本地工程路径：`/Users/yomua/Documents/code/BUGs`

远程仓库：`https://github.com/yomua-cat/BUGs`

> 2026-07-31 已完成迁移：旧 Unity 6 工程文件已删除，项目文件整理后迁移到当前路径，并与远程仓库重新绑定同步。

## 当前状态

正在进行 **4 周核心原型验证**（2026-07 启动）：

- [ ] **Week 1**：Quartz 音频时钟核心链路（样本级调度、<10ms 延迟）
- [ ] **Week 2**：判定系统 + Fx 总线（GAS Ability/Effect）
- [ ] **Week 3**：谱面运行时 + DataAsset（v0.2 JSON、多判定点）
- [ ] **Week 4**：UMG 玩法组件 + Shipping Build 验收（<80MB、60fps 稳）

详见 [路线图](docs/TODO.md)。

### 项目结构

```
/
├── .github/workflows/     ← GitHub Actions（opencode 评论触发 / PR Review / Issue Triage）
├── Config/                ← UE5 项目配置
├── Content/               ← UE5 内容资产（待填充）
├── Source/BUGs/           ← UE5 C++ 模块源码（已创建基础模块）
├── design/                ← 设计系统（本地-only，不上传 GitHub）
├── docs/                  ← 项目文档
│   ├── AGENT_MEMORY.md    ← 项目当前状态速查
│   ├── PROJECT.md         ← 项目概述
│   ├── TECH.md            ← 技术架构
│   ├── TODO.md            ← 路线图
│   ├── GAMEPLAY.md        ← 玩法框架
│   ├── FORMAT.md          ← 谱面格式 v0.2
│   ├── DECISIONS.md       ← 重要决策记录
│   └── CONTRIBUTING.md    ← 贡献指南
├── README.md
├── LICENSE
└── .gitignore
```

### 已知缺口

继续开发前需要确认/处理：

1. **旧 C# 接口迁移**：`docs/AGENT_MEMORY.md` 中记录了旧 `src/core/Interfaces/` 的 C# 接口，需迁移为 UE5 C++ 模块或废弃重建。
2. **美术资源**：`Content/` 为空，需按 `design/REQUIRED_ART.md` 准备资产。
3. **测试**：尚未建立测试目录与脚手架。
4. **Flopperam MCP**：需接入验证，才能无 UI 驱动 UE5 开发。

## 技术栈

| 项目 | 选型 | 说明 |
|------|------|------|
| 游戏引擎 | **Unreal Engine 5.6.1** | 源码开放、版税制、长期支持版本 |
| 核心语言 | **C++20** | 性能、确定性、引擎深度集成 |
| 游戏玩法脚本 | **Blueprint + GAS** | 可视化逻辑、Fx 总线 |
| 音频引擎 | **Audio Mixer + Quartz + MetaSound** | 样本级时钟、量化播放、参数化音频 |
| 谱面格式 | BUGs Chart Format v0.2 | JSON、AI 可读、可扩展 |
| 自动化驱动 | **opencode + Flopperam MCP** | 全流程无 UI 开发 |
| CI/CD | GitHub Actions + RunUAT | 多平台打包、自动化测试 |
| 许可证 | MIT (游戏代码) / UE EULA (引擎) | 代码完全开源 |

## 核心架构特点

- **模块化**：接口驱动，编译器强制契约一致性
- **音频优先**：Quartz 样本级时钟，原生量化播放，零 miniaudio 集成维护
- **判定点而非判定线**：XY + Z + Angle + Radius + Mode，支持自由轨道、多判定点、演出锚点分离
- **Fx 总线 = GAS**：每个 Fx = 一个 Ability/Effect，原生并发控制、标签查询、优先级
- **AI 友好开发**：opencode 任务分解 + Flopperam MCP（50+ 工具）驱动 Blueprint/C++/打包/测试全流程

## 相关文档

- [项目状态速查](docs/AGENT_MEMORY.md)
- [项目概述](docs/PROJECT.md)
- [技术架构](docs/TECH.md)
- [路线图](docs/TODO.md)
- [玩法框架](docs/GAMEPLAY.md)
- [视觉规格](docs/GAMEPLAY_VISUAL_SPEC.md)
- [谱面格式](docs/FORMAT.md)
- [重要决策](docs/DECISIONS.md)
- [贡献指南](CONTRIBUTING.md)

## 设计资产

设计系统见本地 `design/` 目录（被 `.gitignore` 忽略，不上传 GitHub）：

- `design/system/tokens.css` — 设计令牌
- `design/showcase/` — 组件展示
- `design/mockups/boot-sequence.html` — 已冻结的 Stage 概念原型
- `design/REQUIRED_ART.md` — 美术资源清单

## 贡献

核心原型验证完成后开放贡献。当前接受 Issue 讨论与设计反馈。

## 许可证

MIT © 2026 [油木然 (yomua)](https://github.com/yomua-cat)
