
using System;
using System.Reflection;

using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Atlanta.Application.Domain.DomainBase
{

    /// <summary>
    /// Contains rules for auto-mapping
    /// </summary>
    public static class Mapping
    {

        /// <summary> Create mapping </summary>
        public static AutoPersistenceModel CreateMapping()
        {
            return
                AutoMap.AssemblyOf<IdentityConvention>()
                    .Where(Mapping.TypeIsMapped)
                    .Conventions.AddFromAssemblyOf<IdentityConvention>();
        }

        /// <summary> Determine if type should be mapped </summary>
        public static bool TypeIsMapped(Type type)
        {
            return type.IsSubclassOf(typeof(DomainObjectBase));
        }

        /// <summary> Map private property </summary>
        public static bool IsPrivatePropertyMapped(PropertyInfo propertyInfo)
        {
            Console.WriteLine(propertyInfo.Name);
            return false;
        }

        /// <summary> Identity convention </summary>
        public class IdentityConvention : IIdConvention
        {
            /// <summary> Identity convention </summary>
            public void Apply(IIdentityInstance instance)
            {
                instance.GeneratedBy.Native();
            }
        }

        /// <summary> Cascade convention </summary>
        public class OneToManyConvention : IHasManyConvention
        {
            /// <summary> Cascade convention </summary>
            public void Apply(IOneToManyCollectionInstance instance)
            {
                instance.Cascade.AllDeleteOrphan();
            }
        }

    }

}

