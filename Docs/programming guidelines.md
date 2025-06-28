# DEVELOPMENT GUIDELINES

These guidelines define coding standards and best practices for developing in the Gluten Guardian Project. They are meant to ensure code quality, observability, maintainability, and seamless collaboration within AI-augmented workflows.

The guideline is here to produce traceable, testable, and validated code.
## 1. Logging

### ✅ Use Structured Logging
- Use the Logger class which wraps [Pino](https://getpino.io/).
- All logs must be structured JSON.
- Avoid using `console.log` ,vite log or raw `pino` instances.

### 🔧 Logging Format
Every log entry should include:
- `timestamp`
- `level`
- `context` (module or file name)
- `traceID`
- `message`
- `metadata` (structured key-value)

### 📍 Logging Example
```ts
import { logger } from './logger';

logger.info({
  context: 'user-service',
  userId: 'abc123',
  action: 'UserCreated'
}, 'New user created');
```
## 2. OpenTelemetry Integration
📌 Instrumentation
All services and HTTP routes must be traced.

Use the project-standard OpenTelemetry middleware for automatic instrumentation whi

Manual spans should be used for critical business logic.

📍 Manual Span Example
```ts

const span = tracer.startSpan('process-order');
try {
  await processOrder(orderId);
} finally {
  span.end();
}
```
📎 Correlation
Ensure trace IDs are injected into logs.

Use the logger’s context linking feature to enrich logs with trace information.

## 3. Documentation
📄 File Header
Each file must start with a header block:

```ts
/**
 * @fileoverview [Short description of what this module does]
 * @module [module-name]
 */
```
📑 Function & Method Docs
Use JSDoc format:

```ts
/**
 * Processes a new order and updates inventory.
 * 
 * @param {Order} order - The order object.
 * @returns {Promise<boolean>} - Returns true if successful.
 */
async function processOrder(order) { ... }
```

4. Unit Testing
🧪 Test Framework
Use the defined project testing framework - Jest .
test are implemented in the `__tests__` folder. each code file should have adedicated test code file

Tests must cover:

- Normal paths
- Edge cases
- Invalid inputs

✅ Best Practices
- 100% coverage for business logic.
- Avoid relying on external services (use mocks).
- Place tests in __tests__ folders or co-located as .test.ts.

## 5. Input Validation
🔍 Runtime Validation
Use the Zod schema validation library 

All external inputs (HTTP, CLI, etc.) must be validated.

Fail-fast on invalid input.

📍 Example
```ts
const userSchema = z.object({
  id: z.string().uuid(),
  email: z.string().email()
});
const validated = userSchema.parse(input);
```
## 6. Versioning
🔢 SemVer
Follow Semantic Versioning:

MAJOR: Breaking changes

MINOR: New features, backward compatible

PATCH: Fixes and improvements

🛠️ Internal Change Policy
Even internal refactors must bump patch version if deployed.

Library contracts must be versioned with public changelogs.

## 7. Commit Handling
✍️ Conventional Commits
Use the Conventional Commits standard:

```swift

<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
Allowed Types:
feat: New feature

fix: Bug fix

chore: Maintenance or build changes

docs: Documentation only

refactor: Code refactoring

test: Adding or updating tests
```
📍 Examples
```csharp
feat(auth): add OAuth2 flow support
fix(api): handle null response from endpoint
```
## 8. Git Branching
🌿 Feature Branch Flow
Use feature branches for each unit of work:

feature/<short-description>

bugfix/<short-description>

🔄 Process
Branch from dev


Open Pull Request when complete

Use Squash and Merge strategy