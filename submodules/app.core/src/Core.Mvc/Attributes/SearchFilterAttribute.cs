using System;

namespace Core.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SearchFilterAttribute: Attribute
    {
        public string LabelName { get; private set; }
        public string FieldType { get; private set; }
        public bool DataGroup { get; private set; }
        public int Order { get; private set; }

        public SearchFilterAttribute(string labelName, string fieldType, int order = 1, bool dataGroup = false)
        {
            LabelName = labelName;
            FieldType = fieldType;
            DataGroup = dataGroup;
            Order = order;
        }
    }
}
// attribute for all types exclusive of dataGroup:
//          [SearchFilter(LabelName: "some name", FieldType: "some type")]
// attribute for dataGroup:
//          [SearchFilter(LabelName: "some name", FieldType: "some type", DataGroup: true/false)]

