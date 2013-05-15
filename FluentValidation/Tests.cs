using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FluentValidation
{
    internal class TestClass
    {
        //===============================================================
        public TestClass(String stringProp, int intProp, TestClass nestedObject)
        {
            StringProperty = stringProp;
            IntProperty = intProp;
            NestedObject = nestedObject;
        }
        //===============================================================
        public String StringProperty { get; set; }
        //===============================================================
        public int IntProperty { get; set; }
        //===============================================================
        public TestClass NestedObject { get; set; }
        //===============================================================
    }

    [TestFixture]
    class Tests
    {
        //===============================================================
        [Test]
        public void ValidationWorks()
        {
            var val = new TestClass("outer", 1, new TestClass("inner", 2, null));

            var validate = Validate.For(val)
                                  .That(x => x.IntProperty > 0, "error")
                                  .That(x => x.IntProperty, Rules.IsGreaterThan(0));

            Assert.IsFalse(validate.HasErrors);

            validate.That(x => x.IntProperty > 10, "error");
            Assert.IsTrue(validate.HasErrors);
            Assert.IsTrue(validate.Errors.First() == "error");
        }
        //===============================================================
    }
}
