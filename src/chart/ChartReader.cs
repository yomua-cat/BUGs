// ChartReader.cs
// 谱面读取器实现（最小可用）
// 遵循 IChartReader 契约
// ponytail: 用 System.Text.Json 零自定义解析器

using System;
using System.IO;
using System.Text.Json;
using BUGs.Core.Interfaces;

namespace BUGs.Chart
{
    public class ChartReader : IChartReader
    {
        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ChartData ReadFromFile(string path)
        {
            var json = File.ReadAllText(path);
            return ReadFromString(json);
        }

        public ChartData ReadFromString(string content)
        {
            var raw = JsonSerializer.Deserialize<RawChart>(content, JsonOpts)
                      ?? throw new InvalidOperationException("Invalid chart JSON");

            var notes = new NoteData[raw.Notes?.Length ?? 0];
            for (int i = 0; i < notes.Length; i++)
            {
                var n = raw.Notes[i];
                notes[i] = new NoteData
                {
                    Time = n.t,
                    Channel = n.ch,
                    Type = n.type,
                    Duration = n.dur
                };
            }

            return new ChartData
            {
                Notes = notes,
                BPM = raw.Meta?.bpm ?? 120,
                Offset = raw.Meta?.offset ?? 0
            };
        }

        public ChartValidationResult Validate(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                var _ = ReadFromString(json); // round-trip parse test
                return new ChartValidationResult { IsValid = true, Errors = Array.Empty<string>() };
            }
            catch (Exception ex)
            {
                return new ChartValidationResult { IsValid = false, Errors = new[] { ex.Message } };
            }
        }

        public ChartMetadata GetMetadata(string path)
        {
            var json = File.ReadAllText(path);
            var raw = JsonSerializer.Deserialize<RawChart>(json, JsonOpts);

            return new ChartMetadata
            {
                Title = raw?.Meta?.title ?? "",
                Artist = raw?.Meta?.artist ?? "",
                Charter = raw?.Meta?.charter ?? "",
                Difficulty = raw?.Meta?.difficulty ?? "",
                Level = raw?.Meta?.level ?? 0,
                BPM = raw?.Meta?.bpm ?? 0,
                Duration = raw?.Meta?.duration ?? 0
            };
        }

        // Internal DTOs for JSON mapping (keeps public structs clean)
        private record RawChart(RawMeta? Meta, RawNote[]? Notes);
        private record RawMeta(string? title, string? artist, string? charter, string? difficulty, int level, double bpm, double offset, double duration);
        private record RawNote(double t, int ch, int type, double dur);
    }
}
