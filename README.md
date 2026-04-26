# 🔷 Brdd.Design.Core (.NET)

[![NuGet](https://img.shields.io/nuget/v/Brdd.Design.Core?label=nuget)](https://www.nuget.org/packages/Brdd.Design.Core/)

**Business Rule Driven Design (BRDD)** implementation for .NET. 

This library provides the foundational building blocks for implementing the BRDD pattern in .NET applications, prioritizing business rules as the first-class citizens of your architecture.

## 🤖 AI-First by Design
Brdd.Design.Core is built to be **AI-Native**. Its highly structured pattern of `ExecutionContext`, `ValidationContext`, and specialized services makes it exceptionally easy for AI coding assistants to understand, implement, and test your business logic without ambiguity.

## 📦 Key Components
- **`ExecutionContext`**: A narrative container for data, setters, and side effects.
- **`ValidationContext`**: A standardized report for business rule compliance.
- **`UseCase`**: The central orchestrator for business processes.

## 🚀 Installation
```bash
dotnet add package Brdd.Design.Core
```

## 🏗 Usage Example
```csharp
// Define your Use Case
public class ProcessOrderUseCase : IUseCase<OrderInput, OrderResult> 
{
    public async Task<ExecutionContext<OrderResult>> Execute(OrderInput input) 
    {
        var context = ExecutionContext.Init<OrderResult>("US001_PROCESS_ORDER");
        
        // 1. Validation
        if (input.Amount <= 0) {
            context.Validation.AddError("ORD_001", "Invalid amount");
            return context;
        }

        // 2. Logic & Setters
        context.AddSetter("SET_STATUS", "PAID");
        
        // 3. Side Effects
        context.AddEffect(EffectType.Post, "EFF_SEND_EMAIL", async () => { /* ... */ });

        return context;
    }
}
```

## 📚 Links
- **Official Website:** [https://brdd.design](https://brdd.design)
- **Manifesto:** [BRDD Manifesto](https://github.com/brdd-design/brdd/blob/main/BRDD.md)
- **GitHub Repository:** [brdd-dotnet](https://github.com/brdd-design/brdd-dotnet)

---
Officially maintained by **Defol Tech**.
