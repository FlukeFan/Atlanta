
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

        [Test]
        public void TestCopySingleObjectIgnoreReferenceType()
        {
            Parent parent = Parent.Create().SetId(1).SetName("parent1");
            Child child = new Child() { Id=2, Name="child2", Parent=parent };

            Child childCopy = child.Graph().Copy();

            Assert.AreNotSame(child, childCopy);
            Assert.AreEqual(2, childCopy.Id);
            Assert.AreEqual("child2", childCopy.Name);
            Assert.AreEqual(null, childCopy.Parent);
        }

        [Test]
        public void TestCopySingleObjectAddReferenceType()
        {
            Parent parent = Parent.Create().SetId(1).SetName("parent1");
            Child child = new Child() { Id=2, Name="child2", Parent=parent };

            Child childCopy =
                child
                    .Graph()
                    .Add(c => c.Parent)
                    .Copy();

            Assert.AreNotSame(child, childCopy);
            Assert.AreEqual(2, childCopy.Id);
            Assert.AreEqual("child2", childCopy.Name);

            Assert.IsNotNull(childCopy.Parent, "childCopy.Parent not copied");
            Assert.AreNotSame(parent, childCopy.Parent);
            Assert.AreEqual(1, childCopy.Parent.Id);
            Assert.AreEqual("parent1", childCopy.Parent.Name);
            Assert.AreEqual(0, childCopy.Parent.ChildList.Count);
        }

    }

}
