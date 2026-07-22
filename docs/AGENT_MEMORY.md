# AGENT_MEMORY — BUGs 项目当前状态

**生成日期**：2026-07-22  
**用途**：会话重启时加载此文件 + 关键 docs，防止上下文污染。  
**原则**：只保留必要事实、决策、当前任务。历史对话不要带入。

---

## 项目身份（核心，不可变）

**BUGs** 是一款**设计驱动**的跨平台节奏游戏（音游）。

### 是什么
- 玩法第一，设计驱动（不是故事/世界观驱动）
- 移动端优先，桌面端增强
- **输入无关**：任何有效输入都可触发判定（键盘/触屏/手柄/MIDI/太鼓/无障碍）
- 开源社区协作（GitHub 哲学）
- AI 友好仓库

### 不是什么
- 假操作系统、黑客模拟器、赛博朋克、程序员模拟器

### 核心原则（来自 1.txt）
1. **停止创建新页面**（当前阶段最重要纪律）
2. 所有 UI 必须从可复用组件库组装
3. 验证三大支柱：
   - Home Player（首页就是活的音乐表演）
   - Motion System（与音乐同步的统一动画语言）
   - Gameplay Core（可靠的节奏判定基础）
4. 极简、克制（每个元素必须证明价值）

---

## 当前阶段与进展

### 已完成
- **Phase 0**：项目初始化、文档框架、技术选型（Unity 6 + C# + miniaudio）
- **设计系统（Phase 1 原型阶段）**：
  - 完整设计令牌系统（`design/system/tokens.css`）
  - 组件库按关注点拆分（base / layout / display / overlay / navigation）
  - 纯 vanilla JS 行为模块（10 个 IIFE）
  - 交互式组件展示页（`design/showcase/index.html`）
  - 早期完整 Home Player 原型已汉化 + 修复（`design/mockups/boot-sequence.html`）

### 进行中
- 按照 1.txt 严格执行：**不再新增整页**，聚焦可复用系统。
- 应用 **ponytail**（马尾辫）思维：最懒、最少代码、YAGNI。
- 应用 **cavecrew**（穴居人）思维：调查时使用压缩输出。

### 待做（按 1.txt 优先级）
1. **Phase 2**：Motion System（统一动画 + BPM 同步）
2. **Phase 3**：Home Player 验证（真正播放音乐 + 空闲谱面渲染 + 切歌）
3. 继续从原型中提取缺失组件（Tooltip、Dropdown、Progress Bar、Loading Indicator 等）
4. Unity 侧实际实现（当前设计系统主要服务于 web 原型验证）

---

## 关键文件结构

```
/
├── docs/
│   ├── PROJECT.md
│   ├── DESIGN.md          ← 已更新，指向实际系统
│   ├── TODO.md            ← 已更新设计系统进度
│   ├── AGENT_MEMORY.md    ← 本文件（重启入口）
│   └── ...
├── design/
│   ├── README.md          ← 新增，解释 design 目录
│   ├── system/            ← 设计系统源码（核心）
│   │   ├── tokens.css
│   │   ├── *.css（按类别）
│   │   └── system.js
│   ├── showcase/
│   │   └── index.html     ← 可直接打开的组件画廊
│   └── mockups/
│       └── boot-sequence.html  ← 汉化后的完整原型
├── src/                   ← Unity 项目（目前为空壳）
└── 1.txt                  ← 下一阶段最高优先级指示（必须阅读）
```

---

## 活跃技能与思维模式

- **ponytail (full)**：每次响应都激活。优先删除、标准库、单行方案。标记 `ponytail:` 注释。
- **cavecrew**：调查时使用压缩格式（path:line — `symbol` — 简注）。
- **review**：需要审查时使用双轴（Standards + Spec）。
- **其他已加载**：frontend-design、design-an-interface、improve-codebase-architecture、task-management 等。

**重启时建议加载顺序**：
1. 本文件 (`docs/AGENT_MEMORY.md`)
2. `1.txt`
3. `docs/DESIGN.md` + `design/README.md`
4. `docs/PROJECT.md`
5. 按需加载具体组件源码

---

## 重要决策与约束

- 设计系统目前主要服务于 **web 原型验证**，未来需迁移/适配到 Unity。
- 所有新工作必须遵守“停止建新页面”纪律。
- 组件必须真正可复用（不要页面耦合的硬编码 ID）。
- 保持 AI 友好：清晰命名、少抽象、文档先行。

---

## 下一步行动建议（重启后）

1. 确认是否继续 Phase 1 提炼（提取更多组件到 system/）。
2. 或直接进入 Phase 2 Motion System。
3. 或用 ponytail 风格审计当前 design/system/，删除不必要的部分。
4. 验证 Home Player 原型是否真的“活”起来（需要真实音频？）。

**永远先问**：这个工作是否违反“停止创建新页面”原则？

---

## 污染防护

- 不要把完整历史对话塞进新会话。
- 不要假设之前聊过的具体实现细节，除非在本文件中记录。
- 需要细节时，显式读取对应文件，而不是靠记忆。
- 本文件应保持简洁（目标 < 200 行）。

---

**最后更新**：由 OpenAgent 于 2026-07-22 生成。  
准备重启会话时，只加载此文件 + 1.txt + 必要 docs。
