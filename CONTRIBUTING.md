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

| Type           | Purpose                                                | Example                                            |
|:---------------|:-------------------------------------------------------|:---------------------------------------------------|
| **`feat`**     | A new feature                                          | `feat(auth): add JWT token refresh endpoint`       |
| **`fix`**      | A bug fix                                              | `fix(api): resolve null reference in user mapper`  |
| **`refactor`** | Code refactoring without changing external behavior    | `refactor(tests): simplify page object models`     |
| **`docs`**     | Documentation changes                                  | `docs: add commit conventions guide`               |
| **`test`**     | Adding or updating tests                               | `test(e2e): add checkout flow Playwright tests`    |
| **`chore`**    | Maintenance, updating dependencies, tool configuration | `chore(deps): bump Google.GenAI version to 1.19.0` |
| **`perf`**     | Code changes that improve performance                  | `perf(db): add index for user email search`        |
| **`build`**    | Changes to build system or external dependencies       | `build(docker): update base Node.js image`         |
| **`ci`**       | Changes to CI/CD pipelines and workflows               | `ci: add test execution step on pull request`      |

---

## 💡 Best Practices

1. **Use Imperative Mood:** Write the summary in the imperative mood ("add", not "added" or "adds").
2. **Lowercase:** Start the summary after the colon with a lowercase letter.
3. **No Trailing Period:** Do not end the subject line with a period `.`.

# Pull Request Conventions Guide

Please choose the appropriate merge strategy based on the nature of your changes:

## Rebase and Merge (Default)

Use for clean, sequential commits that do not introduce merge conflicts. Keep history linear without adding unnecessary merge commits.

## Squash and Merge

Use when a branch contains multiple small, intermediate, or "work-in-progress" commits (e.g., fix typo, address review). This combines all changes into a single clean commit in master.

## Merge Commit

Use when resolving complex merge conflicts or when retaining explicit branch integration context is required.

# Branch Naming Conventions Guide

We use a standardized lowercase branch naming convention based on commit types.

## 📌  Branch Name Structure

```text
<type>/[issue-number-]/<short-description>
```

**type** – Matches the Conventional Commits types (e.g., feat, fix, chore, test, refactor).

**issue-number** – The optional [issue tracker](https://github.com/Bl1c-S/vgt7-voice/issues) ticket number (e.g., 42).

**short-description** – A concise, hyphen-separated.

## 🏷️ Branch Types and Examples

| Type           | Simple Branch                   | Layered Branch Structure            |
|:---------------|:--------------------------------|:------------------------------------|
| **`feat`**     | `feat/12-jwt-auth-service`      | `jwt-12/feat/auth-service`          |
| **`fix`**      | `fix/43-postgres-logging-error` | `jwt-12/feat/token-service`         |
| **`refactor`** | `refactor/identity-models`      | `jwt-12/refactor/login-controllers` |
| **`test`**     | `test/1-playwright-login-flow`  | `jwt-12/docs/login-md`              |

## 💡 Best Practices

1. Use kebab-case: Always use lowercase letters and separate words.
2. Keep it Descriptive but Concise.
3. Delete branch After Merging