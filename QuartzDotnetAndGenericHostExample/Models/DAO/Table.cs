using System;
using System.Collections.Generic;

#nullable disable

namespace QuartzDotnetAndGenericHost.Models.DAO
{
    public partial class Table
    {
        public int Id { get; set; }
        public string MyColumn1 { get; set; }
        public decimal? MyColumn2 { get; set; }
        public Guid? MyColumn3 { get; set; }
        public string MyColumn4 { get; set; }
    }
}
