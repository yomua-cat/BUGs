# 路线图

> **重要更新（2026-07-27）**：核心玩法视觉规格已定稿（`docs/GAMEPLAY_VISUAL_SPEC.md`），谱面格式升级至 v0.2，判定点 + Fx 系统设计完成。当前重点：**设计系统组件库落地**（`design/system/components/`、`motion/`）与 **Unity 核心原型验证**。

## 阶段 0：项目初始化 ✅

- [x] 技术选型
- [x] 仓库搭建
- [x] 文档框架
- [ ] Unity 项目创建
- [ ] miniaudio 集成验证（延迟基线测量）

## 阶段 1：核心原型（验证架构可行性）

### 音频与输入
- [ ] 音频引擎（miniaudio P/Invoke 封装 + `IAudioEngine` 实现）
- [ ] 输入抽象层（键盘/触屏/手柄 → `IInputSource` 实现）
- [ ] 延迟校准工具（真机测量 + 补偿存储）

### 判定与谱面
- [ ] 判定系统核心（`IJudgementSystem`：时间窗、自动 Miss、统计事件）
- [ ] **判定点系统**（动态位置/角度/半径/模式查询）
- [ ] 谱面格式 v0.2 解析器（`judge_points`、`events`、`fx`、`x,y,z` 坐标）
- [ ] Fx 触发器总线（`IFxBus` + 内置模块注册表 + Schema 校验）

### 视觉与 UI（设计系统落地）
- [x] 设计系统脚手架 — web 原型阶段完成
- [ ] **组件库扩充**（`design/system/components/`）：
  - `judgement-flash.html` — 判定光效（Perfect/Great/Good/Miss 四种）
  - `combo-counter.html` — 连击计数（滑入/呼吸/碎裂/里程碑）
  - `pause-overlay.html` — 暂停菜单（ESC/双击/手柄 Start）
  - `stage-root.html` — Stage 容器（坐标系、相机、渲染层级）
  - `note-renderer.html` — Note 渲染器（Tap/Hold/Slide/Fx 四类）
  - `result.html` — 结算页（评级/分数/准确率/判定分布/最大连击）
- [ ] **动效库扩充**（`design/system/motion/`）：
  - `flash.css` — 判定光效关键帧
  - `scale.css` — 连击/按钮缩放
  - `slide.css` — 面板/抽屉滑入滑出
  - `pulse.css` — 呼吸/待机动画
- [ ] 令牌完善（`design/system/tokens/` 已有基础，按需补充）
- [ ] 基础 UI 框架（Unity 侧：`IUIManager` 实现 + 预制体映射设计系统）

## 阶段 2：可玩版本（首个可完整游玩的 Build）

- [ ] 3 首内置谱面（覆盖 Tap/Hold/Slide/Fx、判定点移动、镜头/背景 Fx）
- [ ] 完整判定视觉反馈（仅外圆、无填充、局部播放、Fx 附加效果）
- [ ] 连击系统完整（递增/维持/断连/里程碑粒子）
- [ ] 暂停/继续/重开/退出流程
- [ ] 结算页完整数据展示
- [ ] 设置界面（音量、延迟补偿、键位、画质）
- [ ] 性能达标（Draw Calls ≤30、粒子 ≤500、Fx 耗时 <1ms/帧、零 GC）

## 阶段 3：社区就绪

- [ ] 谱面编辑器（Web 端优先，支持 judge_points 轨迹、Fx 可视化编辑）
- [ ] 社区谱面导入/校验/预览
- [ ] 本地排行榜（分难度/模式）
- [ ] 多语言支持（Unity 侧）

## 未来

- [ ] MIDI 输入支持
- [ ] 太鼓输入支持
- [ ] 无障碍设备支持
- [ ] 在线排行榜
- [ ] 谱面市场
- [ ] MOD 支持（WASM 沙箱、模块签名、插件商店）