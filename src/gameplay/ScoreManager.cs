// ScoreManager.cs
// 计分管理器最小实现
// 纯逻辑，无 Unity 依赖，可独立测试
// ponytail: 最少代码满足 IScoreManager

using BUGs.Core.Interfaces;

namespace BUGs.Gameplay
{
    public class ScoreManager : IScoreManager
    {
        private long _score;
        private int _combo;
        private int _maxCombo;
        private int _totalNotes;
        private int _hitNotes;

        // 简易权重（与 gameplay/README 一致，待实测调整）
        private const long PerfectScore = 1000;
        private const long GreatScore   = 800;
        private const long GoodScore    = 500;

        public void ProcessJudgement(JudgementEvent judgement)
        {
            _totalNotes++;

            switch (judgement.Result)
            {
                case Judgement.Perfect:
                    _score += PerfectScore;
                    _combo++;
                    _hitNotes++;
                    break;
                case Judgement.Great:
                    _score += GreatScore;
                    _combo++;
                    _hitNotes++;
                    break;
                case Judgement.Good:
                    _score += GoodScore;
                    _combo++;
                    _hitNotes++;
                    break;
                case Judgement.Miss:
                    _combo = 0;
                    break;
            }

            if (_combo > _maxCombo) _maxCombo = _combo;
        }

        public long GetScore() => _score;
        public int GetCombo() => _combo;
        public int GetMaxCombo() => _maxCombo;

        public float GetAccuracy()
        {
            if (_totalNotes == 0) return 1f;
            return (float)_hitNotes / _totalNotes;
        }

        public string GetGrade()
        {
            var acc = GetAccuracy();
            if (acc >= 0.95f) return "S";
            if (acc >= 0.90f) return "A";
            if (acc >= 0.80f) return "B";
            if (acc >= 0.70f) return "C";
            return "D";
        }

        public ScoreStats GetStats() => new()
        {
            Score = _score,
            Combo = _combo,
            MaxCombo = _maxCombo,
            Accuracy = GetAccuracy(),
            Grade = GetGrade(),
            Judgements = default // 由 JudgementSystem 提供更完整统计
        };

        public void Reset()
        {
            _score = 0;
            _combo = 0;
            _maxCombo = 0;
            _totalNotes = 0;
            _hitNotes = 0;
        }
    }
}
