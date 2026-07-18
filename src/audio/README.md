# 音频模块

## 实现的接口

`IAudioEngine` — 见 `src/core/Interfaces/IAudioEngine.cs`

## 依赖的接口

无（底层模块，不依赖其他游戏模块）

## 技术栈

- **miniaudio** (Public Domain) — 跨平台低延迟音频库
- 通过 Unity Native Plugin 集成

## 公开方法

| 方法 | 说明 |
|------|------|
| `Load(path)` | 加载音频文件，返回句柄 |
| `Unload(handle)` | 卸载音频 |
| `Play(handle, loop)` | 播放音频 |
| `Pause(handle)` | 暂停 |
| `Stop(handle)` | 停止 |
| `GetPosition(handle)` | 获取播放位置（音游时间基准） |
| `GetDuration(handle)` | 获取总时长 |
| `SetVolume(handle, volume)` | 设置音量 |
| `GetOutputLatency()` | 获取输出延迟 |
| `SetLatencyCompensation(seconds)` | 设置延迟补偿 |

## 使用示例

```csharp
var audio = new AudioEngine();
int music = audio.Load("Assets/Audio/song.ogg");
audio.SetLatencyCompensation(0.015); // 15ms 补偿
audio.Play(music, loop: false);

// 游戏循环中
double currentTime = audio.GetPosition(music);
judgementSystem.Update(currentTime);
```

## 性能目标

- 音频延迟 < 10ms（桌面端）
- 音频延迟 < 30ms（移动端）
- 零内存分配（热路径）

## 文件结构

```
audio/
├── AudioEngine.cs       # IAudioEngine 实现
├── miniaudio/           # miniaudio 源码
│   ├── miniaudio.h
│   └── UnityPlugin.c    # Unity 原生插件桥接
└── README.md            # 本文件
```