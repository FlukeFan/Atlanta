
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using NHibernate;
using NHibernate.Cfg;

using NUnit.Framework;

namespace Atlanta.Application.Domain.DomainBase.Test
{

    [TestFixture]
    public abstract class DomainTestBase
    {

        [SetUp]
        virtual public void SetUp()
        {
        }

        [TearDown]
        virtual public void TearDown()
        {
            DomainRegistry.Library = null;
        }

        public object MakeCopy(object sourceObject)
        {
            object copy;

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            formatter.Serialize(stream, sourceObject);
            stream.Seek(0, SeekOrigin.Begin);

            copy = formatter.Deserialize(stream);
            stream.Close();

            return copy;
        }

    }

}

