# 输入模块

## 实现的接口

`IInputSource` — 见 `src/core/Interfaces/IInputSource.cs`

## 依赖的接口

无（底层模块）

## 职责

将键盘、触屏、手柄等输入设备抽象为统一的 `InputEvent` 流。

## 公开成员

| 成员 | 说明 |
|------|------|
| `OnInput` | 输入事件回调 |
| `Enable()` | 启用输入 |
| `Disable()` | 禁用输入 |
| `LoadKeyConfig(path)` | 加载键位配置 |
| `GetKeyMapping()` | 获取当前键位映射 |

## 使用示例

```csharp
var input = new InputSource();
input.LoadKeyConfig("Configs/keybindings.json");
input.OnInput += (e) => judgementSystem.ProcessInput(e);
input.Enable();
```

## 支持的设备

- 键盘（默认）
- 触屏
- 手柄
- MIDI（未来）
- 自定义设备（未来）

## 键位配置格式

```json
// TODO: 定义键位配置文件格式
```

## 文件结构

```
input/
├── InputSource.cs       # IInputSource 实现
├── KeyConfig.cs         # 键位配置解析
└── README.md            # 本文件
```