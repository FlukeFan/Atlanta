
using System;

using Atlanta.Application.Domain.DomainBase;

namespace Atlanta.Application.Domain.Lender
{

    /// <summary>
    /// Class to represent Media filter/order criteria
    ///
    ///  TODO: Need to code-generate these classes
    /// </summary>
    public class MediaCriteria : DomainCriteriaBase<MediaCriteria>
    {

        // Begin Media.Type filter
        MediaType[]     _typeValues = new MediaType[0];

        /// <summary> Check for number of Type filters </summary>
        public int TypeFilterCount { get { return _typeValues.Length; } }

        /// <summary> Retrieves the value for the filter</summary>
        public MediaType GetTypeFilter(int i)
        {
            return _typeValues[i];
        }

        /// <summary> Set the Type filter, and return the modified filter object </summary>
        public MediaCriteria SetTypeFilter(MediaType type)
        {
            MediaType[] newValue = new MediaType[_typeValues.Length + 1];
            _typeValues.CopyTo(newValue, 1);
            _typeValues = newValue;

            _typeValues[0] = type;
            return this;
        }

        /// <summary> Clear the Type filter </summary>
        public void ClearTypeFilter()
        {
            _typeValues = new MediaType[0];
        }
        // End Media.Type filter


        // Begin Media.Name filter ...
        string[]        _nameValues = new string[0];

        /// <summary> Check for number of Name filters </summary>
        public int NameFilterCount { get { return _nameValues.Length; } }

        /// <summary> Retrieves the value for the filter</summary>
        public string GetNameFilter(int i)
        {
            return _nameValues[i];
        }

        /// <summary> Set the Name filter, and return the modified filter object </summary>
        public MediaCriteria SetNameFilter(string name)
        {
            string[] newValue = new string[_nameValues.Length + 1];
            _nameValues.CopyTo(newValue, 1);
            _nameValues = newValue;

            _nameValues[0] = name;
            return this;
        }

        /// <summary> Clear the Name filter </summary>
        public void ClearNameFilter()
        {
            _nameValues = new string[0];
        }

    }

}

