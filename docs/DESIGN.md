# 设计系统

BUGs 的 UI 采用**设计驱动**的现代桌面软件风格。

## 当前状态（2026-07）

设计系统已进入 **Phase 1** 实现阶段。

- 设计令牌已建立（见 `design/system/tokens.css`）
- 组件库已拆分（`design/system/`）
- 组件展示页已提供（`design/showcase/index.html`）
- 早期完整原型保留在 `design/mockups/boot-sequence.html`（已汉化）

**重要原则**（来自 1.txt）：
- 停止创建新页面。
- 未来所有界面必须从组件库组装。
- 每个视觉元素必须证明其存在价值。

## 设计原则

### 视觉语言
- 现代桌面软件风格
- 大留白、矩形布局
- 细分割线、微妙阴影
- 终端排版作为视觉语言（仅作为排版风格，非黑客美学）
- 最小信息密度

### 借鉴来源
| 来源          | 借鉴内容             |
|---------------|----------------------|
| Microsoft Fluent | 间距系统、极简主义   |
| 看门狗2       | 交互节奏、流畅过渡   |
| Steam         | 组件组织方式         |
| 专业桌面软件  | 可信度、克制         |

### 禁止元素
赛博朋克、黑客美学、六边形、HUD、故障效果、CRT、扫描线、霓虹、RGB、玻璃拟态、程序员梗、假代码、电影黑客特效。

**每个视觉元素必须证明其存在价值。**

## 设计令牌

所有令牌集中在 `design/system/tokens.css`：

- **颜色**：--bg-base / --bg-elevated / --accent / --warn / --error / 评级色等
- **字体**：--font-display (Chakra Petch)、--font-body (Inter)、--font-mono (Source Code Pro)
- **间距**：--space-1 到 --space-40 体系
- **圆角、阴影、z-index、动效时长与缓动**

## 组件库

组件按关注点拆分在 `design/system/` 下：

### 基础组件 (base.css)
- Button（含 primary / muted / mono 变体）
- Close Button
- Text Input / Select
- Range Slider
- Toggle Switch
- Tag / Highlight
- Field Label / Note / Value

### 布局组件 (layout.css)
- Page Panel + Overlay（滑入页面系统）
- Drawer（左右侧滑）
- Split Layout（主从分栏）
- Tab Sidebar
- Filter Group / Settings Row

### 展示组件 (display.css)
- Stat Card
- Rank Badge（S/A/B/C）
- Song Card（可展开）
- Feed Item / Timeline Item
- Leaderboard Row
- Artwork + Song Meta

### 覆盖层组件 (overlay.css)
- Toast Notification（带类型与回调）
- Global Search
- Modal / Event Card
- Result Screen（带爆炸动画 + 终端解码）

### 导航组件 (navigation.css)
- Top Nav / Bottom Nav
- Carousel Dots
- Brand（带呼吸光标，支持状态变体）

### 行为 (system.js)
纯 vanilla 模块（IIFE）：
- `BUGsToast`
- `BUGsPages`
- `BUGsTabs` / `BUGsToggle` / `BUGsSlider`
- `BUGsSearch`
- `BUGsDecode`（终端乱码解码动画）
- `BUGsDrawer` / `BUGsCarousel`
- 全局键盘处理（Ctrl+K、Escape 优先级关闭）

## 使用

```html
<link rel="stylesheet" href="design/system/tokens.css">
<link rel="stylesheet" href="design/system/base.css">
<!-- 按需引入其他 -->
<script src="design/system/system.js"></script>
```

详见 `design/showcase/index.html` 和 `design/README.md`。

## 后续计划

- Phase 2：Motion System（统一动画语言，支持 BPM 同步）
- 继续从原型中提取缺失组件（Tooltip、Dropdown、Progress Bar 等）
- 保持极简（应用 ponytail 原则）

## 相关文件
- `design/README.md` — design 文件夹说明
- `design/system/` — 实际组件源码
- `design/showcase/index.html` — 交互式组件画廊
- `design/mockups/boot-sequence.html` — 早期完整 Home Player 原型
