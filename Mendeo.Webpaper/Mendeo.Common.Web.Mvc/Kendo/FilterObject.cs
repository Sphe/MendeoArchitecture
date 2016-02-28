﻿namespace Mendeo.Common.Web.Mvc.Kendo
{
    public class FilterObject
    {
        public string Field1 { get; set; }
        public string Operator1 { get; set; }
        public string Value1 { get; set; }
        public string Field2 { get; set; }
        public string Operator2 { get; set; }
        public string Value2 { get; set; }
        public string Logic { get; set; }
        public bool IsConjugate
        {
            get { return (Field2 != null); }
        }
    }
}