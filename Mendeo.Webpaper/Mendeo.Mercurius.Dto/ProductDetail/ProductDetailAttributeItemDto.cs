﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailAttributeItemDto : BaseDto
    {
        public int AttributeItemID { get; set; }
        public Nullable<int> AttributeTypeID { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public int Sort { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> NameBool { get; set; }
        public Nullable<decimal> NameNumber { get; set; }
        public Nullable<System.DateTime> NameDate { get; set; }
        public Nullable<int> ItemPrimitiveType { get; set; }
        public string ItemUnitLabel { get; set; }
    }
}
