using FluentAssertions;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ValidatorTest.Tests
{
    public class CustomValidatorShould
    {
        [Fact]
        public void PassValidEnum()
        {
            var testData = TestEnum.Value0;
            var context = new ValidationContext(testData)
            {
                DisplayName = "testData"
            };
            var result = CustomValidator.ValidateEnum(testData, context);
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void FailInvalidEnum()
        {
            var testData = Enum.Parse(typeof(TestEnum), "4");
            var context = new ValidationContext(testData)
            {
                DisplayName = "testData"
            };
            var result = CustomValidator.ValidateEnum(testData, context);
            result.Should().NotBe(ValidationResult.Success);
        }

        [Fact]
        public void PassValidEnumFlags()
        {
            var testData = TestEnumFlags.Value0 | TestEnumFlags.Value1 | TestEnumFlags.Value2;
            var context = new ValidationContext(testData);
            context.DisplayName = "testData";
            var result = CustomValidator.ValidateEnum(testData, context);
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void FailInvalidEnumFlags()
        {
            var testData = Enum.Parse(typeof(TestEnumFlags), "4");
            var context = new ValidationContext(testData)
            {
                DisplayName = "testData"
            };
            var result = CustomValidator.ValidateEnum(testData, context);
            result.Should().NotBe(ValidationResult.Success);
        }
    }
    public enum TestEnum
    {
        Value0 = 0,
        Value1 = 1,
        Value2 = 2
    }

    [Flags]
    public enum TestEnumFlags
    {
        Value0 = 0,
        Value1 = 1,
        Value2 = 2
    }
}
