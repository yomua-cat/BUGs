# 谱面模块

## 实现的接口

`IChartReader` — 见 `src/core/Interfaces/IChartReader.cs`

## 依赖的接口

无（底层模块）

## 职责

解析 BUGs Chart Format 文件，生成 `ChartData`。

## 格式规范

见 [FORMAT.md](FORMAT.md)（v0.1，已建立，最小 JSON 结构匹配接口）。

## 公开方法

| 方法 | 说明 |
|------|------|
| `ReadFromFile(path)` | 从文件读取谱面 |
| `ReadFromString(content)` | 从字符串读取 |
| `Validate(path)` | 验证格式 |
| `GetMetadata(path)` | 获取元数据 |

## 使用示例

```csharp
var reader = new ChartReader();
var result = reader.Validate("Charts/song.bugs");
if (result.IsValid) {
    var chart = reader.ReadFromFile("Charts/song.bugs");
    judgementSystem.LoadChart(chart);
}
```

## 设计原则

- 人类可读 + AI 可读
- 支持复杂谱面表演（变速、特效 Note 等）
- 可扩展（新 Note 类型不破坏兼容性）

## 文件结构

```
chart/
├── ChartReader.cs       # IChartReader 实现
├── FORMAT.md            # 谱面格式规范
└── README.md            # 本文件
```