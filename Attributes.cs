using System;

namespace Brdd.Design.Core.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class BrddUseCaseAttribute : Attribute {
        public string Id { get; }
        public BrddUseCaseAttribute(string id) => Id = id;
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class BrddRuleAttribute : Attribute {
        public string Id { get; }
        public string Message { get; }
        public BrddRuleAttribute(string id, string message = "") {
            Id = id;
            Message = message;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class BrddEffectAttribute : Attribute {
        public string Id { get; }
        public BrddEffectAttribute(string id) => Id = id;
    }
}
