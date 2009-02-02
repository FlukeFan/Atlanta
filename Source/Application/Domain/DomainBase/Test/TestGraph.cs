
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    public class TestGraphRoot
    {
        public int Id { get; set; }
    }

    public class Parent : TestGraphRoot
    {
        public static Parent Create() { return new Parent(); }
        protected Parent() { ChildList = new List<Child>(); }
        public string Name { get; set; }
        public IList<Child> ChildList { get; set; }
        public Parent SetId(int id) { Id = id; return this; }
        public Parent SetName(string name) { Name = name; return this; }
    }

    public class Child : TestGraphRoot
    {
        public Child() { Children = new List<Grandchild>(); }
        public string Name { get; set; }
        public Parent Parent { get; set; }
        protected IList<Grandchild> Children { get; set; }
        public IEnumerable ChildrenEnumeration { get { return Children; } }
    }

    public class Grandchild : TestGraphRoot
    {
        public string Name { get; set; }
        public Child Parent { get; set; }
    }


    [TestFixture]
    public class TestGraph : DomainTestBase
    {

        [Test]
        public void TestCopySingleObject()
        {
            Parent parent = Parent.Create().SetId(1).SetName("parent1");

            Parent parentCopy = parent.Graph().Copy();

            Assert.AreNotSame(parent, parentCopy);
            Assert.AreEqual(1, parentCopy.Id);
            Assert.AreEqual("parent1", parentCopy.Name);
            Assert.AreEqual(0, parentCopy.ChildList.Count);
        }

    }

}
