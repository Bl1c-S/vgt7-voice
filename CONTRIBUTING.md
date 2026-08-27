# Pull Request Conventions Guide

This guide outlines the conventions for creating pull requests in this repository.

## ✨ Issue

First, create a [new issue](https://github.com/Bl1c-S/vgt7-voice/issues) for the task you are working on. This allows for automatic tracking of tasks. Make a note of the issue number.

A good issue title is concise and written in a free style.

---

## 🌱 Branch

Create a branch from the issue, following the conventions below.

```text
<type>/[issue-number]-<short-description>
```

---

## 🔰 Pull Request

Create a [new pull request](https://github.com/Bl1c-S/vgt7-voice/pulls) from your branch.

In the pull request description, add a line that says `Closes #[issue-number]` to automatically close the corresponding issue.

```text
Closes #[issue-number]
```

- Describe the changes you made in the pull request.
- Assign reviewers, assignees, labels, and projects for the PR.

After the final commit, the PR needs to be moved to the `In Review` column. After a successful review, the PR will be merged, and the linked issue will automatically move to the `Done` column.

---

# Branch Conventions Guide

## 📌 Branch Name Structure

We use a standardized lowercase branch naming convention based on commit types.

```text
<type>/[issue-number]-<short-description>
```

**type** – Matches the Conventional Commits types (e.g., feat, fix, chore, test, refactor).

**issue-number** – The optional [issue tracker](https://github.com/Bl1c-S/vgt7-voice/issues) ticket number (e.g., 42).

**short-description** – A concise, hyphen-separated description.

---

## 🏷️ Branch Types and Examples

| Type           | Simple Branch                   | Layered Branch Structure            |
|:---------------|:--------------------------------|:------------------------------------|
| **`feat`**     | `feat/12-jwt-auth-service`      | `jwt-12/feat/auth-service`          |
| **`fix`**      | `fix/43-postgres-logging-error` | `jwt-12/feat/token-service`         |
| **`refactor`** | `refactor/identity-models`      | `jwt-12/refactor/login-controllers` |
| **`test`**     | `test/1-playwright-login-flow`  | `jwt-12/docs/login-md`              |

---

## 🌿 Branch Merging Strategy

Please choose the appropriate merge strategy based on the nature of your changes.

---

### 💚 Rebase and Merge (Default)

Use for clean, sequential commits that do not introduce merge conflicts. Keep history linear without adding unnecessary merge commits.

### 💙 Squash and Merge

Use when a branch contains multiple small, intermediate, or "work-in-progress" commits (e.g., fix typo, address review). This combines all changes into a single clean commit in master.

### 💜 Merge Commit

Use when resolving complex merge conflicts or when retaining explicit branch integration context is required.

---

## 💡 Best Practices

1. Use kebab-case: Always use lowercase letters and separate words.
2. Keep it Descriptive but Concise.
3. Delete branches after merging.

---

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

---