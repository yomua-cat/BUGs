# 贡献指南

## 开发原则

1. **文档先于实现** — 任何功能先写文档，再写代码
2. **架构先于编码** — 先定义接口，再实现
3. **小步提交** — 每个 commit 只做一件事
4. **AI 友好** — 代码和文档必须能被 AI 模型理解

## 工作流

```
1. Fork 仓库
2. 创建功能分支：git checkout -b feature/xxx
3. 编写/更新文档
4. 定义接口（如需要）
5. 实现功能
6. 编写测试
7. 提交：git commit -m "feat: xxx"
8. 推送：git push origin feature/xxx
9. 创建 Pull Request
10. 等待 Review
```

## 提交规范

```
feat: 新功能
fix: 修复 bug
docs: 文档变更
refactor: 重构
test: 测试
chore: 构建/工具
```

## 模块开发规则

1. 模块间只能通过 `src/core/Interfaces/` 通信
2. 永远不直接引用另一个模块的实现
3. 接口变更必须同步更新 `core/Interfaces/` + 所有实现模块
4. 每个模块有独立的 `README.md`

## 代码风格

- C# 命名：PascalCase（类/方法）、camelCase（变量）
- 纯函数优先
- 依赖注入
- 函数 < 50 行
- 见 [code-quality.md](.opencode/context/core/standards/code-quality.md)

## AI 协作

本仓库设计为 AI 友好。AI 贡献者应：

1. 先阅读 `docs/` 了解项目
2. 先阅读 `src/core/Interfaces/` 了解模块契约
3. 先阅读目标模块的 `README.md`
4. 变更接口时同步更新文档

## 许可证

MIT。贡献即同意代码以 MIT 许可证发布。