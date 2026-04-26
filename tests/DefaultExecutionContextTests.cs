using System.Linq;
using Xunit;
using Brdd.Design.Core;

namespace Brdd.Design.Core.Tests
{
    public class DefaultExecutionContextTests
    {
        [Fact]
        public void Should_Initialize_With_Default_Values()
        {
            var ctx = new DefaultExecutionContext<int>(1);

            Assert.Equal(1, ctx.Data);
            Assert.Empty(ctx.Errors);
            Assert.Empty(ctx.Effects);
            Assert.Empty(ctx.Setters);
            Assert.Equal(200, ctx.Status);
            Assert.True(ctx.IsValid());
        }

        [Fact]
        public void Should_Add_Error_And_Update_Status()
        {
            var ctx = new DefaultExecutionContext<int>();
            ctx.AddError("R001", "Invalid test");

            Assert.Single(ctx.Errors);
            Assert.Equal("R001", ctx.Errors.First().Code);
            Assert.Equal(400, ctx.Status);
            Assert.False(ctx.IsValid());
        }

        [Fact]
        public void Should_Add_Effect_And_Setter()
        {
            var ctx = new DefaultExecutionContext<int>();
            ctx.AddEffect("E001");
            ctx.AddSetter("S001");

            Assert.Contains("E001", ctx.Effects);
            Assert.Contains("S001", ctx.Setters);
        }

        [Fact]
        public void Should_Set_Data()
        {
            var ctx = new DefaultExecutionContext<int>();
            ctx.SetData(2);

            Assert.Equal(2, ctx.Data);
        }
    }
}
