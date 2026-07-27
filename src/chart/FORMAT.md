# BUGs Chart Format (v0.2)

Minimal, human + AI readable format for rhythm game charts.
Extensible for complex performances (free tracks, judge points, Fx triggers).

## File extension

`.bugs` (recommended) or `.bugs.json`

## Structure

Top level JSON object:

```json
{
  "meta": {
    "title": "Song Title",
    "artist": "Artist Name",
    "charter": "Charter Name",
    "difficulty": "Normal",
    "level": 5,
    "bpm": 120.0,
    "offset": 0.0,
    "duration": 123.45
  },
  "judge_points": [
    {
      "id": "main",
      "x": 0.0,
      "y": 0.0,
      "z": 0.5,
      "angle": 0,
      "radius": 0.12,
      "mode": "both"
    }
  ],
  "notes": [
    { "t": 1.234, "x": 0.0, "y": -0.8, "z": 0.5, "type": 0, "dur": 0.0, "judge_point": "main", "fx": null },
    { "t": 2.500, "x": 0.3, "y": -0.6, "z": 0.5, "type": 1, "dur": 0.8, "judge_point": "main", "fx": null },
    { "t": 3.000, "x": -0.2, "y": -0.4, "z": 0.5, "type": 2, "dur": 0.0, "judge_point": "main", "fx": { "module": "camera_push", "params": { "zoom": 1.2, "duration": 0.5 } } }
  ],
  "events": [
    { "t": 30.0, "type": "judge_point_move", "target": "main", "params": { "x": 0.5, "y": 0.0, "duration": 2.0 } }
  ]
}
```

### Fields

**meta** (required for editor / selection):
- title, artist, charter, difficulty, level: strings/ints for display
- bpm: base BPM (double)
- offset: audio start offset in seconds (double, can be negative)
- duration: total length (optional, for UI)

**judge_points** (array, optional, default: single point at origin):
- id: string, unique identifier, referenced by notes/events
- x, y: normalized position `[-1, 1]`, default `0, 0`
- z: depth `[0, 1]`, 0=foreground, 1=background, default `0.5`
- angle: rotation in degrees `[0, 360)`, default `0`
- radius: judge trigger radius, default `0.12`
- mode: `"judge" | "perform" | "both"`, default `"both"`

**notes** (array of note objects):
- t: time in seconds from chart start (after offset), double, required
- x, y: normalized position `[-1, 1]` at judgement moment, required
- z: depth `[0, 1]`, default `0.5` (render order, not judgement)
- type: note type (int):
  - 0 = Tap
  - 1 = Hold
  - 2 = Slide / Drag (straight line, future: curve via control points)
  - 3 = Fx Trigger (no judgement, only triggers fx module)
- dur: duration in seconds (only meaningful for Hold/Slide; 0 for Tap/Fx)
- judge_point: string, references `judge_points.id` (optional, defaults to first)
- fx: null | object (Fx trigger, see below)

**events** (array, optional, for dynamic changes during playback):
- t: trigger time (seconds)
- type: event type string
- target: target id (judge_point id, etc.)
- params: object, event-specific parameters

### Fx Object

```json
{
  "fx": {
    "module": "camera_push",
    "params": { "zoom": 1.2, "duration": 0.5 }
  }
}
```

| Field | Type | Description |
|-------|------|-------------|
| `module` | string | Built-in module identifier (see module table) |
| `params` | object | Module parameters, key-value, types defined by module |

**Built-in Module Table** (minimal set):

| module | Purpose | params example |
|--------|---------|----------------|
| `camera_push` | Camera dolly/zoom | `{ "zoom": 1.2, "duration": 0.5, "ease": "outQuad" }` |
| `camera_shake` | Camera shake | `{ "intensity": 0.15, "duration": 0.3, "frequency": 25 }` |
| `bg_shift` | Background hue/saturation shift | `{ "hue": 45, "saturation": 1.2, "transition": 0.7 }` |
| `judge_point_move` | Judge point position/rotation | `{ "x": 0.3, "y": -0.2, "angle": 45, "duration": 0.4 }` |
| `judge_point_scale` | Judge point radius scale | `{ "scale": 1.5, "duration": 0.3 }` |
| `particle_burst` | Particle burst | `{ "preset": "spark", "count": 30, "color": "#5ec97a" }` |
| `time_scale` | Global time scale | `{ "scale": 0.5, "duration": 1.0, "ease": "inOutQuad" }` |
| `screen_flash` | Single-frame screen flash | `{ "color": "#ffffff", "intensity": 0.3, "frames": 1 }` |

> **Constraint**: Default judgement visuals **must not** use full-screen effects. `screen_flash` and similar modules are only available via explicit Fx declaration in chart. This prevents accidental screen obstruction during Miss streaks.

### Rules

- Times are absolute (post-offset).
- Notes should be sorted by `t` ascending (parser may sort defensively).
- Multiple notes at same time on different positions supported.
- `judge_points` can be moved/rotated/scaled at runtime via `events` or Fx.
- `fx` on a note triggers **at judgement moment** (when note reaches judge point).
- `events` trigger at absolute time `t` during playback.
- Keep minimal: no visual data in chart (that's UI/renderer layer).
- Parser must be forward-compatible: ignore unknown fields, unknown modules log warning but don't fail.

### Validation

See `IChartReader.Validate` and `ChartValidationResult`.

### Example (minimal playable)

```json
{
  "meta": { "title": "Test", "artist": "Dev", "bpm": 128, "offset": -0.05 },
  "judge_points": [
    { "id": "main", "x": 0.0, "y": 0.0, "z": 0.5, "angle": 0, "radius": 0.12, "mode": "both" }
  ],
  "notes": [
    {"t": 0.5, "x": 0.0, "y": -0.8, "z": 0.5, "type": 0, "dur": 0, "judge_point": "main", "fx": null},
    {"t": 1.0, "x": 0.0, "y": -0.8, "z": 0.5, "type": 0, "dur": 0, "judge_point": "main", "fx": null},
    {"t": 1.5, "x": 0.3, "y": -0.6, "z": 0.5, "type": 1, "dur": 0.4, "judge_point": "main", "fx": null}
  ]
}
```

### Why this format

- JSON = trivial parse in C# (System.Text.Json), readable in any editor, great for AI.
- Matches exactly the `ChartData` / `NoteData` / `JudgePointData` in interfaces.
- Easy to hand-author or generate.
- Room to grow without parser breakage (add optional fields).
- Free-track coordinate system enables arbitrary choreography.

### Future (v1.0+)

- Compact text variant or binary for size.
- Variable BPM via `events` (bpm_change type).
- Slide curve control points: `cp: [{x,y}, {x,y}, ...]`.
- Judge point path following (spline).
- Per-note visual overrides (color, size, custom shader).
- MOD support: custom Fx modules via WASM/scripting.

See also: `docs/GAMEPLAY.md` for judgement windows, `src/core/Interfaces/IChartReader.cs`, `src/core/Interfaces/IJudgementSystem.cs`, `src/core/Interfaces/IFxBus.cs`