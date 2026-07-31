# BUGs 项目现状记录

**更新日期**：2026-07-31

> 这个文件记录了项目现在做到哪了。如果你隔了一段时间回来，先看这个快速回忆。

---

## 这个游戏是做什么的

BUGs 是一款**节奏游戏（音游）**——跟着音乐节拍操作，获得分数。

一些特点：
- 手机和电脑都能玩
- 键盘、触屏、游戏手柄都能用（以后还能接 MIDI 乐器、太鼓）
- 玩法核心：判定点（XY + Z + Angle + Radius + Mode），自由轨道
- 做这个游戏的所有代码都公开，谁都可以参与

---

## 核心技术栈

| 项目 | 选型 | 说明 |
|------|------|------|
| 游戏引擎 | **Unreal Engine 5.6.1** | 2026-07-28 从 Unity 6 迁移过来 |
| 核心语言 | **C++20** | 性能、确定性、引擎深度集成 |
| 玩法脚本 | **Blueprint + GAS** | 可视化逻辑、Fx 总线 |
| 音频引擎 | **Audio Mixer + Quartz + MetaSound** | 样本级时钟、量化播放、参数化音频 |
| 谱面格式 | BUGs Chart Format v0.2 | JSON、AI 可读、可扩展 |
| 自动化驱动 | **opencode + Flopperam MCP** | 全流程无 UI 开发 |
| CI/CD | GitHub Actions + RunUAT | 多平台打包、自动化测试 |
| 许可证 | MIT (游戏代码) / UE EULA (引擎) | 代码完全开源 |

---

## 已经做了什么

### 项目搭好了
- 选好了游戏引擎：Unreal Engine 5.6.1
- 本地工程路径：`/Users/yomua/Documents/code/BUGs`
- 远程仓库：`https://github.com/yomua-cat/BUGs`
- 搭好了 GitHub 仓库并重新绑定
- 配置了 opencode GitHub Actions（评论触发、PR Review、Issue Triage）

### 架构与接口
- 写好了 7 个模块接口约定：`IAudioEngine`、`IChartReader`、`IFxBus`、`IInputSource`、`IJudgementSystem`、`IScoreManager`、`IUIManager`
- 定义了谱面格式 v0.2：`judge_points`、`notes`、`events`、`fx`、自由坐标系、多判定点
- 写了最小计分实现 `ScoreManager.cs`
- 写了最小谱面读取实现 `ChartReader.cs`（**待适配 v0.2**：目前只解析了旧字段）

### 设计系统（Web 原型阶段）
- 设计令牌已最终确定：`design/system/tokens.css` 及 `tokens/` 拆分
- 组件展示页和令牌可视化页已存在
- Stage 启动序列概念原型已冻结：`design/mockups/boot-sequence.html`
- 美术资源清单已确定：`design/REQUIRED_ART.md`

### 文档
- 更新了项目概述、技术架构、路线图、玩法框架、视觉规格、决策记录等
- 更新了 `README.md` 说明当前项目位置、结构、状态与已知缺口
- 详细记录了引擎迁移理由：见 `docs/DECISIONS.md` #009

---

## 正在做什么

当前主要工作：

1. **UE5 核心原型验证（4 周）**
   - Week 1：Quartz 音频时钟核心链路
   - Week 2：判定系统 + Fx 总线（GAS Ability/Effect）
   - Week 3：谱面运行时 + DataAsset
   - Week 4：UMG 玩法组件 + Shipping Build 验收
2. **填充设计系统组件库**：`design/system/components/`（6 个玩法组件优先）
3. **填充动效模板**：`design/system/motion/`（4 个基础动效）

---

## 还没做的（按优先级）

1. 把旧 C# 接口/实现迁移为 UE5 C++ 模块（新位置 Source/ 已有 UE5 工程）
2. 适配 `ChartReader.cs` 到谱面格式 v0.2（或直接用 UE5 C++ 重写）
3. 完成设计系统 `components/` 和 `motion/`
4. 建立动效系统（让界面动起来，跟音乐节拍同步）
5. 建立测试目录与测试脚手架
6. 准备美术资源
7. Flopperam MCP 接入验证
8. 把设计系统 Token 映射到 UE5 UMG/Slate

---

## 几个重要的提醒

- **引擎已经迁移到 UE5.6.1**：不要再按 Unity 思路新增代码
- **`Source/` 目录已有 UE5 工程**，旧 `src/` 的 C# 代码需迁移为 C++ 或废弃重建
- **设计系统目前只是 Web 原型**（浏览器里看），最终要映射到 UE5 UMG/Slate
- **别再新建整页了**——所有新界面必须从组件库里拼出来
- **美术资源**：开始 UE5 实现前，先确认 `REQUIRED_ART.md` 里的资源到位
- **`design/`、`docs/design/`、`docs/GAMEPLAY_VISUAL_SPEC.md` 只在本地**：被 `.gitignore` 忽略，不上传 GitHub，避免版权问题
- **代码风格**：追求简洁、能删就删、能用现成的就不自己写

---

## 新会话同步要点 / 待决策清单

> 开新会话时先看本节，按顺序确认下一件要做的事。

当前项目已迁移到 `/Users/yomua/Documents/code/BUGs`，远程仓库 `https://github.com/yomua-cat/BUGs`，UE5 工程已创建，GitHub Actions 已恢复。可以继续开发，但以下 4 件事需要按优先级决策：

1. **Flopperam MCP 接入验证**
   - 阻塞后续无 UI 开发。
   - 动作：安装/验证 ue-mcp，测试 `bp_create` 或 `editor(action="get_status")`。

2. **旧 C# 接口迁移到 UE5 C++**
   - 旧 `src/core/Interfaces/` 的 7 个 C# 接口已删除，但文档里仍有记录。
   - 动作：逐步重写为 `Source/BUGs/` 下的 C++ 接口/模块，或直接废弃重建。

3. **美术资源准备**
   - `Content/` 为空，`design/REQUIRED_ART.md` 已列出必须资源。
   - 动作：确认资源来源，导入 UE5 Content。

4. **测试脚手架**
   - 尚未建立测试目录与 CI 测试流程。
   - 动作：搭建单元测试/集成测试目录，配置 Gauntlet 或虚幻自动化测试。

**默认推荐顺序**：1 → 2 → 4 → 3（先让自动化工具链跑通，再写核心代码）。

---

## 项目文件结构

```
/
├── .github/               ← GitHub Actions 工作流
│   └── workflows/
│       ├── opencode.yml
│       ├── opencode-review.yml
│       └── opencode-triage.yml
├── assets/                ← 游戏美术/音频资源（目前为空）
├── design/                ← 界面设计源码（本地-only，不上传）
│   ├── README.md
│   ├── REQUIRED_ART.md
│   ├── mockups/
│   │   └── boot-sequence.html       ← 已冻结的 Stage 原型
│   ├── showcase/
│   │   ├── index.html               ← 组件展示页
│   │   └── tokens.html              ← 令牌可视化
│   ├── stage-expected-effects.md
│   └── system/
│       ├── tokens.css               ← 设计令牌入口
│       ├── tokens/                  ← 颜色、字体、间距、动效等
│       ├── components/              ← 玩法/通用组件模板（空，待填充）
│       └── motion/                  ← 动效模板（空，待填充）
├── docs/                  ← 文档
│   ├── PROJECT.md
│   ├── GAMEPLAY.md
│   ├── GAMEPLAY_VISUAL_SPEC.md      ← 视觉规格（本地-only）
│   ├── TECH.md
│   ├── TODO.md
│   ├── FORMAT.md           ← 谱面格式 v0.2
│   ├── DECISIONS.md
│   ├── CONTRIBUTING.md
│   ├── AGENT_MEMORY.md              ← 就是本文件
│   └── design/                      ← 设计文档（本地-only）
│       ├── DESIGN.md
│       ├── DESIGN_TOKEN_SPEC.md
│       └── BOOT_SEQUENCE_CONCEPT.md
├── Source/                ← UE5 C++ 源代码（已创建）
│   └── BUGs/                        ← 模块源码（待填充）
├── Content/               ← UE5 内容资产（目前为空）
├── Config/                ← UE5 项目配置
├── README.md
├── LICENSE
└── .gitignore
```

---

## 工作习惯

- **追求简洁**：能删就删，能用现成的就不自己写
- **用 gh 命令操作 GitHub**：不手动提交
- **每个改动只做一件事**
- **接口先行**：新增模块先写接口，再写实现
- **文档同步**：接口或架构变更必须同步更新 `AGENT_MEMORY.md` 和相关文档

---

**最后更新**：2026-07-31（整理目录结构、识别 UE5 迁移缺口、标记待确认问题）
