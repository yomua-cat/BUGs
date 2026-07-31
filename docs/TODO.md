# 路线图

> **重要更新（2026-07-28）**：引擎迁移至 **Unreal Engine 5.6.1**，全流程 opencode + Flopperam MCP 驱动。核心玩法视觉规格已定稿（`docs/GAMEPLAY_VISUAL_SPEC.md`），谱面格式 v0.2，判定点 + Fx 系统设计完成。当前重点：**UE5 核心原型验证** 与 **设计系统 Token 映射至 UMG/Slate**。

## 阶段 0：项目初始化 ✅

- [x] 技术选型
- [x] 仓库搭建
- [x] 文档框架
- [ ] **UE 5.6.1 源码获取与编译**（GitHub 5.6 分支）
- [ ] **Flopperam MCP 接入验证**（API Key + 插件安装 + bp_create 测试）
- [ ] opencode 自动化脚本库建立（ue_compile/ue_package/ue_test/ue_mcp/ue_generate）

## 阶段 1：核心原型（验证 UE5 架构可行性，4 周）

### Week 1：Quartz 音频时钟核心链路
- [ ] 创建 `QuartzRhythmCore` 插件（C++ 模块 + Build.cs + Target.cs）
- [ ] 实现 `RhythmAudioModule`：Quartz Clock 创建、BPM 设置、PlayQuantized 触发
- [ ] C++ 订阅 `FQuartzClockProxy::OnQuantizationEvent` → 样本级时间戳打印
- [ ] 真机验证：iOS/Android/Win64 延迟 <10ms、零音频线程分配

### Week 2：判定系统 + Fx 总线
- [ ] `JudgementSystem` 核心算法移植（时间窗、自动 Miss、统计事件）
- [ ] 输入抽象层适配 `Enhanced Input` → `IInputSource` 接口
- [ ] `FxBus` 基于 **Gameplay Ability System (GAS)** 实现（每个 Fx = 一个 Ability/Effect）
- [ ] 3 首内置谱面完整跑通（Tap/Hold/Slide/Fx、判定点移动、镜头/背景/粒子 Fx）

### Week 3：谱面运行时 + DataAsset
- [ ] `ChartDataAsset` (UDataAsset) + `FChartData` 结构体定义
- [ ] v0.2 JSON 反序列化 → `ChartDataAsset` 运行时加载
- [ ] `JudgePointManager`：多 Quartz Clock 并行、动态创建/销毁/移动/缩放
- [ ] 谱面编辑器雏形（UMG，支持 judge_points 轨迹、Fx 可视化编辑）

### Week 4：UMG 玩法组件 + 打包验收
- [ ] 设计系统 Token 映射：`design/system/tokens/` → `Slate Style` / `UMG Theme`
- [ ] 6 个玩法组件 UMG 重写：
  - `WBP_JudgementFlash` — 判定光效（仅外圆、无填充、局部播放）
  - `WBP_ComboCounter` — 连击计数（滑入/呼吸/碎裂/里程碑粒子）
  - `WBP_PauseOverlay` — 暂停菜单（ESC/双击/手柄 Start）
  - `WBP_StageRoot` — Stage 容器（坐标系、相机、渲染层级）
  - `WBP_NoteRenderer` — Note 渲染器（Tap/Hold/Slide/Fx 四类）
  - `WBP_ResultScreen` — 结算页（评级/分数/准确率/判定分布/最大连击）
- [ ] 4 个动效类：`Flash`/`Scale`/`Slide`/`Pulse` (UMG Animation / Slate Timeline)
- [ ] Shipping Build 验收：iOS/Android/Win64、包体 <80MB、Draw Calls ≤30、粒子 ≤500、Fx 耗时 <1ms/帧、零 GC

## 阶段 2：可玩版本（首个可完整游玩的 Build）

- [ ] 5 首内置谱面（覆盖全 Note 类型、判定点动态、复杂 Fx 编排）
- [ ] 完整判定视觉反馈（Perfect/Great/Good/Miss 四种光效 + Fx 附加效果）
- [ ] 连击系统完整（递增/维持/断连/里程碑粒子 + GAS 触发）
- [ ] 暂停/继续/重开/退出流程 + 焦点陷阱
- [ ] 设置界面（音量、延迟补偿、键位重映射、画质、语言）
- [ ] 性能达标（移动端 60fps 稳、内存 <500MB、启动 <3s）

## 阶段 3：社区就绪

- [ ] 谱面编辑器完善（Web 端优先，支持 judge_points 轨迹、Fx 可视化编辑、实时预览）
- [ ] 社区谱面导入/校验/预览（沙箱验证、版本兼容）
- [ ] 本地排行榜（分难度/模式、导出分享）
- [ ] 多语言支持（UE5 Localization Dashboard + String Table）

## 未来

- [ ] MIDI 输入支持（Enhanced Input + MIDI Device Plugin）
- [ ] 太鼓输入支持（自定义 Input Device）
- [ ] 无障碍设备支持（Input Remapping + Audio Cues + Visual Assists）
- [ ] 在线排行榜（EOS / 自建后端）
- [ ] 谱面市场（Mod.io / 自建）
- [ ] MOD 支持（WASM 沙箱、模块签名、插件商店、GAS Ability 扩展）