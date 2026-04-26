# 🤖 AI Guidelines for brdd-dotnet

## 🏗 Core Components

### 1. `ExecutionContext<T>`
```csharp
public record ExecutionContext<T>(
    T? Data,
    List<BRDDError> Errors,
    List<string> Setters,
    List<string> Effects,
    int Status
);
```

### 2. Implementation Rules
- **Task Parallel Library:** All service methods should be `async Task<...>`.
- **System.Text.Json:** Use standard attributes for JSON property naming.
- **Middlewares:** Implement a global result filter that automatically handles `ExecutionContext` responses.
