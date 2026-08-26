# Commit Conventions Guide

This repository follows the **Conventional Commits** specification. Using this standard ensures an explicit, readable commit history and allows for automated changelog generation.

---

## 📌 Commit Message Structure

```text
<type>(<scope>): <short summary>

[optional body]

[optional footer(s)]
```

* **`type`**: Mandatory tag describing the purpose of the commit.
* **`scope`**: Optional tag indicating the part of the codebase affected (e.g., `auth`, `api`, `ui`, `playwright`, `deps`).
* **`short summary`**: Succinct description of the changes.

---

## 🏷️ Commit Types

| Type | Purpose | Example |
| :--- | :--- | :--- |
| **`feat`** | A new feature | `feat(auth): add JWT token refresh endpoint` |
| **`fix`** | A bug fix | `fix(api): resolve null reference in user mapper` |
| **`refactor`** | Code refactoring without changing external behavior | `refactor(tests): simplify page object models` |
| **`docs`** | Documentation changes | `docs: add commit conventions guide` |
| **`test`** | Adding or updating tests | `test(e2e): add checkout flow Playwright tests` |
| **`chore`** | Maintenance, updating dependencies, tool configuration | `chore(deps): bump Google.GenAI version to 1.19.0` |
| **`perf`** | Code changes that improve performance | `perf(db): add index for user email search` |
| **`build`** | Changes to build system or external dependencies | `build(docker): update base Node.js image` |
| **`ci`** | Changes to CI/CD pipelines and workflows | `ci: add test execution step on pull request` |

---

## 💡 Best Practices

1. **Use Imperative Mood:** Write the summary in the imperative mood ("add", not "added" or "adds").
    * ✅ `feat: add user login`
    * ❌ `feat: added user login`
2. **Lowercase:** Start the summary after the colon with a lowercase letter.
3. **No Trailing Period:** Do not end the subject line with a period `.`.