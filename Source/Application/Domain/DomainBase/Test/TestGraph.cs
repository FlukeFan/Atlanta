﻿
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public Child() { ChildrenList = new List<Grandchild>(); }
        public string Name { get; set; }
        public Parent Parent { get; set; }
        protected IList<Grandchild> ChildrenList { get; set; }
        public IEnumerable<Grandchild> Children { get { return ChildrenList; } }
        public Child Add(Grandchild grandchild) { ChildrenList.Add(grandchild); return this; }
    }

    public class Grandchild : TestGraphRoot
    {
        public string Name { get; set; }
        public Child Parent { get; set; }
    }


    [TestFixture]
    public class TestGraph : DomainPersistenceTestBase
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

        [Test]
        public void TestCopyNullValue()
        {
            Parent parent = null;

            Parent parentCopy = parent.Graph().Copy();

            Assert.IsNull(parent, "parent is not null");
        }

        [Test]
        public void TestCopyNullReference()
        {
            Child child = new Child() { Id=1, Name="child2", Parent=null };

            Child childCopy =
                child
                    .Graph()
                    .Add(c => c.Parent)
                    .Copy();

            Assert.IsNull(child.Parent, "parent is not null");
        }

        [Test]
        public void TestCopyList()
        {
            Parent parent1 = Parent.Create().SetId(1).SetName("parent1");
            Parent parent2 = Parent.Create().SetId(2).SetName("parent2");
            IList<Parent> parentList = new List<Parent>() { parent1, parent2 };

            IList<Parent> parentListCopy = parentList.Graph().Copy();

            Assert.AreNotSame(parentList, parentListCopy);
            Assert.AreEqual(parentList.Count, parentListCopy.Count);
            Assert.AreNotSame(parentList[0], parentListCopy[0]);
            Assert.AreNotSame(parentList[1], parentListCopy[1]);
            Assert.AreEqual(1, parentListCopy[0].Id);
        }

        [Test]
        public void TestCopyListAndSubGraph()
        {
            Parent parent = Parent.Create().SetId(3).SetName("parent3");
            Child child1 = new Child() { Id=1, Name="child1", Parent=parent };
            Child child2 = new Child() { Id=2, Name="child2", Parent=parent };
            IList<Child> childList = new List<Child>() { child1, child2 };

            IList<Child> childListCopy =
                childList
                    .GraphList()
                    .Add(c => c.Parent)
                    .CopyList();

            Assert.AreEqual(2, childListCopy.Count);
            Assert.AreEqual(3, childListCopy[1].Parent.Id);
        }

        [Test]
        public void TestEnumeration()
        {
            Child child = new Child() { Id=1, Name="child2", Parent=null };
            child.Add(new Grandchild() { Id=2, Name="granchild" });

            Child childCopy =
                child
                    .Graph()
                    .Add(c => c.Children)
                    .Copy();

            Assert.AreNotSame(child, childCopy);
            Assert.AreEqual(1, childCopy.Id);

            Assert.AreNotSame(child.Children, childCopy.Children);
            Assert.AreEqual(1, childCopy.Children.Count());
            Assert.AreNotSame(child.Children.First(), childCopy.Children.First());
            Assert.AreEqual(2, childCopy.Children.First().Id);
            Assert.AreEqual("granchild", childCopy.Children.First().Name);
        }

        [Test]
        public void TestSpecifiedSubGraph()
        {
            Parent parent = Parent.Create().SetId(1).SetName("parent");
            Child child = new Child() { Id=2, Name="child", Parent=parent };
            parent.ChildList.Add(child);

            Parent parentCopy =
                parent
                    .Graph()
                    .Add(p => p.ChildList, new Graph<Child>()
                        .Add(c => c.Parent))
                    .Copy();

            Assert.AreNotEqual(parent, parentCopy);
            Assert.AreEqual(1, parentCopy.Id);
            Child childCopy = parentCopy.ChildList[0];
            Assert.AreNotEqual(child, childCopy);
            Assert.AreEqual(2, childCopy.Id);
            Assert.AreNotEqual(parentCopy, childCopy.Parent);
            Assert.AreEqual(1, childCopy.Parent.Id);
        }

        [Test]
        public void TestDeepGraph()
        {
            Parent parent = Parent.Create().SetId(1).SetName("parent");
            Child child = new Child() { Id=2, Name="child", Parent=parent };
            parent.ChildList.Add(child);
            Grandchild grandchild = new Grandchild() { Id=3, Name="granchild", Parent=child };
            child.Add(grandchild);

            Parent parentCopy =
                parent
                    .Graph()
                    .Add(p => p.ChildList, new Graph<Child>()
                        .Add(c => c.Children, new Graph<Grandchild>()
                            .Add(g => g.Parent)))
                    .Copy();

            Assert.AreNotEqual(parent, parentCopy);
            Child childCopy = parentCopy.ChildList[0];
            Assert.AreNotEqual(child, childCopy);
            Assert.AreEqual(2, childCopy.Id);
            Child grandchildParent = childCopy.Children.First().Parent;
            Assert.AreNotEqual(child, grandchildParent);
            Assert.AreNotEqual(childCopy, grandchildParent);
            Assert.AreEqual(2, grandchildParent.Id);
        }

    }

}
