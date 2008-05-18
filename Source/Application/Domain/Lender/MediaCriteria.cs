
using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent Media filter/order criteria
    ///
    ///  TODO: Need to add order-by criteria
    ///  TODO: Need to code-generate these classes
    /// </summary>
    [Serializable]
    public class MediaCriteria : DomainCriteria<MediaCriteria, Media>
    {

        /// <summary> Overriden to check a domain object passes the filters </summary>
        override protected bool PassesFilter(Media media)
        {
            return CompareIntFilters("Type", (int) media.Type)
                    && CompareStringFilters("Name", media.Name);
        }


        /// <summary> Check for number of Type filters </summary>
        public int TypeFilterCount
        {
            get { return GetFilterCount("Type"); }
        }

        /// <summary> Retrieves the value for the filter</summary>
        public MediaType GetTypeFilter(int i)
        {
            return (MediaType) GetFilterValue("Type", i);
        }

        /// <summary> Retrieves the value for the filter</summary>
        public FilterCondition GetTypeFilterCondition(int i)
        {
            return GetFilterCondition("Type", i);
        }

        /// <summary> Set the Type filter, and return the modified filter object </summary>
        public MediaCriteria SetTypeFilter(MediaType type, FilterCondition filterCondition)
        {
            SetFilter("Type", filterCondition, type);
            return this;
        }

        /// <summary> Set the Type filter with an equality condition, and return the modified filter object </summary>
        public MediaCriteria SetTypeFilter(MediaType type)
        {
            return SetTypeFilter(type, FilterCondition.Equal);
        }

        /// <summary> Clear the Type filter </summary>
        public MediaCriteria ClearTypeFilter()
        {
            ClearFilter("Type");
            return this;
        }



        /// <summary> Check for number of Name filters </summary>
        public int NameFilterCount
        {
            get { return GetFilterCount("Name"); }
        }

        /// <summary> Retrieves the value for the filter</summary>
        public string GetNameFilter(int i)
        {
            return (string) GetFilterValue("Name", i);
        }

        /// <summary> Retrieves the value for the filter</summary>
        public FilterCondition GetNameFilterCondition(int i)
        {
            return GetFilterCondition("Name", i);
        }

        /// <summary> Set the Name filter, and return the modified filter object </summary>
        public MediaCriteria SetNameFilter(string name, FilterCondition filterCondition)
        {
            SetFilter("Name", filterCondition, name);
            return this;
        }

        /// <summary> Set the Name filter with an equality condition, and return the modified filter object </summary>
        public MediaCriteria SetNameFilter(string name)
        {
            return SetNameFilter(name, FilterCondition.Equal);
        }

        /// <summary> Clear the Name filter </summary>
        public MediaCriteria ClearNameFilter()
        {
            ClearFilter("Name");
            return this;
        }

    }

}

